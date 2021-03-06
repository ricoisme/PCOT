using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System.Text.RegularExpressions;
using System.Threading;
using Windows.Globalization;
using Windows.Media.Ocr;
using Windows.Graphics.Imaging;

namespace PCOT
{
    public partial class frmScan : Form
    {
        #region 定数
        /// <summary>スクリーンショット間隔取得用インデックス</summary>
        private const int SS_IDX = 1;
        #endregion

        #region 変数
        /// <summary>FPS制御</summary>
        private Fps fps = new Fps();

        /// <summary>スキャン範囲</summary>
        private Rectangle scanRect;

        /// <summary>スクロールキャプチャ―モード</summary>
        private bool scrollCapture = false;
        /// <summary>スクリーンショット取得フラグ</summary>
        private bool doScreenShot = false;
        /// <summary>スクリーンショット取得完了フラグ</summary>
        private bool endScreenShot = false;
        /// <summary>スクリーンショットカウンター</summary>
        private int sCounter = 0;

        /// <summary>OCRエンジン変更フラグ</summary>
        private bool changeOcrEngine = false;
        /// <summary>読取倍数変更フラグ</summary>
        private bool changeMultiples = false;
        /// <summary>キャプチャーモード変更フラグ</summary>
        private bool changeCaptureMode = false;
        /// <summary>画面保存フラグ</summary>
        private bool doSave = false;
        /// <summary>OCR読み込み失敗フラグ</summary>
        private bool ocrFailed = false;
        /// <summary>画像マージ失敗フラグ</summary>
        private bool imgMergeFailed = false;

        /// <summary>倍率テーブル</summary>
        private List<float> scaleList = new List<float>();
        /// <summary>終了中</summary>
        private bool isClosing = false;
        #endregion

        #region プロパティ
        /// <summary>対象画面の位置座標サイズ</summary>
        public Rectangle Rect { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmScan()
        {
            InitializeComponent();
        }
        #endregion

        #region イベントハンドラ
        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_Load(object sender, EventArgs e)
        {
            try
            {
                Location = new Point(Rect.X, Rect.Y);
                Size = new Size(Rect.Width - Rect.X, Rect.Height - Rect.Y);

                // 常に最前面に表示
                TopMost = true;
                // 透過色の設定
                TransparencyKey = Color.PowderBlue;

                lblMessage.Text = string.Empty;

                // マウスホイールのイベントを追加
                MouseWheel += frmScan_MouseWheel;

                // 倍率リストを定義
                scaleList = new List<float>
                {
                    0.1f,0.2f,0.3f,0.4f,0.5f,0.6f,0.7f,0.8f,0.9f,1,2,3,4,5,6,7,8,9,10
                };

                // 初期化
                Init();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_Shown(object sender, EventArgs e)
        {
            try
            {
                // 自身を選択
                Select();

                while (!isClosing)
                {
                    Application.DoEvents();
                    if (!isClosing)
                    {
                        Refresh();
                        MainLoop();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    // ESCで能動的に閉じる
                    TranslateInfo.TranslateRect = new Rectangle();
                    TranslateInfo.OriginalText = string.Empty;
                    TranslateInfo.ShowScanForm = false;
                    Close();
                }

                if (e.KeyCode == Keys.Space)
                {
                    // OCRエンジンの切替
                    if (!Util.IsEnabledWindows10OcrWithWindowsVersion())
                    {
                        TranslateInfo.UseOcrEngine = 1;
                        return;
                    }

                    if (!Util.IsEnabledWindows10Ocr())
                    {
                        TranslateInfo.UseOcrEngine = 1;
                        return;
                    }

                    if (TranslateInfo.UseOcrEngine == 0)
                    {
                        // Win10 → Tesseract
                        TranslateInfo.UseOcrEngine = 1;
                    }
                    else
                    {
                        // Tesseract → Win10
                        TranslateInfo.UseOcrEngine = 0;
                    }

                    changeOcrEngine = true;
                    Fps.FpsCountList[0] = 0;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.Cross;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスホイール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (doScreenShot)
                {
                    return;
                }

                // 現在のインデックスを取得
                int idx = 0;
                for (int i = 0; i < scaleList.Count; i++)
                {
                    if (scaleList[i] == TranslateInfo.ReadMultiples)
                    {
                        idx = i;
                        break;
                    }
                }

                var value = e.Delta / 120;
                idx = CommonUtil.GetLimitValue(idx + value, 0, scaleList.Count - 1);
                TranslateInfo.ReadMultiples = scaleList[idx];

                changeMultiples = true;
                Fps.FpsCountList[0] = 0;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // スキャン範囲初期化
                InitScanSize();

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (doScreenShot)
                        {
                            doScreenShot = false;
                            endScreenShot = true;
                            return;
                        }

                        var scanPos = PointToClient(Cursor.Position);
                        scanRect.X = scanPos.X;
                        scanRect.Y = scanPos.Y;
                        break;
                    case MouseButtons.Right:
                        doScreenShot = false;
                        InitScanSize();
                        break;
                    case MouseButtons.Middle:
                        if (TranslateInfo.IsFreeSelect)
                        {
                            if (doScreenShot)
                            {
                                endScreenShot = true;
                                return;
                            }
                            scrollCapture = !scrollCapture;
                            changeCaptureMode = true;
                            Fps.FpsCountList[0] = 0;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        var scanPos = PointToClient(Cursor.Position);
                        scanRect.Width = scanPos.X - scanRect.X;
                        scanRect.Height = scanPos.Y - scanRect.Y;
                        break;
                    case MouseButtons.Right:
                        InitScanSize();
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスアップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (scanRect.X != 0 && scanRect.Y != 0 && scanRect.Width != 0 && scanRect.Height != 0)
                {
                    if (scrollCapture)
                    {
                        if (!doScreenShot)
                        {
                            // スクロールキャプチャー取得開始
                            doScreenShot = true;
                            Fps.FpsCountList[SS_IDX] = 0;
                        }
                    }
                    else
                    {
                        doSave = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// スキャンフォームペイント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;

                g.FillRectangle(Brushes.Black, 0, 0, Size.Width, Size.Height);

                if (scanRect.X != 0 && scanRect.Y != 0 && scanRect.Width != 0 && scanRect.Height != 0)
                {
                    using (SolidBrush fillBrush = new SolidBrush(Color.PowderBlue))
                    using (Pen frameNormalPen = new Pen(Color.Cyan, 2))
                    using (Pen frameScrollPen = new Pen(Color.Red, 2))
                    {
                        Rectangle workRect = scanRect;
                        if (scanRect.Width < 0)
                        {
                            // 負数の場合の範囲変換
                            int backW = scanRect.Width;
                            workRect.Width = Math.Abs(scanRect.Width);
                            workRect.X = scanRect.X + backW;
                        }

                        if (scanRect.Height < 0)
                        {
                            // 負数の場合の範囲変換
                            int backH = scanRect.Height;
                            workRect.Height = Math.Abs(scanRect.Height);
                            workRect.Y = scanRect.Y + backH;
                        }

                        // 矩形描画(スキャン範囲)
                        g.FillRectangle(fillBrush, workRect);

                        // 枠線範囲を定義
                        int x = workRect.X - 1;
                        int y = workRect.Y - 1;
                        int w = workRect.Width + 2;
                        int h = workRect.Height + 2;
                        Rectangle frameRect = new Rectangle(x, y, w, h);

                        if (scrollCapture)
                        {
                            // 外枠描画
                            g.DrawRectangle(frameScrollPen, frameRect);
                        }
                        else
                        {
                            // 外枠描画
                            g.DrawRectangle(frameNormalPen, frameRect);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// クロージング
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isClosing = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion



        #region メソッド
        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            // 既に翻訳範囲が指定されている場合
            if (TranslateInfo.TranslateRect.X != 0 &&
                TranslateInfo.TranslateRect.Y != 0 &&
                TranslateInfo.TranslateRect.Width != 0 &&
                TranslateInfo.TranslateRect.Height != 0)
            {
                scanRect = TranslateInfo.TranslateRect;
                if (TranslateInfo.FromRunCmd)
                {
                    // コマンド実行時は透明
                    Opacity = 0;
                }
                else
                {
                    // 通常は薄く表示
                    Opacity = 0.1;
                }

                doSave = true;
            }

            if (Directory.Exists("temp"))
            {
                Directory.Delete("temp", true);
            }

            Activate();
        }

        /// <summary>
        /// スキャンサイズ初期化
        /// </summary>
        private void InitScanSize()
        {
            scanRect.X = 0;
            scanRect.Y = 0;
            scanRect.Width = 0;
            scanRect.Height = 0;
        }

        /// <summary>
        /// メインループ(描画処理)
        /// </summary>
        private void MainLoop()
        {
            // FPS更新
            fps.UpdateFps();

            if (changeOcrEngine)
            {
                if (Fps.FpsCountList[0] < Fps.SEC)
                {
                    var selectedOcrEngine = string.Empty;
                    if (TranslateInfo.UseOcrEngine == 0)
                    {
                        selectedOcrEngine = "Windows10 OCR";
                    }
                    else
                    {
                        selectedOcrEngine = "Tesseract OCR";
                    }

                    var showMessage = selectedOcrEngine;
                    lblMessage.Visible = false;
                    lblMessage.Text = showMessage;
                    int x = CommonUtil.GetCenterX(0, Size.Width, lblMessage.Size.Width);
                    int y = CommonUtil.GetCenterY(0, Size.Height, lblMessage.Size.Height);
                    lblMessage.Location = new Point(x, y);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    changeMultiples = false;
                }
            }

            if (changeMultiples)
            {
                if (Fps.FpsCountList[0] < Fps.SEC)
                {
                    var showMessage = $"読取倍率：x{TranslateInfo.ReadMultiples}";
                    lblMessage.Visible = false;
                    lblMessage.Text = showMessage;
                    int x = CommonUtil.GetCenterX(0, Size.Width, lblMessage.Size.Width);
                    int y = CommonUtil.GetCenterY(0, Size.Height, lblMessage.Size.Height);
                    lblMessage.Location = new Point(x, y);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    changeMultiples = false;
                }
            }

            if (changeCaptureMode)
            {
                if (Fps.FpsCountList[0] < Fps.SEC)
                {
                    string showMessage;

                    if (scrollCapture)
                    {
                        showMessage = "キャプチャーモード\r\nスクロール";
                    }
                    else
                    {
                        showMessage = "キャプチャーモード\r\n通常";
                    }

                    lblMessage.Visible = false;
                    lblMessage.Text = showMessage;
                    int x = CommonUtil.GetCenterX(0, Size.Width, lblMessage.Size.Width);
                    int y = CommonUtil.GetCenterY(0, Size.Height, lblMessage.Size.Height);
                    lblMessage.Location = new Point(x, y);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    changeCaptureMode = false;
                }
            }

            // OCR読み込み成否チェック
            if (ocrFailed)
            {
                if (Fps.FpsCountList[0] < Fps.SEC * 2)
                {
                    var showMessage = "OCR読み込み失敗";
                    lblMessage.Text = showMessage;
                    int x = CommonUtil.GetCenterX(0, Size.Width, lblMessage.Size.Width);
                    int y = CommonUtil.GetCenterY(0, Size.Height, lblMessage.Size.Height);
                    lblMessage.Location = new Point(x, y);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    ocrFailed = false;
                }
            }

            // 画像マージ成否チェック
            if (imgMergeFailed)
            {
                if (Fps.FpsCountList[0] < Fps.SEC * 2)
                {
                    var showMessage = "画像の連結に失敗";
                    lblMessage.Text = showMessage;
                    int x = CommonUtil.GetCenterX(0, Size.Width, lblMessage.Size.Width);
                    int y = CommonUtil.GetCenterY(0, Size.Height, lblMessage.Size.Height);
                    lblMessage.Location = new Point(x, y);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    imgMergeFailed = false;
                }
            }

            if (endScreenShot)
            {
                var showMessage = "画像処理中...";
                lblMessage.Text = showMessage;
                int x = CommonUtil.GetCenterX(0, Size.Width, lblMessage.Size.Width);
                int y = CommonUtil.GetCenterY(0, Size.Height, lblMessage.Size.Height);
                lblMessage.Location = new Point(x, y);
                lblMessage.Visible = true;
                lblMessage.Refresh();
            }

            if (scrollCapture)
            {
                if (endScreenShot)
                {
                    try
                    {
                        SaveMergeImage();
                    }
                    catch
                    {
                        var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                        if (prcs != null)
                        {
                            prcs.Threads.Resume();
                        }
                        throw;
                    }
                }
                else
                {
                    if (doScreenShot && Fps.FpsCountList[SS_IDX] > Fps.SEC / 2)
                    {
                        if (Util.GetCurrentProcessName() != ProcessNameInfo.SelectedProcessName)
                        {
                            var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                            if (prcs != null)
                            {
                                var hndl = UseWinApi.GetWindowHandle(prcs);
                                UseWinApi.WakeupWindow(hndl);
                                prcs.Threads.Resume();
                            }
                        }

                        GetScreenShot();
                        Fps.FpsCountList[SS_IDX] = 0;
                    }
                }
            }
            else
            {
                // 表示しきったところで画面をキャプチャー
                SaveScanRangeCapture();
            }

            // FPS待機
            fps.WaitFps();
        }

        /// <summary>
        /// 画面のキャプチャー
        /// </summary>
        private void SaveScanRangeCapture()
        {
            try
            {
                if (scanRect.X != 0 && scanRect.Y != 0 && scanRect.Width != 0 && scanRect.Height != 0 && doSave)
                {
                    if (scanRect.Width < 0)
                    {
                        // 負数の場合の範囲変換
                        int backW = scanRect.Width;
                        scanRect.Width = Math.Abs(scanRect.Width);
                        scanRect.X = scanRect.X + backW;
                    }

                    if (scanRect.Height < 0)
                    {
                        // 負数の場合の範囲変換
                        int backH = scanRect.Height;
                        scanRect.Height = Math.Abs(scanRect.Height);
                        scanRect.Y = scanRect.Y + backH;
                    }

                    Rectangle rect;

                    // 一旦フォーム全体を範囲に含める
                    rect = Bounds;

                    int dpi = 0;
                    float dpiFactor = 0f;
                    UseWinApi.GetDpiInfo(ref dpi, ref dpiFactor);

                    // オフセット座標に補正
                    if (dpiFactor > 1)
                    {
                        rect.X = Convert.ToInt32(rect.X * dpiFactor);
                        rect.Y = Convert.ToInt32(rect.Y * dpiFactor);
                        rect.Width = Convert.ToInt32(rect.Width * dpiFactor);
                        rect.Height = Convert.ToInt32(rect.Height * dpiFactor);
                        rect.X += Convert.ToInt32(scanRect.X * dpiFactor);
                        rect.Y += Convert.ToInt32(scanRect.Y * dpiFactor);
                        rect.Width = Convert.ToInt32(scanRect.Width * dpiFactor);
                        rect.Height = Convert.ToInt32(scanRect.Height * dpiFactor);
                    }
                    else
                    {
                        rect.X += scanRect.X;
                        rect.Y += scanRect.Y;
                        rect.Width = scanRect.Width;
                        rect.Height = scanRect.Height;
                    }

                    Bitmap bmp = new Bitmap(rect.Width, rect.Height);
                    Bitmap resizeBmp;
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

                        resizeBmp = Util.ResizeBitmap(
                            bmp, Convert.ToInt32(bmp.Width * TranslateInfo.ReadMultiples),
                            Convert.ToInt32(bmp.Height * TranslateInfo.ReadMultiples),
                            System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor);
                    }

                    // OCR読み取り
                    string readText = string.Empty;

                    try
                    {
                        if (TranslateInfo.UseOcrEngine == 0)
                        {
                            if (!TranslateInfo.IsFreeSelect)
                            {
                                Thread.Sleep(100);
                            }
                            var ocrResult = ReadWin10Ocr(ref resizeBmp);
                            readText = ProcessingOcrResultForWin10Ocr(ocrResult);
                            readText = ProcessingOcrResultText(readText);

                            // OCRに失敗した場合でも画像は必ず残す
                            string filePath = "screen.png";
                            resizeBmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                            if (string.IsNullOrEmpty(readText.Trim()) || readText.Trim() ==
                                Environment.NewLine)
                            {
                                // OCR読み込み失敗
                                InitScanSize();

                                if (!TranslateInfo.FromRunCmd)
                                {
                                    Fps.FpsCountList[0] = 0;
                                    ocrFailed = true;
                                    doSave = false;
                                    if (Opacity == 0.1)
                                    {
                                        Opacity = 0.6;
                                    }

                                    return;
                                }
                            }

                            Opacity = 0;
                        }
                        else
                        {
                            readText = ReadOcr(ref resizeBmp);

                            readText = readText.Replace("\n", Environment.NewLine);
                            readText = readText.Replace("\r\n ", Environment.NewLine);
                            readText = readText.Replace("\r\n\r\n\r\n", Environment.NewLine);

                            // OCRに失敗した場合でも画像は必ず残す
                            string filePath = "screen.png";
                            resizeBmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                            if (string.IsNullOrEmpty(readText.Trim()) || readText.Trim() ==
                                Environment.NewLine)
                            {
                                // OCR読み込み失敗
                                InitScanSize();

                                if (!TranslateInfo.FromRunCmd)
                                {
                                    Fps.FpsCountList[0] = 0;
                                    ocrFailed = true;
                                    doSave = false;
                                    if (Opacity == 0.1)
                                    {
                                        Opacity = 0.6;
                                    }

                                    return;
                                }
                            }

                            Opacity = 0;

                            readText = ProcessingOcrResultText(readText);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.InnerException.Message);
                        }
                        else
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
                        }

                        InitScanSize();
                        doSave = false;
                        if (Opacity == 0.1)
                        {
                            Opacity = 0.6;
                        }
                        return;
                    }

                    // 原文
                    TranslateInfo.OriginalText = readText;
                    // 翻訳範囲
                    TranslateInfo.TranslateRect = scanRect;

                    // 全て処理が完了した場合は閉じる
                    TranslateInfo.ShowScanForm = false;
                    Close();
                }
            }
            catch
            {
                var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                if (prcs != null)
                {
                    prcs.Threads.Resume();
                }
                throw;
            }
        }

        /// <summary>
        /// スクリーンショット取得
        /// </summary>
        private void GetScreenShot()
        {
            if (!Directory.Exists("temp"))
            {
                Directory.CreateDirectory("temp");
            }

            if (scanRect.Width < 0)
            {
                // 負数の場合の範囲変換
                int backW = scanRect.Width;
                scanRect.Width = Math.Abs(scanRect.Width);
                scanRect.X += backW;
            }

            if (scanRect.Height < 0)
            {
                // 負数の場合の範囲変換
                int backH = scanRect.Height;
                scanRect.Height = Math.Abs(scanRect.Height);
                scanRect.Y += backH;
            }

            Rectangle rect;

            // 一旦フォーム全体を範囲に含める
            rect = Bounds;

            int dpi = 0;
            float dpiFactor = 0f;
            UseWinApi.GetDpiInfo(ref dpi, ref dpiFactor);

            // オフセット座標に補正
            if (dpiFactor > 1)
            {
                rect.X = Convert.ToInt32(rect.X * dpiFactor);
                rect.Y = Convert.ToInt32(rect.Y * dpiFactor);
                rect.Width = Convert.ToInt32(rect.Width * dpiFactor);
                rect.Height = Convert.ToInt32(rect.Height * dpiFactor);
                rect.X += Convert.ToInt32(scanRect.X * dpiFactor);
                rect.Y += Convert.ToInt32(scanRect.Y * dpiFactor);
                rect.Width = Convert.ToInt32(scanRect.Width * dpiFactor);
                rect.Height = Convert.ToInt32(scanRect.Height * dpiFactor);
            }
            else
            {
                rect.X += scanRect.X;
                rect.Y += scanRect.Y;
                rect.Width = scanRect.Width;
                rect.Height = scanRect.Height;
            }

            Bitmap resizeBmp;
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

                    resizeBmp = Util.ResizeBitmap(
                        bmp, Convert.ToInt32(bmp.Width * TranslateInfo.ReadMultiples),
                        Convert.ToInt32(bmp.Height * TranslateInfo.ReadMultiples),
                        System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor);

                    // 4色に減色
                    resizeBmp = Util.ConvertTo24bpp(resizeBmp);
                    Util.ConvertTo4Color(resizeBmp);

                    string filePath = $@"temp\{sCounter:000}.png";
                    resizeBmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                    resizeBmp.Dispose();

                    sCounter++;
                }
            }
        }

        /// <summary>
        /// 画像のマージ
        /// </summary>
        private void SaveMergeImage()
        {
            Bitmap resultBmp = null;

            try
            {
                var imgFiles = Directory.GetFileSystemEntries("temp", "*.png");
                int offset = 0;
                const float RANGE = 0.5f;

                for (int i = 0; i < imgFiles.Length - 1; i++)
                {
                    bool isMatch = false;
                    using (Bitmap org1 = new Bitmap(imgFiles[i]))
                    using (Bitmap org2 = new Bitmap(imgFiles[i + 1]))
                    {
                        // 画像の比較
                        for (int j = 0; j < Convert.ToInt32(org1.Height * RANGE); j++)
                        {
                            // 画像をトリミング
                            Rectangle rect1 = new Rectangle(0, j, org1.Width, Convert.ToInt32(org1.Height * RANGE));
                            Rectangle rect2 = new Rectangle(0, 0, org2.Width, Convert.ToInt32(org2.Height * RANGE));
                            using (var resizeEdt1 = org1.Clone(rect1, org1.PixelFormat))
                            using (var resizeEdt2 = org2.Clone(rect2, org2.PixelFormat))
                            {
                                if (Util.ImageCompare(resizeEdt1, resizeEdt2))
                                {
                                    // 同じ画像だった場合は統合
                                    isMatch = true;
                                    Bitmap newBmp;
                                    offset += j;

                                    if (resultBmp == null)
                                    {
                                        resultBmp = new Bitmap(org1, org1.Width, org1.Height);
                                    }

                                    newBmp = new Bitmap(resultBmp.Width, resultBmp.Height + j);

                                    using (Graphics g = Graphics.FromImage(newBmp))
                                    {
                                        g.DrawImage(resultBmp, 0, 0);
                                        g.DrawImage(org2, 0, offset);
                                        resultBmp = (Bitmap)newBmp.Clone();
                                    }

                                    newBmp.Dispose();
                                    break;
                                }
                            }
                        }

                        if (!isMatch)
                        {
                            scrollCapture = false;
                            endScreenShot = false;
                            doSave = false;

                            if (resultBmp != null)
                            {
                                resultBmp.Dispose();
                            }

                            InitScanSize();
                            if (Opacity == 0.1)
                            {
                                Opacity = 0.6;
                            }

                            Fps.FpsCountList[0] = 0;
                            imgMergeFailed = true;
                            return;
                        }
                    }
                }

                // OCR読み取り
                string readText = string.Empty;

                try
                {
                    if (TranslateInfo.UseOcrEngine == 0)
                    {
                        var ocrResult = ReadWin10Ocr(ref resultBmp);
                        readText = ProcessingOcrResultForWin10Ocr(ocrResult);
                        readText = ProcessingOcrResultText(readText);

                        // OCRに失敗した場合でも画像は必ず残す
                        string filePath = "screen.png";
                        resultBmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                        if (string.IsNullOrEmpty(readText.Trim()) || readText.Trim() ==
                            Environment.NewLine)
                        {
                            // OCR読み込み失敗
                            InitScanSize();
                            Fps.FpsCountList[0] = 0;
                            ocrFailed = true;
                            doSave = false;
                            if (Opacity == 0.1)
                            {
                                Opacity = 0.6;
                            }

                            return;
                        }
                    }
                    else
                    {
                        readText = ReadOcr(ref resultBmp);
                        readText = readText.Replace("\n", Environment.NewLine);
                        readText = readText.Replace("\r\n ", Environment.NewLine);
                        readText = readText.Replace("\r\n\r\n\r\n", Environment.NewLine);

                        // OCRに失敗した場合でも画像は必ず残す
                        string filePath = "screen.png";
                        resultBmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                        if (string.IsNullOrEmpty(readText.Trim()) || readText.Trim() == Environment.NewLine)
                        {
                            // OCR読み込み失敗
                            scrollCapture = false;
                            endScreenShot = false;
                            doSave = false;
                            InitScanSize();
                            Fps.FpsCountList[0] = 0;
                            ocrFailed = true;
                            if (Opacity == 0.1)
                            {
                                Opacity = 0.6;
                            }

                            return;
                        }

                        readText = ProcessingOcrResultText(readText);
                    }
                }
                catch (Exception ex)
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);

                    scrollCapture = false;
                    endScreenShot = false;
                    doSave = false;

                    InitScanSize();
                    if (Opacity == 0.1)
                    {
                        Opacity = 0.6;
                    }
                    return;
                }

                // 原文
                TranslateInfo.OriginalText = readText;
                // 翻訳範囲
                TranslateInfo.TranslateRect = new Rectangle();

                // 全て処理が完了した場合は閉じる
                TranslateInfo.ShowScanForm = false;
                Close();
            }
            finally
            {
                if (resultBmp != null)
                {
                    resultBmp.Dispose();
                }

                // tempフォルダを中のファイルごと全て削除
                Directory.Delete("temp", true);
            }
        }

        /// <summary>
        /// OCR読み取り
        /// </summary>
        /// <param name="bitmap">ビットマップイメージ</param>
        /// <returns>OCRの結果オブジェクト</returns>
        private string ReadOcr(ref Bitmap bitmap)
        {
            string langPath = "tessdata";
            string lngStr = "eng";
            string ret = string.Empty;

            BitmapToPixConverter toPix = new BitmapToPixConverter();
            using (var pix = toPix.Convert(bitmap).ConvertRGBToGray())
            using (var tesseract = new TesseractEngine(langPath, lngStr))
            {
                // OCRの実行
                tesseract.SetVariable("tessedit_char_blacklist", "|");

                // 行テキスト格納用リスト
                List<string> lineTextList = new List<string>();

                using (var page = tesseract.Process(pix, PageSegMode.Auto))
                using (var iterator = page.GetIterator())
                {
                    int prevY2 = 0;
                    int cnt = 0;

                    iterator.Begin();

                    do
                    {
                        if (iterator.TryGetBoundingBox(PageIteratorLevel.TextLine, out var rect))
                        {
                            var lineText = iterator.GetText(PageIteratorLevel.TextLine);

                            if (prevY2 > 0)
                            {
                                if ((rect.Y1 - prevY2) < rect.Height * 1.1)
                                {
                                    if ((cnt - 1) >= 0)
                                    {
                                        lineTextList[cnt - 1] = lineTextList[cnt - 1].Replace("\n\n", "\n");
                                    }
                                }
                                else
                                {
                                    if (!lineTextList[cnt - 1].Contains("\n\n"))
                                    {
                                        lineTextList[cnt - 1] = lineTextList[cnt - 1].Replace("\n", "\n\n");
                                    }
                                }
                            }

                            if (lineText == "\n\n" || lineText == "\n")
                            {
                                prevY2 = 0;
                            }
                            else
                            {
                                prevY2 = rect.Y2;
                            }

                            lineTextList.Add(lineText);

                            cnt++;
                        }
                    } while (iterator.Next(PageIteratorLevel.TextLine));
                }

                foreach (var text in lineTextList.ToArray())
                {
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        lineTextList.Remove(text);
                    }
                }

                ret = string.Join("", lineTextList);

                PixToBitmapConverter toBmp = new PixToBitmapConverter();
                bitmap = toBmp.Convert(pix);
            }

            return ret;
        }

        /// <summary>
        /// Windows10 OCR読取
        /// </summary>
        /// <param name="bitmap">ビットマップイメージ</param>
        /// <returns></returns>
        private OcrResult ReadWin10Ocr(ref Bitmap bitmap)
        {
            bitmap = Util.CreateGrayscaleImage(bitmap);

            try
            {
                var workBmp = bitmap;
                var sftBmp = Task.Run(() => Util.GetSoftwareBitmap(workBmp)).Result;
                var ocrResult = Task.Run(() => RunWin10Ocr(sftBmp)).Result;
                return ocrResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// OCR実行
        /// </summary>
        /// <param name="bitmap">ソフトウェアビットマップ</param>
        /// <returns>OCRの結果オブジェクトタスク</returns>
        private async Task<OcrResult> RunWin10Ocr(SoftwareBitmap bitmap)
        {
            var osVer = Environment.OSVersion;
            Language language = new Language("en");

            if (osVer.Version.Major != 10 && osVer.Version.Build < 15063)
            {
                throw new Exception("ご使用のWindowsではWindows10 OCRがサポートされていません。");
            }

            var ocrEngine = OcrEngine.TryCreateFromLanguage(language);
            if (ocrEngine == null)
            {
                throw new Exception("Windows言語パック(英語)がインストールされていません。\nシステム設定を確認してください。");
            }

            var ocrResult = await ocrEngine.RecognizeAsync(bitmap);
            return ocrResult;
        }

        /// <summary>
        /// OCR読取で得られたテキストを加工する
        /// </summary>
        /// <param name="readText">加工前テキスト</param>
        /// <returns>加工後テキスト</returns>
        private string ProcessingOcrResultText(string readText)
        {
            if (TranslateInfo.ToIgnoreReturn)
            {
                // 改行文字をスペースに置き換える
                readText = Util.ConvertReturnToSpace(readText);
            }

            if (!TranslateInfo.ToReturnOrginal)
            {
                // 改行単位で分割
                var readList = readText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 0; i < readList.Length; i++)
                {
                    // 念のため全ての行をトリム
                    readList[i] = readList[i].Trim();

                    if (string.IsNullOrEmpty(readList[i]))
                    {
                        // ブランクの場合は改行を2つ付与
                        readList[i] = "\r\n\r\n";
                    }
                }

                // 空白区切りで連結
                readText = string.Join(" ", readList);
                // 改行をトリム
                readText = readText.Replace(" \r\n\r\n ", "\r\n\r\n").Trim();
            }

            // 文章単位で全て大文字の場合は小文字に変換
            bool isConverted = false;
            var matchesLine = Regex.Matches(readText, "[a-zA-Z]");
            if (matchesLine.Cast<Match>().All(a => char.IsUpper(a.Value[0])))
            {
                readText = readText.ToLower();
                isConverted = true;
            }

            // 単語単位で大文字の場合は小文字に変換
            if (!isConverted)
            {
                // 変換がなかった場合のみ精査する
                var splitSpace = readText.Split(' ');
                for (int i = 0; i < splitSpace.Length; i++)
                {
                    // 改行単位で分割
                    var splitReturn = splitSpace[i]
                        .Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    for (int j = 0; j < splitReturn.Length; j++)
                    {
                        // アルファベットのみ抽出
                        var matches = Regex.Matches(splitReturn[j], "[a-zA-Z]");
                        var values = matches.Cast<Match>().Select(s => s.Value).ToArray();

                        // 一文字だけは対象としない
                        if (values.Length == 1)
                        {
                            continue;
                        }

                        // 先頭以外で大文字が存在した場合、小文字に変換
                        if (values.Where((w, index) => index != 0 && char.IsUpper(w[0])).Count() > 0)
                        {
                            // 小文字に変換
                            splitReturn[j] = splitReturn[j].ToLower();
                        }
                    }

                    splitSpace[i] = string.Join("\r\n", splitReturn);
                }

                readText = string.Join(" ", splitSpace);
            }

            // コンソール出力
            Console.WriteLine(readText);

            return readText;
        }

        /// <summary>
        /// OCR読取で得られたテキストを加工する
        /// </summary>
        /// <param name="ocrResult">OCRの結果</param>
        /// <returns>加工後テキスト</returns>
        private string ProcessingOcrResultForWin10Ocr(OcrResult ocrResult)
        {
            if (TranslateInfo.ToIgnoreReturn)
            {
                return ocrResult.Text;
            }

            string readText = string.Empty;

            if (ocrResult.Lines.Count > 1)
            {
                // 2行以上ある場合
                List<string> connect = new List<string>();
                for (int i = 0; i < ocrResult.Lines.Count - 1; i++)
                {
                    var word1 = ocrResult.Lines[i].Words
                        .Where(w => w.BoundingRect.Y ==
                        ocrResult.Lines[i].Words
                        .Select(s => s.BoundingRect.Y).Min()
                        ).FirstOrDefault();

                    var word2 = ocrResult.Lines[i + 1].Words
                        .Where(w => w.BoundingRect.Y ==
                        ocrResult.Lines[i + 1].Words
                        .Select(s => s.BoundingRect.Y).Min()
                        ).FirstOrDefault();

                    var wordHeight = ocrResult.Lines[i].Words
                        .Where(w =>
                        w.BoundingRect.Height ==
                        ocrResult.Lines[i].Words.Select(s => s.BoundingRect.Height).Max()).FirstOrDefault();

                    if (word1.BoundingRect.Y + wordHeight.BoundingRect.Height * 2.2 >= word2.BoundingRect.Y)
                    {
                        if (TranslateInfo.ToReturnOrginal)
                        {
                            connect.Add("\r\n");
                        }
                        else
                        {
                            connect.Add(" ");
                        }
                    }
                    else
                    {
                        connect.Add("\r\n\r\n");
                    }
                }

                for (int i = 0; i < ocrResult.Lines.Count; i++)
                {
                    if (i == 0)
                    {
                        readText = ocrResult.Lines[i].Text;
                    }
                    else
                    {
                        readText += connect[i - 1] + ocrResult.Lines[i].Text;
                    }
                }
            }
            else
            {
                // 1行あるいは読取失敗
                readText = ocrResult.Text;
            }

            return readText;
        }
        #endregion
    }
}
