using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmImageList : Form
    {
        #region グローバルクラス
        /// <summary>
        /// 画面の状態を保存
        /// </summary>
        public class RestoreWindowStateInfo
        {
            #region プロパティ
            /// <summary>画面状態</summary>
            public static FormWindowState RestoreWindowState { get; set; }
            /// <summary>画面の位置</summary>
            public static Point RestoreWindowLocation { get; set; }
            /// <summary>画面のサイズ</summary>
            public static Size RestoreWindowSize { get; set; }
            /// <summary>選択中の画像</summary>
            public static int RestoreImgIndex { get; set; }
            /// <summary>倍率その他</summary>
            public static Matrix RestoreMatrix { get; set; } = new Matrix();
            /// <summary>補間モード</summary>
            public static InterpolationMode RestoreInterpolationMode { get; set; }
            /// <summary>最小倍率</summary>
            public static float RestoreMinScale { get; set; }
            /// <summary>翻訳範囲</summary>
            public static Rectangle RestoreScanRect { get; set; }
            /// <summary>対象通り改行</summary>
            public static bool RestoreReturnOriginal { get; set; }
            /// <summary>改行を無視</summary>
            public static bool RestoreIgnoreReturn { get; set; }
            /// <summary>画像ファイルリスト</summary>
            public static List<ImgFile> RestoreImgFileList { get; set; } = new List<ImgFile>();
            /// <summary>自動再生速度</summary>
            public static int RestoreAutoPlaySpeed { get; set; }
            /// <summary>更新フラグ</summary>
            public static bool UpdateFlg { get; set; }
            #endregion

            #region メソッド
            /// <summary>
            /// 初期化
            /// </summary>
            public static void Reset()
            {
                RestoreWindowState = FormWindowState.Normal;
                RestoreWindowLocation = new Point(0, 0);
                RestoreWindowSize = new Size(0, 0);
                RestoreImgIndex = 0;
                RestoreMatrix = new Matrix();
                RestoreInterpolationMode = InterpolationMode.HighQualityBicubic;
                RestoreMinScale = 1f;
                RestoreScanRect = new Rectangle();
                RestoreReturnOriginal = false;
                RestoreIgnoreReturn = false;
                RestoreImgFileList = new List<ImgFile>();
                RestoreAutoPlaySpeed = 1000;
                UpdateFlg = false;
            }

            /// <summary>
            /// 画面状態を退避
            /// </summary>
            /// <param name="winState">画面の状態</param>
            /// <param name="winLocation">画面の位置</param>
            /// <param name="winSize">画面のサイズ</param>
            /// <param name="imgIndex">選択中の画像</param>
            /// <param name="mat">アフィン変換行列</param>
            /// <param name="interpolationMode">補間モード</param>
            /// <param name="minScale">最小倍率</param>
            /// <param name="scanRect">翻訳範囲</param>
            /// <param name="returnOriginal">対象通り改行</param>
            /// <param name="ignoreReturn">改行を無視</param>
            /// <param name="imgFiles">画像ファイルリスト</param>
            /// <param name="autoPlaySpeed">自動再生速度</param>
            public static void SaveRestoreWindowInfo(
                FormWindowState winState, Point winLocation, Size winSize,
                int imgIndex, Matrix mat, InterpolationMode interpolationMode,
                float minScale, Rectangle scanRect, bool returnOriginal, bool ignoreReturn,
                List<ImgFile> imgFiles, int autoPlaySpeed)
            {
                RestoreWindowState = winState;
                RestoreWindowLocation = winLocation;
                RestoreWindowSize = winSize;
                RestoreImgIndex = imgIndex;
                RestoreMatrix = mat.Clone();
                RestoreInterpolationMode = interpolationMode;
                RestoreMinScale = minScale;
                RestoreScanRect = scanRect;
                RestoreReturnOriginal = returnOriginal;
                RestoreIgnoreReturn = ignoreReturn;
                RestoreImgFileList = imgFiles;
                RestoreAutoPlaySpeed = autoPlaySpeed;

                UpdateFlg = true;
            }
            #endregion
        }
        #endregion

        #region ローカルクラス
        /// <summary>
        /// 画像ファイルパス
        /// </summary>
        public class ImgFile
        {
            #region プロパティ
            /// <summary>表示/非表示フラグ</summary>
            public bool ShowImage { get; set; }
            /// <summary>画像ファイルパス</summary>
            public string ImgFilePath { get; set; }
            #endregion
        }
        #endregion

        #region 定数
        private const string TITLE_STR = "画像翻訳：{0}";
        /// <summary>ズーム単位</summary>
        private const float ZOOM_UNIT = 1.1f;
        /// <summary>ズーム最大</summary>
        private const float ZOOM_MAX = 15f;

        /// <summary>
        /// サイズ超過区分
        /// </summary>
        private enum SizeOverType
        {
            Both,
            Width,
            Height,
            None
        }
        #endregion

        #region 変数
        /// <summary>プロセス名</summary>
        public string ProcessName = string.Empty;
        /// <summary>翻訳実行フラグ</summary>
        public bool RunTranslate = false;
        /// <summary>現在表示している画像のインデックス</summary>
        private int currentIdx = 0;
        /// <summary>画像ファイルリスト</summary>
        private List<ImgFile> imgFileList = new List<ImgFile>();
        /// <summary>画像ファイルリスト(全件)</summary>
        private List<ImgFile> imgFileListOfAll = new List<ImgFile>();
        /// <summary>描画用Graphicsオブジェクト</summary>
        private Graphics graphics = null;
        /// <summary>描画用ビットマップ</summary>
        private Bitmap srcBmp = null;
        /// <summary>補間モード</summary>
        private InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic;
        /// <summary>マウスダウンフラグ</summary>
        private bool isMouseDown = false;
        /// <summary>マウスをクリックした位置の保持用</summary>
        private PointF oldPoint;
        /// <summary>アフィン変換行列</summary>
        private Matrix matAffine = new Matrix();
        /// <summary>最小倍率</summary>
        private float minScale = 1f;
        /// <summary>倍率変更フラグ</summary>
        private bool isScaleChanged = false;
        /// <summary>翻訳範囲</summary>
        private Rectangle scanRect = new Rectangle();
        /// <summary>左右キー押下フラグ</summary>
        private bool isLeftRightDown = false;
        /// <summary>自動再生フラグ</summary>
        private bool isAutoPlay = false;
        /// <summary>再生速度</summary>
        private int playSpeed = 1000;
        /// <summary>簡易コマンド画面が閉じた場合</summary>
        private bool isSimpleCmdHide = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmImageList()
        {
            InitializeComponent();
        }
        #endregion

        #region イベント
        /// <summary>スキャンフォームが閉じた</summary>
        public event EventHandler ScanFormClosed;
        #endregion

        #region イベントハンドラ
        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmImageList_Load(object sender, EventArgs e)
        {
            try
            {
                // マウスホイールイベントを追加
                picImg.MouseWheel += picImg_MouseWheel;

                // 画像情報を取得
                if (RestoreWindowStateInfo.UpdateFlg)
                {
                    imgFileListOfAll = RestoreWindowStateInfo.RestoreImgFileList;
                    MergeImgList();
                }
                else
                {
                    imgFileListOfAll = GetImageFileList();
                }

                imgFileList = GetImageFileList();

                lblImgCount.Text = $"1 / {imgFileList.Count}";
                Text = string.Format(
                    TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));

                // トラックバーの設定
                SetTrackBar();

                // リサイズイベントを強制的に実行（Graphicsオブジェクトの作成のため）
                frmImageList_Resize(null, null);

                // 補間モードの設定
                graphics.InterpolationMode = interpolationMode;

                // 初回表示
                InitDrawImage();
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
        private void frmImageList_Shown(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = null;
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
        private void picImg_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                {
                    // 拡大
                    if (matAffine.Elements[0] < ZOOM_MAX)  // X方向の倍率を代表してチェック
                    {
                        // ポインタの位置周りに拡大
                        ScaleAt(ref matAffine, ZOOM_UNIT, e.Location);
                        if (matAffine.Elements[0] > 1)
                        {
                            interpolationMode = InterpolationMode.NearestNeighbor;
                            graphics.InterpolationMode = interpolationMode;
                        }
                        isScaleChanged = true;
                    }
                }
                else
                {
                    // 縮小
                    if (matAffine.Elements[0] > minScale)  // X方向の倍率を代表してチェック
                    {
                        // ポインタの位置周りに縮小
                        ScaleAt(ref matAffine, 1.0f / ZOOM_UNIT, e.Location);
                        if (matAffine.Elements[0] < 1)
                        {
                            interpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.InterpolationMode = interpolationMode;
                        }
                    }
                    else
                    {
                        isScaleChanged = false;
                    }
                }

                // 画像の描画
                DrawImage();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picImg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ZoomFit(ref matAffine);
                DrawImage();
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
        private void picImg_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    // マウスをクリックした位置の記録
                    oldPoint.X = e.X;
                    oldPoint.Y = e.Y;
                    // マウスダウンフラグ
                    isMouseDown = true;
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
        private void picImg_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // マウスをクリックしながら移動中のとき
                if (isMouseDown == true)
                {
                    using (var srcBmp = new Bitmap(imgFileList[currentIdx].ImgFilePath))
                    {
                        float x = e.X - oldPoint.X;
                        float y = e.Y - oldPoint.Y;
                        float chkW = picImg.Width - srcBmp.Width * matAffine.Elements[0];
                        float chkH = picImg.Height - srcBmp.Height * matAffine.Elements[0];

                        // 移動制限
                        if (x + matAffine.OffsetX >= 0 || x + matAffine.OffsetX <= chkW)
                        {
                            x = 0;
                        }

                        if (y + matAffine.OffsetY >= 0 || y + matAffine.OffsetY <= chkH)
                        {
                            y = 0;
                        }

                        // 画像の移動
                        matAffine.Translate(x, y, MatrixOrder.Append);

                        // 画像の描画
                        DrawImage();

                        // ポインタ位置の保持
                        oldPoint.X = e.X;
                        oldPoint.Y = e.Y;
                    }
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
        private void picImg_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                // マウスダウンフラグ
                isMouseDown = false;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void frmImageList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (isLeftRightDown)
                {
                    isLeftRightDown = false;
                    return;
                }

                if (e.KeyCode == Keys.Escape)
                {
                    Close();
                    return;
                }

                if (e.KeyCode == Keys.Delete)
                {
                    if (imgFileList.Count == 1 && imgFileListOfAll.Count > 1)
                    {
                        CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                            "表示中の画像を削除すると非表示の画像を表示出来なくなる為、削除できません。");
                        return;
                    }

                    if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                        "表示中の画像を削除してもよろしいですか？") == DialogResult.No)
                    {
                        return;
                    }

                    var filePath = imgFileList[currentIdx].ImgFilePath;
                    DeleteImage(filePath);
                    foreach (var item in imgFileListOfAll.ToArray())
                    {
                        if (item.ImgFilePath == filePath)
                        {
                            imgFileListOfAll.Remove(item);
                        }
                    }

                    if (imgFileListOfAll.Count == 0)
                    {
                        Directory.Delete($@"image\{ProcessName}");
                        Close();
                        return;
                    }

                    currentIdx = CommonUtil.GetLimitValue(currentIdx, 0, imgFileList.Count - 1);
                    lblImgCount.Text = $"{currentIdx + 1} / {imgFileList.Count}";
                    Text = string.Format(
                        TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));
                    trkImgListBar.Maximum = imgFileList.Count;
                    trkImgListBar.Value = currentIdx + 1;

                    DrawImage();
                }

                if (e.Shift && e.KeyCode == Keys.Return)
                {
                    if (isAutoPlay)
                    {
                        return;
                    }

                    TranslateInfo.TranslateRect = scanRect;
                    RunScanForm();
                    return;
                }

                if (e.KeyCode == Keys.Return)
                {
                    if (isAutoPlay)
                    {
                        return;
                    }

                    TranslateInfo.Init();
                    RunScanForm();
                    return;
                }

                if (e.KeyCode == Keys.Space)
                {
                    if (!isAutoPlay)
                    {
                        lblSpeed.Text = $"{playSpeed}ms";
                        isAutoPlay = true;
                        pnlAutoPlay.Visible = true;
                        pnlControl.Enabled = false;
                    }
                    else
                    {
                        isAutoPlay = false;
                        pnlAutoPlay.Visible = false;
                        pnlControl.Enabled = true;
                    }

                    while (isAutoPlay)
                    {
                        await Task.Delay(playSpeed);

                        if (isAutoPlay)
                        {
                            int prevIdx = currentIdx;

                            // 次の画像を表示
                            if (currentIdx == imgFileList.Count - 1)
                            {
                                currentIdx = 0;
                            }
                            else
                            {
                                currentIdx++;
                            }

                            lblImgCount.Text = $"{currentIdx + 1} / {imgFileList.Count}";
                            Text = string.Format(
                                TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));

                            var currentBmp = new Bitmap(Util.GetImage(imgFileList[currentIdx].ImgFilePath));
                            var prevBmp = new Bitmap(Util.GetImage(imgFileList[prevIdx].ImgFilePath));

                            MergeImgList();

                            if (currentBmp.Width == prevBmp.Width && currentBmp.Height == prevBmp.Height)
                            {
                                RestoreWindowStateInfo.SaveRestoreWindowInfo(
                                    WindowState, Location, Size,
                                    currentIdx, matAffine, interpolationMode,
                                    minScale, scanRect, chkTargetReturn.Checked, chkIgnoreReturn.Checked,
                                    imgFileListOfAll, playSpeed);
                            }

                            InitDrawImage();
                            Refresh();
                        }
                    }
                }

                // 自動再生中の再生速度変更
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    if (isAutoPlay)
                    {
                        if (e.KeyCode == Keys.Up)
                        {
                            if (playSpeed == 1)
                            {
                                playSpeed = CommonUtil.GetLimitValue(playSpeed + 9, 1, 1000);
                            }
                            else
                            {
                                playSpeed = CommonUtil.GetLimitValue(playSpeed + 10, 1, 1000);
                            }
                        }
                        else
                        {
                            playSpeed = CommonUtil.GetLimitValue(playSpeed - 10, 1, 1000);
                        }

                        lblSpeed.Text = $"{playSpeed}ms";
                    }
                }

                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    if (isAutoPlay)
                    {
                        return;
                    }

                    int prevIdx = currentIdx;

                    if (e.KeyCode == Keys.Left)
                    {
                        // 前の画像を表示
                        if (currentIdx == 0)
                        {
                            currentIdx = imgFileList.Count - 1;
                        }
                        else
                        {
                            currentIdx--;
                        }
                    }
                    else
                    {
                        // 次の画像を表示
                        if (currentIdx == imgFileList.Count - 1)
                        {
                            currentIdx = 0;
                        }
                        else
                        {
                            currentIdx++;
                        }
                    }

                    if (prevIdx != currentIdx)
                    {
                        lblImgCount.Text = $"{currentIdx + 1} / {imgFileList.Count}";
                        Text = string.Format(
                            TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));

                        var currentBmp = new Bitmap(Util.GetImage(imgFileList[currentIdx].ImgFilePath));
                        var prevBmp = new Bitmap(Util.GetImage(imgFileList[prevIdx].ImgFilePath));

                        MergeImgList();

                        if (currentBmp.Width == prevBmp.Width && currentBmp.Height == prevBmp.Height)
                        {
                            RestoreWindowStateInfo.SaveRestoreWindowInfo(
                                WindowState, Location, Size,
                                currentIdx, matAffine, interpolationMode,
                                minScale, scanRect, chkTargetReturn.Checked, chkIgnoreReturn.Checked,
                                imgFileListOfAll, playSpeed);
                        }

                        InitDrawImage();
                        Refresh();
                    }

                    trkImgListBar.Value = currentIdx + 1;

                    isLeftRightDown = true;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// キーアップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmImageList_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                isLeftRightDown = false;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 画面リサイズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmImageList_Resize(object sender, EventArgs e)
        {
            try
            {
                if ((picImg.Width == 0) || (picImg.Height == 0)) return;

                // PictureBoxと同じ大きさのBitmapクラスを作成する。
                Bitmap bmpPicBox = new Bitmap(picImg.Width, picImg.Height);
                // 空のBitmapをPictureBoxのImageに指定する。
                picImg.Image = bmpPicBox;
                // Graphicsオブジェクトの作成(FromImageを使う)
                graphics = Graphics.FromImage(picImg.Image);
                graphics.InterpolationMode = interpolationMode;

                if (!isScaleChanged)
                {
                    var sizeOver = CheckSizeOver(1);
                    if (sizeOver == SizeOverType.None)
                    {
                        matAffine.Reset();
                        MoveCenter(ref matAffine, 1);
                        minScale = 1;
                    }
                    else
                    {
                        ZoomFit(ref matAffine);
                        minScale = matAffine.Elements[0];
                    }
                }
                else
                {
                    if (CheckSizeOver(matAffine.Elements[0]) == SizeOverType.None)
                    {
                        if (CheckSizeOver(1) == SizeOverType.None)
                        {
                            MoveCenter(ref matAffine, matAffine.Elements[0]);
                            minScale = 1f;
                        }
                        else
                        {
                            ZoomFit(ref matAffine);
                        }
                    }
                    else
                    {
                        MoveCorrectLocation(ref matAffine, matAffine.Elements[0]);
                        if (CheckSizeOver(1) == SizeOverType.None)
                        {
                            minScale = 1f;
                        }
                    }
                }

                // 画像の描画
                DrawImage();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// トラックバースクロール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkImgListBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = null;
                int prevIdx = currentIdx;
                currentIdx = trkImgListBar.Value - 1;

                lblImgCount.Text = $"{currentIdx + 1} / {imgFileList.Count}";
                Text = string.Format(
                    TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));

                var currentBmp = new Bitmap(Util.GetImage(imgFileList[currentIdx].ImgFilePath));
                var prevBmp = new Bitmap(Util.GetImage(imgFileList[prevIdx].ImgFilePath));

                MergeImgList();

                if (currentBmp.Width == prevBmp.Width && currentBmp.Height == prevBmp.Height)
                {
                    RestoreWindowStateInfo.SaveRestoreWindowInfo(
                        WindowState, Location, Size,
                        currentIdx, matAffine, interpolationMode,
                        minScale, scanRect, chkTargetReturn.Checked, chkIgnoreReturn.Checked,
                        imgFileListOfAll, playSpeed);
                }

                InitDrawImage();
                Refresh();

            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ファイル操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpeImgFile_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = null;
                using (var form = new frmOperationImgFIleList())
                {
                    DataTable dt = Util.CreateOperationImgDataTable();

                    foreach (var imgFile in imgFileListOfAll)
                    {
                        var dr = dt.NewRow();
                        dr[OperationImgColumns.Delete] = false;
                        dr[OperationImgColumns.ShowImg] = imgFile.ShowImage;
                        dr[OperationImgColumns.ImgFileName] = Path.GetFileName(imgFile.ImgFilePath);
                        dr[OperationImgColumns.ImgFilePath] = imgFile.ImgFilePath;
                        dt.Rows.Add(dr);
                    }

                    var selectedImg = imgFileListOfAll.ToArray()
                        .Where(w => w.ImgFilePath == imgFileList[currentIdx].ImgFilePath)
                        .FirstOrDefault();

                    int selectedIdx = currentIdx;
                    if (selectedImg != null)
                    {
                        selectedIdx = imgFileListOfAll.IndexOf(selectedImg);
                    }

                    form.ParentSelectedIdx = selectedIdx;
                    form.ImgListDt = dt;

                    form.ShowDialog();

                    if (form.IsChanged)
                    {
                        // 削除
                        foreach (var file in form.DeleteFileList)
                        {
                            // 画像ファイルの削除
                            DeleteImage(file);
                        }

                        // 全体件数の方も更新
                        foreach (var path in form.DeleteFileList)
                        {
                            var imgFile = imgFileListOfAll.Where(w => w.ImgFilePath == path).FirstOrDefault();
                            if (imgFile != null)
                            {
                                imgFileListOfAll.Remove(imgFile);
                            }
                        }

                        // 全体件数の方を見る
                        if (imgFileListOfAll.Count == 0)
                        {
                            // 全ての画像を削除した場合はフォルダも削除する
                            Directory.Delete($@"image\{ProcessName}");
                            if (!Util.IsExistsProcessData())
                            {
                                ProcessNameInfo.DeleteProcessName();
                            }
                            Close();
                            return;
                        }

                        // 編集結果を反映
                        List<ImgFile> resultImgList = new List<ImgFile>();
                        foreach (DataRow row in form.ImgListDt.Rows)
                        {
                            resultImgList.Add(new ImgFile
                            {
                                ImgFilePath = row[OperationImgColumns.ImgFilePath].ToString(),
                                ShowImage = (bool)row[OperationImgColumns.ShowImg]
                            });
                        }

                        // 全体件数の方はフラグのみ反映
                        foreach (var img in resultImgList)
                        {
                            imgFileListOfAll.Where(w => w.ImgFilePath == img.ImgFilePath)
                                .Select(s => s.ShowImage = img.ShowImage).ToList();
                        }

                        imgFileList = resultImgList.Where(w => w.ShowImage).ToList();

                        // インデックスを一番先頭に戻す
                        currentIdx = 0;

                        lblImgCount.Text = $"{currentIdx + 1} / {imgFileList.Count}";
                        Text = string.Format(
                            TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));

                        trkImgListBar.Maximum = imgFileList.Count;
                        trkImgListBar.Value = 1;

                        RestoreWindowStateInfo.Reset();
                        frmImageList_Resize(null, null);

                        InitDrawImage();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フリー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFreeSelect_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = null;
                // 読取倍率を退避
                TranslateInfo.Init();
                RunScanForm();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 翻訳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = null;
                TranslateInfo.TranslateRect = scanRect;
                RunScanForm();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 対象通り改行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTargetReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTargetReturn.Checked)
                {
                    chkIgnoreReturn.Checked = false;
                }

                ActiveControl = null;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 改行を無視
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIgnoreReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIgnoreReturn.Checked)
                {
                    chkTargetReturn.Checked = false;
                }

                ActiveControl = null;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = null;
                Close();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// スキャンフォームが閉じた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_Closed(object sender, EventArgs e)
        {
            try
            {
                TranslateInfo.ShowScanForm = false;

                var simpleCmd = Application.OpenForms.Cast<Form>()
                    .Where(w => w.Name == nameof(frmSimpleCmd))
                    .Select(s => (frmSimpleCmd)s)
                    .FirstOrDefault();

                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];

                if (!string.IsNullOrEmpty(TranslateInfo.OriginalText))
                {
                    RunTranslate = true;
                    scanRect = TranslateInfo.TranslateRect;
                    ScanFormClosed?.Invoke(this, EventArgs.Empty);

                    if ((bool)dr[SettingColumns.UseSimpleCmd])
                    {
                        var showTranslated = Application.OpenForms.Cast<Form>()
                            .Where(w => w.Name == nameof(frmShowTranslatedText))
                            .Select(s => (frmShowTranslatedText)s)
                            .FirstOrDefault();

                        if(simpleCmd != null)
                        {
                            simpleCmd.IsNotifyHide = true;
                        }
                    }
                }
                else
                {
                    if (simpleCmd != null && isSimpleCmdHide)
                    {
                        simpleCmd.Show();
                        isSimpleCmdHide = false;
                    }
                }

                ShowInTaskbar = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フォームを閉じるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmImageList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (isAutoPlay)
                {
                    isAutoPlay = false;
                }

                if (imgFileList.Count > 0)
                {
                    // 画面の状態を保持
                    MergeImgList();
                    RestoreWindowStateInfo.SaveRestoreWindowInfo(
                        WindowState, Location, Size, currentIdx,
                        matAffine, interpolationMode, minScale, scanRect,
                        chkTargetReturn.Checked, chkIgnoreReturn.Checked, imgFileListOfAll, playSpeed);
                }

                // 解放
                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// フォルダ配下の画像ファイルパスを取得
        /// </summary>
        private List<ImgFile> GetImageFileList()
        {
            List<ImgFile> imgFiles = new List<ImgFile>();
            var dirPath = $@"image\{ProcessName}";
            var files = Directory.GetFileSystemEntries(dirPath, "*.png", SearchOption.AllDirectories)
                .OrderBy(o => o).ToArray();

            foreach (var imgFile in files)
            {
                imgFiles.Add(new ImgFile { ShowImage = true, ImgFilePath = imgFile });
            }

            return imgFiles;
        }

        /// <summary>
        /// トラックバーの設定
        /// </summary>
        private void SetTrackBar()
        {
            // 最大値の設定(最小値は1)
            trkImgListBar.Maximum = imgFileList.Count;
            trkImgListBar.Value = currentIdx + 1;
        }

        /// <summary>
        /// 画像ファイル削除
        /// </summary>
        private void DeleteImage(string filePath)
        {
            if (imgFileList.Count == 1)
            {
                picImg.Image.Dispose();
            }

            if (srcBmp != null)
            {
                srcBmp.Dispose();
            }

            File.Delete(filePath);

            while (true)
            {
                if (!File.Exists(filePath))
                {
                    break;
                }
            }

            foreach (var img in imgFileList.ToArray())
            {
                if (img.ImgFilePath == filePath)
                {
                    imgFileList.Remove(img);
                }
            }
        }

        /// <summary>
        /// 中央表示
        /// </summary>
        /// <param name="mat">アフィン変換行列</param>
        private void MoveCenter(ref Matrix mat, float scale)
        {
            using (Bitmap srcBmp = new Bitmap(imgFileList[currentIdx].ImgFilePath))
            {
                int srcWidth = (int)(srcBmp.Width * scale);
                int srcHeight = (int)(srcBmp.Height * scale);
                int picWidth = picImg.Width;
                int picHeight = picImg.Height;
                float x = 0;
                float y = 0;

                float scaleBak = mat.Elements[0];
                mat = new Matrix();
                mat.Scale(scaleBak, scaleBak);

                if (picWidth > srcWidth && picHeight > srcHeight)
                {
                    x = CommonUtil.GetCenterX(0, picWidth, srcWidth);
                    y = CommonUtil.GetCenterY(0, picHeight, srcHeight);
                }
                else if (picWidth > srcWidth)
                {
                    x = CommonUtil.GetCenterX(0, picWidth, srcWidth);
                    y = -CommonUtil.GetCenterY(0, srcHeight, picHeight);
                }
                else if (picHeight > srcHeight)
                {
                    x = -CommonUtil.GetCenterX(0, srcWidth, picWidth);
                    y = CommonUtil.GetCenterY(0, picHeight, srcHeight);
                }
                else
                {
                    x = -CommonUtil.GetCenterX(0, srcWidth, picWidth);
                    y = -CommonUtil.GetCenterY(0, srcHeight, picHeight);
                }

                mat.Translate(x, y, MatrixOrder.Append);
            }
        }

        /// <summary>
        /// 画像をピクチャボックスのサイズに合わせて全体に表示するアフィン変換行列を求める
        /// </summary>
        /// <param name="mat">アフィン変換行列</param>
        private void ZoomFit(ref Matrix mat)
        {
            // アフィン変換行列の初期化（単位行列へ）
            mat.Reset();

            using (Bitmap srcBmp = new Bitmap(imgFileList[currentIdx].ImgFilePath))
            {
                int srcW = srcBmp.Width;
                int srcH = srcBmp.Height;
                int dstW = picImg.Width;
                int dstH = picImg.Height;

                float scale;

                // 縦に合わせるか？横に合わせるか？
                if (srcH * dstW > dstH * srcW)
                {
                    // ピクチャボックスの縦方法に画像表示を合わせる場合
                    scale = dstH / (float)srcH;
                    mat.Scale(scale, scale, MatrixOrder.Append);
                    // 中央へ平行移動
                    mat.Translate((dstW - srcW * scale) / 2f, 0f, MatrixOrder.Append);
                }
                else
                {
                    // ピクチャボックスの横方法に画像表示を合わせる場合
                    scale = dstW / (float)srcW;
                    mat.Scale(scale, scale, MatrixOrder.Append);
                    // 中央へ平行移動
                    mat.Translate(0f, (dstH - srcH * scale) / 2f, MatrixOrder.Append);
                }

                minScale = mat.Elements[0];
                isScaleChanged = false;
            }
        }

        /// <summary>
        /// 初回表示
        /// </summary>
        private void InitDrawImage()
        {
            matAffine = new Matrix();

            if (RestoreWindowStateInfo.UpdateFlg)
            {
                // 保持中の状態を復元
                WindowState = RestoreWindowStateInfo.RestoreWindowState;
                Location = RestoreWindowStateInfo.RestoreWindowLocation;
                Size = RestoreWindowStateInfo.RestoreWindowSize;
                currentIdx = CommonUtil.GetLimitValue(
                    RestoreWindowStateInfo.RestoreImgIndex, 0, imgFileList.Count - 1);
                matAffine = RestoreWindowStateInfo.RestoreMatrix.Clone();
                interpolationMode = RestoreWindowStateInfo.RestoreInterpolationMode;
                minScale = RestoreWindowStateInfo.RestoreMinScale;
                scanRect = RestoreWindowStateInfo.RestoreScanRect;
                chkTargetReturn.Checked = RestoreWindowStateInfo.RestoreReturnOriginal;
                chkIgnoreReturn.Checked = RestoreWindowStateInfo.RestoreIgnoreReturn;
                imgFileList = RestoreWindowStateInfo.RestoreImgFileList.Where(w => w.ShowImage).ToList();
                graphics.InterpolationMode = interpolationMode;
                lblImgCount.Text = $"{currentIdx + 1} / {imgFileList.Count}";
                Text = string.Format(
                    TITLE_STR, Path.GetFileName(imgFileList[currentIdx].ImgFilePath));
                trkImgListBar.Maximum = imgFileList.Count;
                trkImgListBar.Value = currentIdx + 1;
                playSpeed = RestoreWindowStateInfo.RestoreAutoPlaySpeed;

                // 復元を終えたらリセット
                RestoreWindowStateInfo.Reset();
            }
            else
            {
                if (CheckSizeOver(1) == SizeOverType.None)
                {
                    MoveCenter(ref matAffine, 1);
                }
                else
                {
                    ZoomFit(ref matAffine);
                }

                minScale = matAffine.Elements[0];
            }

            DrawImage();
        }

        /// <summary>
        /// 画像の表示位置を補正
        /// </summary>
        /// <param name="mat">アフィン変換行列</param>
        /// <param name="scale">倍率</param>
        private void MoveCorrectLocation(ref Matrix mat, float scale)
        {
            using (var srcBmp = new Bitmap(imgFileList[currentIdx].ImgFilePath))
            {
                float offsetX = mat.OffsetX;
                float offsetY = mat.OffsetY;
                float chkW = picImg.Width - srcBmp.Width * scale;
                float chkH = picImg.Height - srcBmp.Height * scale;

                if (CheckSizeOver(scale) == SizeOverType.Both)
                {
                    if (offsetX >= 0)
                    {
                        mat.Translate(-offsetX, 0f, MatrixOrder.Append);
                    }

                    if (offsetY >= 0)
                    {
                        mat.Translate(0f, -offsetY, MatrixOrder.Append);
                    }

                    if (offsetX <= chkW)
                    {
                        mat.Translate(chkW - offsetX, 0f, MatrixOrder.Append);
                    }

                    if (offsetY <= chkH)
                    {
                        mat.Translate(0f, chkH - offsetY, MatrixOrder.Append);
                    }
                }
                else
                {
                    MoveCenter(ref mat, scale);
                }
            }
        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        private void DrawImage()
        {
            graphics.Clear(picImg.BackColor);

            srcBmp = new Bitmap(Util.GetImage(imgFileList[currentIdx].ImgFilePath));

            RectangleF rect = new RectangleF(-0.5f, -0.5f, srcBmp.Width, srcBmp.Height);

            PointF[] points = new PointF[]
            {
                new PointF(rect.Left,rect.Top),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Left, rect.Bottom)
            };

            // 描画先の座標をアフィン変換で求める（左上、右上、左下の順）
            PointF[] destPoints = (PointF[])points.Clone();
            // 描画先の座標をアフィン変換で求める（変換後の座標は上書きされる）
            matAffine.TransformPoints(destPoints);

            // 描画
            graphics.DrawImage(
                srcBmp,
                destPoints,
                rect,
                GraphicsUnit.Pixel
                );

            // 再描画
            picImg.Refresh();
        }

        /// <summary>
        /// 指定した点（point）周りの拡大縮小
        /// </summary>
        /// <param name="scale">倍率</param>
        /// <param name="point">基準点の座標</param>
        private void ScaleAt(ref Matrix mat, float scale, PointF point)
        {
            SizeOverType sizeOver;
            if (scale > 1)
            {
                sizeOver = CheckSizeOver(matAffine.Elements[0]);
            }
            else
            {
                sizeOver = CheckSizeOver(matAffine.Elements[0] * scale);
            }

            switch (sizeOver)
            {
                case SizeOverType.Both:
                    // 原点へ移動
                    mat.Translate(-point.X, -point.Y, MatrixOrder.Append);

                    // 拡大縮小
                    mat.Scale(scale, scale, MatrixOrder.Append);

                    // 元へ戻す
                    mat.Translate(point.X, point.Y, MatrixOrder.Append);
                    break;
                default:
                    if (sizeOver != SizeOverType.None)
                    {
                        // 初期値へ移動
                        mat.Translate(-mat.OffsetX, -mat.OffsetY, MatrixOrder.Append);

                        // 拡大縮小
                        mat.Scale(scale, scale, MatrixOrder.Append);

                        // 中心へ移動
                        MoveCenter(ref matAffine, matAffine.Elements[0]);
                    }
                    else
                    {
                        if (scale < 1 && CheckSizeOver(1) != SizeOverType.None)
                        {
                            ZoomFit(ref matAffine);
                        }
                        else
                        {
                            // 初期値へ移動
                            mat.Translate(-mat.OffsetX, -mat.OffsetY, MatrixOrder.Append);

                            // 拡大縮小
                            mat.Scale(scale, scale, MatrixOrder.Append);

                            // 中心へ移動
                            MoveCenter(ref matAffine, matAffine.Elements[0]);
                        }
                    }
                    break;
            }

            MoveCorrectLocation(ref mat, mat.Elements[0]);
        }

        /// <summary>
        /// 描画領域よりもサイズが大きいかどうかをチェック
        /// </summary>
        /// <param name="scale">倍率</param>
        /// <returns>サイズ超過区分</returns>
        private SizeOverType CheckSizeOver(float scale)
        {
            using (Bitmap srcBmp = new Bitmap(imgFileList[currentIdx].ImgFilePath))
            {
                float currentW = srcBmp.Width * scale;
                float currentH = srcBmp.Height * scale;

                if (currentW > picImg.Width && currentH > picImg.Height)
                {
                    // 両方超過
                    return SizeOverType.Both;
                }
                else if (currentW > picImg.Width)
                {
                    // 幅だけ超過
                    return SizeOverType.Width;
                }
                else if (currentH > picImg.Height)
                {
                    // 高さだけ超過
                    return SizeOverType.Height;
                }
                else
                {
                    // 超過なし
                    return SizeOverType.None;
                }
            }
        }

        /// <summary>
        /// スキャンフォーム起動
        /// </summary>
        private void RunScanForm()
        {

            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
            if ((bool)dr[SettingColumns.UseSimpleCmd])
            {
                var showTranslated = Application.OpenForms.Cast<Form>()
                    .Where(w => w.Name == nameof(frmShowTranslatedText))
                    .Select(s => (frmShowTranslatedText)s)
                    .FirstOrDefault();

                var simpleCmd = Application.OpenForms.Cast<Form>()
                    .Where(w => w.Name == nameof(frmSimpleCmd))
                    .Select(s => (frmSimpleCmd)s)
                    .FirstOrDefault();

                if (showTranslated.IsHide)
                {
                    if (simpleCmd != null)
                    {
                        simpleCmd.Hide();
                        isSimpleCmdHide = true;
                    }
                }
            }

            TranslateInfo.IsFreeSelect = false;
            TranslateInfo.OriginalText = string.Empty;
            frmScan scan = new frmScan();
            Rectangle rect = new Rectangle();
            rect.X = Location.X;
            rect.Y = Location.Y;
            rect.Width = rect.X + Size.Width;
            rect.Height = rect.Y + Size.Height;

            scan.Rect = rect;
            scan.Disposed += frmScan_Closed;

            TranslateInfo.ToReturnOrginal = chkTargetReturn.Checked;
            TranslateInfo.ToIgnoreReturn = chkIgnoreReturn.Checked;
            TranslateInfo.FromImgTranslate = true;
            TranslateInfo.ShowScanForm = true;
            scan.Show();
        }

        /// <summary>
        /// 画像ファイルリストのマージ
        /// </summary>
        private void MergeImgList()
        {
            var workImgList = GetImageFileList();
            if (imgFileListOfAll.Count < workImgList.Count)
            {
                List<ImgFile> temp = new List<ImgFile>();
                for (int i = 0; i < workImgList.Count; i++)
                {
                    bool isExists = false;
                    for (int j = 0; j < imgFileListOfAll.Count; j++)
                    {
                        if (workImgList[i].ImgFilePath == imgFileListOfAll[j].ImgFilePath)
                        {
                            isExists = true;
                            break;
                        }
                    }

                    if (!isExists)
                    {
                        temp.Add(workImgList[i]);
                    }
                }

                imgFileListOfAll.AddRange(temp);

                // ソートして入れなおす
                var sorted = imgFileListOfAll.OrderBy(o => o.ImgFilePath).ToList();
                imgFileListOfAll = sorted;
            }
        }
        #endregion
    }
}
