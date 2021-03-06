using GameUtil;
using NHotkey;
using NHotkey.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace PCOT
{
    public partial class frmShowTranslatedText : Form
    {
        #region 定数
        /// <summary>シンプル表示時の横幅最小値</summary>
        private const int SHOW_SIMPLE_MIN_WIDTH = 325;
        /// <summary>中部コマンドの高さ</summary>
        private const int MIDDLE_CMD_HEIGHT = 25;
        /// <summary>接続色</summary>
        private readonly Color CONNECT_COLOR = Color.Green;
        /// <summary>接続背景色</summary>
        private readonly Color CONNECT_BACK_COLOR = Color.FromArgb(224, 254, 231);
        /// <summary>停止色</summary>
        private readonly Color STOPPED_COLOR = Color.OrangeRed;
        /// <summary>停止背景色</summary>
        private readonly Color STOPPED_BACK_COLOR = Color.FromArgb(255, 251, 176);
        /// <summary>Google翻訳WebAPI色</summary>
        private readonly Color GOOGLE_NORMAL_COLOR = Color.Blue;
        /// <summary>Google翻訳WebAPI背景色</summary>
        private readonly Color GOOGLE_NORMAL_BACK_COLOR = Color.FromArgb(198, 245, 255);
        /// <summary>Google翻訳GAS色</summary>
        private readonly Color GOOGLE_LOW_COLOR = Color.OrangeRed;
        /// <summary>Google翻訳GAS背景色</summary>
        private readonly Color GOOGLE_LOW_BACK_COLOR = Color.FromArgb(255, 251, 176);
        /// <summary>Google翻訳無効色</summary>
        private readonly Color GOOGLE_NOWORK_COLOR = Color.Red;
        /// <summary>Google翻訳無効背景色</summary>
        private readonly Color GOOGLE_NOWORK_BACK_COLOR = Color.Pink;
        /// <summary>DeepLのプロセス名</summary>
        private const string PROCESS_NAME_OF_DEEPL = "DeepL";
        /// <summary>画面タイトル</summary>
        private const string TITLE_NAME = "翻訳表示 - {0}";
        /// <summary>接続中</summary>
        private const string CONNECT_STR = "接続中：{0}";
        /// <summary>停止中</summary>
        private const string STOPPED_STR = "停止中：{0}";
        /// <summary>デスクトップ接続</summary>
        private const string CONNECT_DESKTOP_STR = "デスクトップ接続";
        /// <summary>Google翻訳：WebAPI</summary>
        private const string GOOGLE_WEBAPI_STR = "Google翻訳：WebAPI";
        /// <summary>Google翻訳：GAS</summary>
        private const string GOOGLE_LOW_STR = "Google翻訳：GoogleAppsScript";
        /// <summary>無効</summary>
        private const string GOOGLE_NOWORK_STR = "Google翻訳：無効";
        /// <summary>翻訳結果出力ファイル名</summary>
        private const string OUTPUT_RESULTFILE = "result.txt";
        /// <summary>接続解除</summary>
        private const string DISCONNECT_CAPTION_NAME = "接続解除";
        /// <summary>再接続</summary>
        private const string RECONNECT_CAPTION_NAME = "再接続";
        /// <summary>コマンドモード：ショートカット</summary>
        private const string CHANGE_SHORTCUT_NAME = "ショートカットに切替";
        /// <summary>コマンドモード：簡易コマンド画面</summary>
        private const string CHANGE_SIMPLECMD_NAME = "簡易コマンドに切替";
        /// <summary>コマンドファイル名</summary>
        private const string CMD_FILE_NAME = "cmd.txt";
        #endregion

        #region 変数
        /// <summary>非表示フラグ</summary>
        public bool IsHide = false;
        /// <summary>起動時エラーフラグ</summary>
        private bool isError = false;
        /// <summary>スキャン範囲更新フラグ</summary>
        private bool doUpdate = false;
        /// <summary>一つ前の原文</summary>
        private string preOriginalText = string.Empty;
        /// <summary>原文検索用(小文字)</summary>
        private string originalText = string.Empty;
        /// <summary>前に選択したインデックス</summary>
        private int prevIndex = 0;
        /// <summary>前に選択した訳文テキスト</summary>
        private string prevSelText = string.Empty;
        /// <summary>訳文選択フラグ</summary>
        private bool isSelectedResult = false;
        /// <summary>終了フラグ</summary>
        private bool isClose = false;
        /// <summary>親からアクティブになったフラグ</summary>
        private bool fromParentFlg = false;
        /// <summary>訳文選択テキスト</summary>
        private string resultSelectedText = string.Empty;
        /// <summary>簡易コマンド画面</summary>
        private frmSimpleCmd simpleCmd = new frmSimpleCmd();
        /// <summary>画像翻訳画面</summary>
        private frmImageList imageList = new frmImageList();
        /// <summary>保護フィルム</summary>
        private frmProtection protectionFilm = new frmProtection();
        /// <summary>翻訳表示画面を能動的に非表示</summary>
        private bool isActiveHideByCode = false;
        /// <summary>子画面表示フラグ</summary>
        private bool isShowChildForm = false;
        /// <summary>登録中フラグ</summary>
        private bool isRegistering = false;
        /// <summary>登録中フラグ</summary>
        private List<string> orgEditHistoryList = new List<string>();
        /// <summary>現在の原文編集インデックス</summary>
        private int currentEditIdx = 0;
        /// <summary>編集追加許可フラグ</summary>
        private bool isAllowAddEditText = true;
        /// <summary>画像翻訳画面表示フラグ</summary>
        private bool isShowImgTranslate = false;
        /// <summary>プロセス停止フラグ</summary>
        private bool isStoppedProcess = false;
        /// <summary>音声出力</summary>
        private SpeechSynthesizer speech = new SpeechSynthesizer();
        /// <summary>音声出力キャンセルフラグ</summary>
        private bool isCanceledSpeech = false;
        /// <summary>コマンド準備フラグ</summary>
        private bool isCmdReady = false;
        /// <summary>切断操作フラグ</summary>
        private bool isDisConnectOperation = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmShowTranslatedText()
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
        private void frmShowTranslatedText_Load(object sender, EventArgs e)
        {
            try
            {
                // プロセスの接続を監視
                timChkPrcs.Start();

                speech.SetOutputToDefaultAudioDevice();

                // 翻訳品質の確認
                string dummyOutput = string.Empty;
                SetTranslateQualityLabel(Util.TranslateToJa("test", ref dummyOutput));

                // リッチテキストボックス入力時のアンチエイリアスを抑制
                txtOrgText.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
                txtResultText.LanguageOption = RichTextBoxLanguageOptions.UIFonts;

                // 画像翻訳のイベント追加
                imageList.FormClosed += frmImageList_Closed;
                imageList.ScanFormClosed += ScanForm_Closed;

                // 音声出力のイベント追加
                speech.SpeakCompleted += speech_SpeakCompleted;

                // 接続表記を更新
                ConnectProcess();

                // システム設定を反映
                SetSystemSettingData();

                // タイトル情報ファイルを読み込む
                if (!File.Exists(TranslateInfo.ConfigFilePath))
                {
                    // 設定ファイルが存在しない場合は中断
                    TranslateInfo.SettingDs =
                        Util.CreateSettingDataSet(ProcessNameInfo.SelectedProcessAliasName);
                    return;
                }

                TranslateInfo.SettingDs = new DataSet();
                TranslateInfo.SettingDs.ReadXml(TranslateInfo.ConfigFilePath);
                cboLabel.DataSource = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE];
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
                isError = true;
            }
        }

        /// <summary>
        /// メインメニュー：プロセス再選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmProcessSelect_Click(object sender, EventArgs e)
        {
            try
            {
                imageList.Close();
                FormClose(false);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// シンプル表示チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmShowSimple_Click(object sender, EventArgs e)
        {
            try
            {
                if (tsmShowSimple.Checked)
                {
                    // シンプル表示
                    Size = new Size(794, Size.Height);
                    pnlModuleContainer.Visible = false;
                    pnlMiddleCmd.Visible = false;
                    FooterVisibleControl(false);
                    spcSplit.Panel2MinSize = 1;
                    MinimumSize = new Size(SHOW_SIMPLE_MIN_WIDTH, 0);
                }
                else
                {
                    // 通常表示
                    pnlModuleContainer.Visible = true;
                    pnlMiddleCmd.Visible = true;
                    FooterVisibleControl(true);
                    spcSplit.Panel2MinSize = MIDDLE_CMD_HEIGHT;
                    MinimumSize = new Size(794, 0);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// メインメニュー：システム設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSystemSetting_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmSetting())
                {
                    StopSpeech();
                    isShowChildForm = true;
                    form.ShowDialog();
                    SetSystemSettingData();
                    IsHide = false;
                    fromParentFlg = false;
                    isShowChildForm = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// メインメニュー：接続解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDisConnect_Click(object sender, EventArgs e)
        {
            try
            {
                DisConnectProcess();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// メインメニュー：再接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmReConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Util.GetProcess(ProcessNameInfo.SelectedProcessName) != null)
                {
                    ConnectProcess();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// メインメニュー：PCOTを終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmExitPcot_Click(object sender, EventArgs e)
        {
            try
            {
                // PCOTを終了
                FormClose(true);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
                isError = true;
            }
        }

        /// <summary>
        /// 表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmShowTranslatedText_Shown(object sender, EventArgs e)
        {
            try
            {
                if (isError)
                {
                    // ロード時にエラーがあった場合は中断
                    FormClose(false);
                }

                isCmdReady = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// アクティブ時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmShowTranslatedText_Activated(object sender, EventArgs e)
        {
            try
            {
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];

                if ((bool)dr[SettingColumns.UseSimpleCmd])
                {
                    if (simpleCmd.ClickFreeSelect)
                    {
                        FreeSelect();
                        simpleCmd.ClickFreeSelect = false;
                    }

                    if (simpleCmd.ClickTranslate)
                    {
                        if (simpleCmd.SelectedId == -1)
                        {
                            cboLabel.SelectedIndex = simpleCmd.SelectedId;
                        }
                        else
                        {
                            cboLabel.SelectedValue = simpleCmd.SelectedId;
                        }

                        Translate();
                        simpleCmd.ClickTranslate = false;
                    }

                    if (simpleCmd.ClickClipboardTranslate)
                    {
                        ClipboardTranslate();
                        simpleCmd.ClickClipboardTranslate = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ショートカット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShortcutKey(object sender, HotkeyEventArgs e)
        {
            try
            {
                // 子画面起動時は無効
                if (isShowChildForm)
                {
                    return;
                }

                // キャプチャー画面起動中にショートカットを押された場合は処理を中断
                if (TranslateInfo.ShowScanForm)
                {
                    return;
                }

                // 簡易コマンド画面使用時は無効
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                if ((bool)dr[SettingColumns.UseSimpleCmd])
                {
                    return;
                }

                // ショートカット確定時は入力をキャンセル
                e.Handled = true;

                // 固定翻訳
                if (e.Name.Contains(Util.SHORTCUT_FIXEDTRANSLATE_NAME))
                {
                    bool isFreeSelect = false;
                    int number = int.Parse(e.Name.Replace(Util.SHORTCUT_FIXEDTRANSLATE_NAME, ""));
                    if (number > cboLabel.Items.Count)
                    {
                        isFreeSelect = true;
                    }

                    if (isFreeSelect)
                    {
                        // フリー選択
                        FreeSelect();
                    }
                    else
                    {
                        // 固定翻訳
                        cboLabel.SelectedIndex = number - 1;
                        Translate();
                    }
                }

                // フリー選択
                if (e.Name == Util.SHORTCUT_FREE_NAME)
                {
                    FreeSelect();
                }

                // 翻訳
                if (e.Name == Util.SHORTCUT_TRANSLATE_NAME)
                {
                    Translate();
                }

                // 画像翻訳
                if (e.Name == Util.SHORTCUT_IMAGE_NAME)
                {
                    ImageTranslate();
                }

                // クリップボード翻訳
                if (e.Name == Util.SHORTCUT_CLIPBOARD_NAME)
                {
                    if (!isStoppedProcess)
                    {
                        ClipboardTranslate();
                    }
                }

                // プロセス停止/再開
                if (e.Name == Util.SHORTCUT_PROCESS_STOP_START_NAME)
                {
                    if (TranslateInfo.ShowScanForm)
                    {
                        // キャプチャー画面が表示されていたら処理を中断
                        return;
                    }

                    if (ProcessNameInfo.DisconnectProcess)
                    {
                        // デスクトップ接続時は処理を中断
                        return;
                    }

                    Show();
                    Activate();

                    var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                    if (prcs == null)
                    {
                        // プロセスの取得に失敗した場合は処理を中断
                        return;
                    }

                    var hndl = UseWinApi.GetWindowHandle(prcs);

                    if (protectionFilm.IsDisposed)
                    {
                        protectionFilm = new frmProtection();
                    }

                    if (!isStoppedProcess)
                    {
                        // 対象プロセスをアクティブ
                        UseWinApi.WakeupWindow(hndl);
                        Thread.Sleep(100);

                        // 保護フィルムを上に重ねる
                        Rectangle rect = new Rectangle();
                        UseWinApi.GetWindowRectContainsFrame(hndl, ref rect);

                        if (rect.X <= 0 && rect.Y <= 0 && rect.Width <= 0 && rect.Height <= 0)
                        {
                            CommonUtil.PutMessage(
                                CommonEnums.MessageType.Warning, "該当アプリの座標及びサイズ取得に失敗しました。");
                            return;
                        }

                        if (rect.X == rect.Width || rect.Y == rect.Height)
                        {
                            Activate();
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, Util.CRITICAL_ERR_MES);
                            return;
                        }

                        protectionFilm.Rect = rect;
                        protectionFilm.ShowInTaskbar = false;

                        // プロセスを停止
                        prcs.Threads.Suspend();

                        // 保護フィルム表示
                        protectionFilm.Show();
                        protectionFilm.Activate();

                        tssConnectStateLbl.Text = string.Format(
                            STOPPED_STR, ProcessNameInfo.SelectedProcessAliasName);
                        tssConnectStateLbl.BackColor = STOPPED_BACK_COLOR;
                        tssConnectStateLbl.ForeColor = STOPPED_COLOR;
                        isStoppedProcess = true;
                    }
                    else
                    {
                        // 保護フィルムを閉じる
                        protectionFilm.Close();

                        // 対象プロセスをアクティブ
                        UseWinApi.WakeupWindow(hndl);
                        Thread.Sleep(100);

                        // プロセスを再開
                        prcs.Threads.Resume();

                        tssConnectStateLbl.Text = string.Format(
                            CONNECT_STR, ProcessNameInfo.SelectedProcessAliasName);
                        tssConnectStateLbl.BackColor = CONNECT_BACK_COLOR;
                        tssConnectStateLbl.ForeColor = CONNECT_COLOR;
                        isStoppedProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// スクリーンショット取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCaptureKey(object sender, HotkeyEventArgs e)
        {
            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];

            try
            {
                // 子画面起動時は無効
                if (isShowChildForm)
                {
                    return;
                }

                // キャプチャー画面起動中にショートカットを押された場合は処理を中断
                if (TranslateInfo.ShowScanForm)
                {
                    return;
                }

                if (isShowImgTranslate)
                {
                    Show();
                    Activate();

                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Error, "画像翻訳画面起動中は画像を取得できません。");
                    return;
                }

                if (isStoppedProcess)
                {
                    Show();
                    Activate();

                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Error, "プロセス停止中は画像を取得できません。");
                    return;
                }

                e.Handled = true;

                if ((bool)dr[SettingColumns.UseSimpleCmd] && IsHide)
                {
                    simpleCmd.Hide();
                }

                if (isStoppedProcess)
                {
                    Opacity = 0;
                }

                if (ProcessNameInfo.DisconnectProcess)
                {
                    // 翻訳表示画面を最小化
                    WindowState = FormWindowState.Minimized;
                    Thread.Sleep(200);

                    // スクリーン全体をキャプチャー
                    Util.SavePrintScreenImageForDesktop();
                }
                else
                {
                    // 接続中プロセスの画像を取得して保存
                    Util.SavePrintScreenImage();
                }

                if ((bool)dr[SettingColumns.UseSimpleCmd] && IsHide)
                {
                    simpleCmd.Show();
                }

                if (isStoppedProcess)
                {
                    Opacity = 1;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Util.CRITICAL_ERR_MES)
                {
                    if ((bool)dr[SettingColumns.UseSimpleCmd])
                    {
                        IsHide = false;
                        simpleCmd.ClickFreeSelect = false;
                        simpleCmd.ClickTranslate = false;
                        simpleCmd.ClickClipboardTranslate = false;
                        simpleCmd.Hide();
                        Show();
                    }

                    Activate();
                }

                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フォームキーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmShowTranslatedText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 常駐アイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntfIcon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (TranslateInfo.ShowScanForm)
                {
                    return;
                }

                if (e.Button == MouseButtons.Left)
                {
                    if (isShowChildForm)
                    {
                        return;
                    }

                    IsHide = false;
                    Show();
                    WindowState = FormWindowState.Normal;
                    Activate();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 翻訳設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTranSetting_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmCreateTitle())
                {
                    isShowChildForm = true;
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog();
                    isShowChildForm = false;

                    switch (form.Result)
                    {
                        case frmCreateTitle.LabelResult.Create:
                        case frmCreateTitle.LabelResult.Edit:

                            if (form.Result == frmCreateTitle.LabelResult.Create)
                            {
                                cboLabel.DataSource = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE];
                            }

                            // 新規作成か編集で範囲選択を選択した場合
                            TranslateInfo.TranslateRect = new Rectangle();
                            TranslateInfo.IsFreeSelect = false;
                            cboLabel.SelectedValue = form.SelectedId;
                            doUpdate = true;
                            RunScanForm();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// タイトルコンボボックス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLabel.SelectedIndex == -1)
                {
                    var settingDr = SystemSettingInfo.SystemSettingDt.Rows[0];
                    chkTargetReturn.Checked = (bool)settingDr[SettingColumns.TargetReturn];
                    chkIgnoreReturn.Checked = (bool)settingDr[SettingColumns.IgnoreReturn];
                    return;
                }

                var dr = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Where(w => (int)w[TitleColumns.Id] == (int)cboLabel.SelectedValue).FirstOrDefault();

                chkTargetReturn.Checked = (bool)dr[TitleColumns.TargetReturn];
                chkIgnoreReturn.Checked = (bool)dr[TitleColumns.IgnoreReturn];
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
                FreeSelect();
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
                Translate();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 画像翻訳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImgTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageTranslate();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 画像一覧のスキャンフォームが閉じた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanForm_Closed(object sender, EventArgs e)
        {
            try
            {
                if (imageList.RunTranslate)
                {
                    frmScan_Closed(null, null);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 画像翻訳が閉じた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmImageList_Closed(object sender, EventArgs e)
        {
            try
            {
                isShowImgTranslate = false;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 対象通りに改行チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTargetReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isRegistering)
                {
                    return;
                }

                // フリー選択時以外は更新
                if (cboLabel.SelectedIndex >= 0)
                {
                    isRegistering = true;
                    // 対象通りに改行
                    TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                        .Where(w => (int)w[TitleColumns.Id] == (int)cboLabel.SelectedValue)
                        .Select(s => s[TitleColumns.TargetReturn] = chkTargetReturn.Checked).ToList();
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                    isRegistering = false;
                }

                if (chkTargetReturn.Checked)
                {
                    chkIgnoreReturn.Checked = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 改行を無視にチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIgnoreReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isRegistering)
                {
                    return;
                }

                // フリー選択時以外は更新
                if (cboLabel.SelectedIndex >= 0)
                {
                    isRegistering = true;
                    // 改行を無視
                    TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                        .Where(w => (int)w[TitleColumns.Id] == (int)cboLabel.SelectedValue)
                        .Select(s => s[TitleColumns.IgnoreReturn] = chkIgnoreReturn.Checked).ToList();
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                    isRegistering = false;
                }

                if (chkIgnoreReturn.Checked)
                {
                    chkTargetReturn.Checked = false;
                    if (!string.IsNullOrEmpty(txtOrgText.Text))
                    {
                        txtOrgText.Text = Util.ConvertReturnToSpace(txtOrgText.Text);
                        orgEditHistoryList.Add(txtOrgText.Text.Trim());
                        currentEditIdx = orgEditHistoryList.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 履歴リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmHistoryList())
                {
                    isShowChildForm = true;
                    form.ShowDialog();
                    isShowChildForm = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 辞書リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDic_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmDictionaryList())
                {
                    isShowChildForm = true;
                    form.ShowDialog();
                    isShowChildForm = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 名詞リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNoun_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmNounList())
                {
                    // 表示前に現在のコンボボックスの選択を退避
                    var index = cboLabel.SelectedIndex;
                    isShowChildForm = true;
                    form.ShowDialog();
                    isShowChildForm = false;
                    // 戻ってきた後に選択位置を元に戻す
                    cboLabel.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 原文変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrgText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtOrgText.ClearUndo();
                originalText = txtOrgText.Text.Trim().ToLower();
                if (isAllowAddEditText)
                {
                    if (orgEditHistoryList.Count == 0 ||
                        txtOrgText.Text.Trim() != orgEditHistoryList[currentEditIdx])
                    {
                        orgEditHistoryList.Add(txtOrgText.Text.Trim());
                        currentEditIdx = orgEditHistoryList.Count - 1;
                    }
                }
                else
                {
                    isAllowAddEditText = true;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 原文キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrgText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.Z)
                {
                    if (orgEditHistoryList.Count > 0)
                    {
                        currentEditIdx = CommonUtil.GetLimitValue(
                            currentEditIdx - 1, 0, orgEditHistoryList.Count - 1);
                        txtOrgText.Text = orgEditHistoryList[currentEditIdx];
                        isAllowAddEditText = false;
                    }
                }
                else if (e.KeyCode == Keys.F1)
                {
                    if (string.IsNullOrEmpty(txtOrgText.SelectedText))
                    {
                        return;
                    }

                    var selectedText = txtOrgText.SelectedText;
                    var editedText = Util.ConvertReturnToSpace(selectedText);
                    txtOrgText.Text = txtOrgText.Text.Replace(selectedText, editedText);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 原文マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrgText_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    txtOrgText.Focus();
                    if (chkRelationDeepL.Checked)
                    {
                        // チェックされている場合はDeepL起動コマンド
                        if (!string.IsNullOrEmpty(txtOrgText.Text))
                        {
                            if (e.X == -1)
                            {
                                SelectAllAndCopySendKey(true);
                            }
                            else
                            {
                                SelectAllAndCopySendKey();
                            }
                        }
                    }
                    else
                    {
                        // チェックされていない場合はクリップボードにコピー
                        Clipboard.SetText(txtOrgText.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 訳文フォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtResultText_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtResultText.SelectedText) &&
                    spcSplit.ActiveControl?.Name == btnRegistNoun.Name)
                {
                    resultSelectedText = txtResultText.SelectedText;
                }
                else
                {
                    resultSelectedText = string.Empty;
                }

                txtResultText.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 即時翻訳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnImmediateTrans_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOrgText.Text))
                {

                    if (preOriginalText != txtOrgText.Text)
                    {
                        Util.TranslateQuality quality = Util.TranslateQuality.NoWork;
                        var inputText = txtOrgText.Text;
                        var resultText = string.Empty;

                        // 名詞置換
                        var nounList = TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].AsEnumerable()
                            .Where(w => w[NounColumns.IsEnabled].Equals(true))
                            .OrderByDescending(o => o[NounColumns.BeforeNoun].ToString().Length)
                            .Select(s => new
                            {
                                beforeText = s[NounColumns.BeforeNoun].ToString(),
                                afterText = s[NounColumns.AfterNoun].ToString()
                            }).ToList();

                        await Task.Run(() =>
                        {
                            quality = Util.TranslateToJa(inputText, ref resultText);
                        });

                        SetTranslateQualityLabel(quality);
                        foreach (var item in nounList)
                        {
                            resultText = resultText.Replace(item.beforeText, item.afterText);
                        }

                        txtResultText.Text = resultText;

                        preOriginalText = inputText;
                    }

                    // 音声出力
                    AutoSpeech();

                    // 表示後にDeepLを起動
                    if (chkRelationDeepL.CheckState == CheckState.Checked)
                    {
                        if (!string.IsNullOrEmpty(txtOrgText.Text))
                        {
                            txtOrgText.Focus();
                            SelectAllAndCopySendKey();
                        }
                    }

                    // 翻訳結果をコピー
                    if (chkCopyResult.Checked)
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(txtResultText.Text.Trim());
                        Util.OutputTextFile(OUTPUT_RESULTFILE, txtResultText.Text.Trim(), Encoding.UTF8, false);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                Util.OutputTextFile(OUTPUT_RESULTFILE, string.Empty, Encoding.UTF8, false);
                orgEditHistoryList = new List<string>();
                txtOrgText.Text = string.Empty;
                txtResultText.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 翻訳結果出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputResult_Click(object sender, EventArgs e)
        {
            try
            {
                // クリップボードに文字列があればそちらを優先
                var writeText = Clipboard.GetText();
                if (string.IsNullOrEmpty(writeText))
                {
                    writeText = txtResultText.Text.Trim();
                }

                Util.OutputTextFile(OUTPUT_RESULTFILE, writeText, Encoding.UTF8, false);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// DeepLチェック状態変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRelationDeepL_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                switch (chkRelationDeepL.CheckState)
                {
                    case CheckState.Checked:
                        dr[SettingColumns.RelationDeepLState] = 0;
                        dr[SettingColumns.CopyResult] = false;
                        chkCopyResult.Checked = false;
                        break;
                    case CheckState.Indeterminate:
                        dr[SettingColumns.RelationDeepLState] = 1;
                        break;
                    case CheckState.Unchecked:
                        dr[SettingColumns.RelationDeepLState] = 2;
                        break;
                }

                for (int i = 0; i < 10; i++)
                {
                    if (!Util.IsFileLocked(SystemSettingInfo.SystemSettingFilePath))
                    {
                        break;
                    }
                }

                // 出力
                Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 翻訳結果をコピーチェック変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCopyResult_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                if (chkCopyResult.Checked)
                {
                    if (chkRelationDeepL.CheckState == CheckState.Checked)
                    {
                        dr[SettingColumns.RelationDeepLState] = 1;
                        chkRelationDeepL.CheckState = CheckState.Indeterminate;
                        dr[SettingColumns.CopyResult] = false;
                    }
                    else
                    {
                        dr[SettingColumns.CopyResult] = true;
                    }
                }
                else
                {
                    dr[SettingColumns.CopyResult] = false;
                }

                // 出力
                Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 履歴登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrgText.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "原文が存在しません。");
                    return;
                }

                if (string.IsNullOrEmpty(txtResultText.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "訳文が存在しません。");
                    return;
                }

                using (var form = new frmCreateHistory())
                {
                    isShowChildForm = true;
                    if (ProcessNameInfo.DisconnectProcess)
                    {
                        // 切断中
                        form.ProcessName = TranslateInfo.PCOT_DESKTOP_CONNECT;
                    }
                    else
                    {
                        // 接続中
                        form.ProcessName = ProcessNameInfo.SelectedProcessAliasName;
                    }

                    orgEditHistoryList = new List<string>();
                    form.OriginalText = txtOrgText.Text.Trim();
                    form.ResultText = txtResultText.Text.Trim();
                    form.ShowDialog();
                    isShowChildForm = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 辞書登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistDic_Click(object sender, EventArgs e)
        {
            try
            {
                string selText = string.Empty;
                if (!string.IsNullOrEmpty(txtOrgText.SelectedText))
                {
                    // 原文でテキストが選択されていた場合
                    selText = txtOrgText.SelectedText;
                }

                using (var form = new frmCreateReplace())
                {
                    isShowChildForm = true;
                    form.InputText = selText.Trim();
                    form.OutputText = selText.Trim();

                    if (ProcessNameInfo.DisconnectProcess)
                    {
                        // 切断中
                        form.ProcessName = TranslateInfo.PCOT_DESKTOP_CONNECT;
                    }
                    else
                    {
                        // 接続中
                        form.ProcessName = ProcessNameInfo.SelectedProcessAliasName;
                    }

                    form.ShowDialog();
                    isShowChildForm = false;

                    if (form.DialogResult == DialogResult.OK)
                    {
                        orgEditHistoryList = new List<string>();
                        txtOrgText.Text = form.OutputText;
                        if (preOriginalText != txtOrgText.Text)
                        {
                            string inputText = txtOrgText.Text;
                            string outputText = string.Empty;
                            var quality = Util.TranslateToJa(inputText, ref outputText);
                            SetTranslateQualityLabel(quality);
                            txtResultText.Text = outputText;
                            preOriginalText = inputText;
                        }

                        AutoSpeech();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 名詞登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistNoun_Click(object sender, EventArgs e)
        {
            try
            {
                var selText = string.Empty;

                if (!string.IsNullOrEmpty(resultSelectedText))
                {
                    // 訳文でテキストが選択されていた場合
                    selText = resultSelectedText;
                }

                using (var form = new frmCreateNoun())
                {
                    form.InputText = selText.Trim();
                    form.OutputText = selText.Trim();
                    isShowChildForm = true;
                    form.ShowDialog();
                    isShowChildForm = false;

                    if (form.DialogResult == DialogResult.OK)
                    {
                        orgEditHistoryList = new List<string>();
                        txtOrgText.Text = form.InputText;
                        txtResultText.Text = form.OutputText;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// プロセス再選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPrcs_Click(object sender, EventArgs e)
        {
            try
            {
                imageList.Close();
                FormClose(false);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 再接続/接続解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecnctPrcs_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnRecnctPrcs.Text == RECONNECT_CAPTION_NAME)
                {
                    if (Util.GetProcess(ProcessNameInfo.SelectedProcessName) != null)
                    {
                        isDisConnectOperation = false;
                        ConnectProcess();
                    }
                }
                else
                {
                    isDisConnectOperation = true;
                    DisConnectProcess();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// コマンドモード切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeCmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                if (btnChangeCmdMode.Text == CHANGE_SIMPLECMD_NAME)
                {
                    // 簡易コマンド画面に切り替える
                    btnChangeCmdMode.Text = CHANGE_SHORTCUT_NAME;
                    dr[SettingColumns.UseSimpleCmd] = true;
                    dr[SettingColumns.UseShortcut] = false;

                    if (isStoppedProcess)
                    {
                        // 保護フィルムを閉じる
                        protectionFilm.Close();

                        var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                        if (prcs != null)
                        {
                            // プロセスが停止中の場合は再開
                            prcs.Threads.Resume();
                            tssConnectStateLbl.Text = string.Format(
                                CONNECT_STR, ProcessNameInfo.SelectedProcessAliasName);
                            tssConnectStateLbl.BackColor = CONNECT_BACK_COLOR;
                            tssConnectStateLbl.ForeColor = CONNECT_COLOR;
                            isStoppedProcess = false;
                        }
                    }
                }
                else
                {
                    // ショートカットに切り替える
                    btnChangeCmdMode.Text = CHANGE_SIMPLECMD_NAME;
                    dr[SettingColumns.UseSimpleCmd] = false;
                    dr[SettingColumns.UseShortcut] = true;
                }

                Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
                IsHide = false;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 再生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                PlaySpeech();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                PauseSpeech();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                StopSpeech();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 音声出力完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speech_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    var prompt = e.Prompt;
                    PromptBuilder promptBuilder = new PromptBuilder();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// PCOTの終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                FormClose(true);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// PCOTの終了(常駐から)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExitPcot_Click(object sender, EventArgs e)
        {
            try
            {
                FormClose(true);
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
                if (TranslateInfo.UseOcrEngine == 0)
                {
                    Text = string.Format(TITLE_NAME, "Windows10 OCR");
                }
                else
                {
                    Text = string.Format(TITLE_NAME, "Tesseract OCR");
                }

                if (string.IsNullOrEmpty(TranslateInfo.OriginalText))
                {
                    orgEditHistoryList = new List<string>();
                    isAllowAddEditText = false;
                    txtOrgText.Text = string.Empty;
                    txtResultText.Text = string.Empty;

                    if (!TranslateInfo.FromRunCmd)
                    {
                        Show();
                        WindowState = FormWindowState.Normal;
                        Refresh();
                        Activate();

                        return;
                    }
                }

                // タイトルの新規作成かスキャン範囲編集で更新
                if (doUpdate && cboLabel.SelectedIndex >= 0)
                {
                    // IDを指定して範囲を更新
                    var id = (int)cboLabel.SelectedValue;
                    var dr = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                        .Where(w => w[TitleColumns.Id].Equals(id)).FirstOrDefault();

                    dr[TitleColumns.UseOcrEngine] = TranslateInfo.UseOcrEngine;
                    dr[TitleColumns.ReadMultiples] = TranslateInfo.ReadMultiples;
                    dr[TitleColumns.X] = TranslateInfo.TranslateRect.X;
                    dr[TitleColumns.Y] = TranslateInfo.TranslateRect.Y;
                    dr[TitleColumns.Width] = TranslateInfo.TranslateRect.Width;
                    dr[TitleColumns.Height] = TranslateInfo.TranslateRect.Height;

                    // 出力
                    if (ProcessNameInfo.DisconnectProcess)
                    {
                        // デスクトップ
                        Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.DesktopConfigFilePath);
                    }
                    else
                    {
                        // 対象プロセス
                        Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                    }
                }

                // 置換処理
                var dicFilePath = Util.GetDictionaryFilePath();
                if (File.Exists(dicFilePath))
                {
                    var dt = new DataTable();
                    dt.ReadXml(dicFilePath);
                    //------------------------------
                    // 項目ごとに順々に置換を行う
                    //------------------------------
                    // 無視
                    var ignoreList = dt.AsEnumerable()
                        .Where(w =>
                        w[DicColumns.IsEnabled].Equals(true) &&
                        (int)w[DicColumns.DicTypeCd] == 1
                        )
                        .Select(s => new
                        {
                            before = s[DicColumns.BeforeText].ToString(),
                            after = s[DicColumns.AfterText].ToString()
                        }).ToList();

                    foreach (var item in ignoreList)
                    {
                        // 無視置換
                        TranslateInfo.OriginalText =
                            TranslateInfo.OriginalText.Replace(item.before, item.after);
                    }

                    // 文字単位
                    var strList = dt.AsEnumerable()
                        .Where(w =>
                        w[DicColumns.IsEnabled].Equals(true) &&
                        (int)w[DicColumns.DicTypeCd] == 0 &&
                        !(bool)w[DicColumns.IsWordUnit]
                        )
                        .Select(s => new
                        {
                            before = s[DicColumns.BeforeText].ToString(),
                            after = s[DicColumns.AfterText].ToString(),
                            ignoreCase = !(bool)s[DicColumns.IsUpperAndLower]
                        }).ToList();

                    foreach (var item in strList)
                    {
                        // 文字置換
                        if (item.ignoreCase)
                        {
                            try
                            {
                                // 大文字小文字を区別しない
                                TranslateInfo.OriginalText =
                                    Regex.Replace(TranslateInfo.OriginalText,
                                    item.before, item.after, RegexOptions.IgnoreCase);
                            }
                            catch
                            {
                                // 大文字小文字を区別する
                                TranslateInfo.OriginalText =
                                    TranslateInfo.OriginalText.Replace(item.before, item.after);
                            }
                        }
                        else
                        {
                            // 大文字小文字を区別する
                            TranslateInfo.OriginalText =
                                TranslateInfo.OriginalText.Replace(item.before, item.after);
                        }
                    }

                    // 単語単位
                    var wordUnitList = dt.AsEnumerable()
                        .Where(w =>
                        w[DicColumns.IsEnabled].Equals(true) &&
                        (int)w[DicColumns.DicTypeCd] == 0 &&
                        (bool)w[DicColumns.IsWordUnit]
                        )
                        .Select(s => new
                        {
                            before = s[DicColumns.BeforeText].ToString(),
                            after = s[DicColumns.AfterText].ToString(),
                            ignoreCase = !(bool)s[DicColumns.IsUpperAndLower]
                        }).ToList();

                    foreach (var item in wordUnitList)
                    {
                        // 単語単位
                        var wordList = TranslateInfo.OriginalText.Split(' ');
                        for (int i = 0; i < wordList.Length; i++)
                        {
                            var splitReturn = wordList[i]
                                .Split(new string[] { "\r\n" }, StringSplitOptions.None);

                            for (int j = 0; j < splitReturn.Length; j++)
                            {
                                try
                                {
                                    if (item.ignoreCase)
                                    {
                                        if (splitReturn[j]
                                            .Trim('\"')
                                            .Trim('\'')
                                            .Trim(',')
                                            .Trim('.')
                                            .Trim(':')
                                            .Trim(';')
                                            .Replace("?", "")
                                            .Replace("!", "")
                                            .ToLower() == item.before.ToLower())
                                        {
                                            // 大文字小文字を区別しない
                                            splitReturn[j] = Regex.Replace(splitReturn[j],
                                            item.before, item.after, RegexOptions.IgnoreCase);
                                        }
                                    }
                                    else
                                    {
                                        if (splitReturn[j]
                                            .Trim('\"')
                                            .Trim('\'')
                                            .Trim(',')
                                            .Trim('.')
                                            .Trim(':')
                                            .Trim(';')
                                            .Replace("?", "")
                                            .Replace("!", "")
                                            == item.before)
                                        {
                                            // 大文字小文字を区別する
                                            splitReturn[j] = splitReturn[j].Replace(item.before, item.after);
                                        }
                                    }
                                }
                                catch
                                {
                                    if (splitReturn[j]
                                        .Trim('\"')
                                        .Trim('\'')
                                        .Trim(',')
                                        .Trim('.')
                                        .Trim(':')
                                        .Trim(';')
                                        .Replace("?", "")
                                        .Replace("!", "")
                                        == item.before)
                                    {
                                        // 大文字小文字を区別する
                                        splitReturn[j] = splitReturn[j].Replace(item.before, item.after);
                                    }
                                }
                            }

                            wordList[i] = string.Join("\r\n", splitReturn);
                        }

                        TranslateInfo.OriginalText = string.Join(" ", wordList);
                    }
                }

                // 原文を表示
                orgEditHistoryList = new List<string>();
                txtOrgText.Text = TranslateInfo.OriginalText;

                // 翻訳
                if (preOriginalText != txtOrgText.Text)
                {
                    string inputText = txtOrgText.Text;
                    string resultText = string.Empty;
                    var quality = Util.TranslateToJa(inputText, ref resultText);
                    SetTranslateQualityLabel(quality);

                    // 名詞置換
                    var nounList = TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].AsEnumerable()
                        .Where(w => w[NounColumns.IsEnabled].Equals(true))
                        .OrderByDescending(o => o[NounColumns.BeforeNoun].ToString().Length)
                        .Select(s => new
                        {
                            beforeText = s[NounColumns.BeforeNoun].ToString(),
                            afterText = s[NounColumns.AfterNoun].ToString()
                        }).ToList();

                    foreach (var item in nounList)
                    {
                        resultText = resultText.Replace(item.beforeText, item.afterText);
                    }

                    // 訳文を表示
                    txtResultText.Text = resultText;
                }

                // 音声出力
                AutoSpeech();

                Show();
                WindowState = FormWindowState.Normal;
                Refresh();
                Activate();

                // 表示後にDeepLを起動
                if (chkRelationDeepL.CheckState == CheckState.Checked)
                {
                    if (!string.IsNullOrEmpty(txtOrgText.Text))
                    {
                        txtOrgText.Focus();
                        MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Right, 1, -1, -1, 120);
                        txtOrgText_MouseDown(txtOrgText, mouseEvent);
                    }
                }
                else
                {
                    AfterTranslatedActivateSelectedProcess();
                }

                // 翻訳結果をコピー
                if (chkCopyResult.Checked)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(txtResultText.Text.Trim());
                    Util.OutputTextFile(OUTPUT_RESULTFILE, txtResultText.Text.Trim(), Encoding.UTF8, false);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
            finally
            {
                // 最後の最後にスレッドを再開
                if (!ProcessNameInfo.DisconnectProcess)
                {
                    var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                    if (prcs != null && !isStoppedProcess)
                    {
                        prcs.Threads.Resume();
                    }
                }

                if (TranslateInfo.FromRunCmd)
                {
                    TranslateInfo.FromRunCmd = false;
                }
            }
        }

        /// <summary>
        /// プロセスの接続チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timChkPrcs_Tick(object sender, EventArgs e)
        {
            try
            {
                // 途中でプロセスが閉じたかどうかを調べる
                if (Util.GetProcess(ProcessNameInfo.SelectedProcessName) == null &&
                    !ProcessNameInfo.DisconnectProcess)
                {
                    DisConnectProcess();
                    return;
                }

                // 途中でプロセスが起動したかどうかを調べる
                if (ProcessNameInfo.DisconnectProcess && !isDisConnectOperation)
                {
                    if (Util.GetProcess(ProcessNameInfo.SelectedProcessName) != null)
                    {
                        ConnectProcess();
                        return;
                    }
                }

                // コマンド実行条件が揃っている場合はコマンド実行
                if (isCmdReady && !isShowChildForm && !TranslateInfo.ShowScanForm && RunCommand())
                {
                    if (simpleCmd.IsShowSimpleCmd)
                    {
                        simpleCmd.Hide();
                        simpleCmd.IsNotifyHide = false;
                        simpleCmd.IsShowSimpleCmd = false;
                        IsHide = false;
                    }
                    return;
                }

                // 簡易コマンド画面の表示切替
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                if (!simpleCmd.IsDisposed && (bool)dr[SettingColumns.UseSimpleCmd] &&
                    !TranslateInfo.ShowScanForm && !isShowChildForm)
                {
                    if (ProcessNameInfo.SelectedProcessName == UseWinApi.GetActiveProcessName())
                    {
                        if (!ProcessNameInfo.DisconnectProcess)
                        {
                            // 対象プロセスがアクティブ
                            if (!IsHide)
                            {
                                // 翻訳表示画面が表示されている場合
                                isActiveHideByCode = true;
                                WindowState = FormWindowState.Minimized;
                            }
                        }
                    }
                    else if (Util.GetCurrentProcessName() != UseWinApi.GetActiveProcessName())
                    {
                        // 対象プロセス以外がアクティブ
                        if (IsHide && isActiveHideByCode && !ProcessNameInfo.DisconnectProcess)
                        {
                            // 翻訳表示画面が隠れている且つコードによって閉じられた場合の復元
                            simpleCmd.Hide();
                            simpleCmd.IsNotifyHide = false;
                            WindowState = FormWindowState.Normal;
                            Show();
                            IsHide = false;
                            isActiveHideByCode = false;
                            dr[SettingColumns.Transparency] = Convert.ToInt32(simpleCmd.CurrentOpacity * 100);
                            Util.WriteXml(
                                SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
                        }
                    }

                    if (WindowState == FormWindowState.Minimized && !IsHide)
                    {
                        WindowState = FormWindowState.Normal;
                        IsHide = true;
                        fromParentFlg = true;
                        Hide();
                    }

                    if (IsHide)
                    {
                        if (!simpleCmd.IsNotifyHide)
                        {
                            if (!simpleCmd.IsShowSimpleCmd)
                            {
                                // 簡易コマンドを表示
                                if (fromParentFlg)
                                {
                                    if (cboLabel.SelectedIndex == -1)
                                    {
                                        // 選択なし(フリー選択)
                                        simpleCmd.SelectedId = cboLabel.SelectedIndex;
                                    }
                                    else
                                    {
                                        // 選択あり(ID)
                                        simpleCmd.SelectedId = (int)cboLabel.SelectedValue;
                                    }

                                    fromParentFlg = false;
                                }

                                simpleCmd.CurrentOpacity =
                                    Convert.ToSingle((int)dr[SettingColumns.Transparency]) / 100;
                                simpleCmd.IsShowSimpleCmd = true;
                                simpleCmd.Show();
                            }
                        }
                        else
                        {
                            // 簡易コマンドを隠す
                            if (simpleCmd.SelectedId == -1)
                            {
                                // 選択なし(フリー選択)
                                cboLabel.SelectedIndex = simpleCmd.SelectedId;
                            }
                            else
                            {
                                // 選択あり(ID)
                                cboLabel.SelectedValue = simpleCmd.SelectedId;
                            }

                            simpleCmd.Hide();
                            simpleCmd.IsNotifyHide = false;
                            simpleCmd.IsShowSimpleCmd = false;
                            WindowState = FormWindowState.Normal;
                            Show();
                            Activate();
                            IsHide = false;
                        }
                    }
                    else
                    {
                        // 簡易コマンドを隠す
                        simpleCmd.IsNotifyHide = false;
                        simpleCmd.IsShowSimpleCmd = false;
                        simpleCmd.Hide();
                        Show();
                    }
                }

                SuspendLayout();
                if (string.IsNullOrEmpty(txtResultText.SelectedText))
                {
                    if (isSelectedResult)
                    {
                        // 訳文でテキストが選択されていない場合は中断
                        string text = txtOrgText.Text;
                        isAllowAddEditText = false;
                        txtOrgText.Text = string.Empty;
                        txtOrgText.ScrollToCaret();
                        isAllowAddEditText = false;
                        txtOrgText.Text = text;
                        ScrollToSearchText(prevIndex);
                        isSelectedResult = false;
                        prevIndex = 0;
                        ResumeLayout(false);
                        return;
                    }
                }

                // 対象テキスト
                var targetText = txtResultText.SelectedText.Trim().ToLower();

                // 原文を全てを検索
                int index = -1;
                while (true)
                {
                    if (string.IsNullOrEmpty(targetText))
                    {
                        break;
                    }

                    if (index + 1 >= originalText.Length)
                    {
                        break;
                    }

                    index = originalText.IndexOf(targetText, index + 1);
                    if (index == -1)
                    {
                        break;
                    }

                    prevIndex = index;

                    if (prevSelText != targetText)
                    {
                        // 前に選択したテキストと検索テキストが異なる場合は初期化
                        string text = txtOrgText.Text;
                        int caret = txtOrgText.SelectionStart;
                        isAllowAddEditText = false;
                        txtOrgText.Text = string.Empty;
                        txtOrgText.ScrollToCaret();
                        isAllowAddEditText = false;
                        txtOrgText.Text = text;
                        ScrollToSearchText(index);
                    }

                    txtOrgText.SelectionStart = index;
                    txtOrgText.SelectionLength = targetText.Length;
                    txtOrgText.SelectionBackColor = Color.Yellow;

                    if (!isSelectedResult)
                    {
                        // 一度だけスクロール
                        ScrollToSearchText(index);
                    }

                    // みつけた場合は検索テキストを退避
                    prevSelText = targetText;
                    isSelectedResult = true;
                }

                ResumeLayout(false);
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
        private void frmShowTranslatedText_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!isClose)
                {
                    e.Cancel = true;
                    IsHide = true;
                    fromParentFlg = true;
                    var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                    if (!simpleCmd.IsDisposed && (bool)dr[SettingColumns.UseSimpleCmd])
                    {
                        Hide();
                    }
                    else
                    {
                        WindowState = FormWindowState.Minimized;
                    }
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
        /// プロセス接続表示
        /// </summary>
        private void ConnectProcess()
        {
            tssConnectStateLbl.Text = string.Format(CONNECT_STR, ProcessNameInfo.SelectedProcessAliasName);
            tssConnectStateLbl.BackColor = CONNECT_BACK_COLOR;
            tssConnectStateLbl.ForeColor = CONNECT_COLOR;
            btnRecnctPrcs.Text = DISCONNECT_CAPTION_NAME;
            ProcessNameInfo.DisconnectProcess = false;
            tsmDisConnect.Enabled = true;
            tsmReConnect.Enabled = false;

            TranslateInfo.Init();

            // タイトル情報ファイルを読み込む
            if (!File.Exists(TranslateInfo.ConfigFilePath))
            {
                // 設定ファイルが存在しない場合は中断
                ((DataTable)cboLabel.DataSource)?.Clear();
                TranslateInfo.SettingDs =
                    Util.CreateSettingDataSet(ProcessNameInfo.SelectedProcessAliasName);

                return;
            }

            ((DataTable)cboLabel.DataSource)?.Clear();
            TranslateInfo.SettingDs = new DataSet();
            TranslateInfo.SettingDs.ReadXml(TranslateInfo.ConfigFilePath);
            cboLabel.DataSource = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE];
            if (simpleCmd != null && !simpleCmd.IsDisposed)
            {
                simpleCmd.BindComboBoxItem();
            }
        }

        /// <summary>
        /// プロセス切断表示
        /// </summary>
        private void DisConnectProcess()
        {
            if (isStoppedProcess)
            {
                // 保護フィルムを閉じる
                protectionFilm.Close();

                // プロセスを停止している場合は再開する
                isStoppedProcess = false;
                var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                if (prcs != null)
                {
                    prcs.Threads.Resume();
                }
            }

            tssConnectStateLbl.BackColor = CONNECT_BACK_COLOR;
            tssConnectStateLbl.ForeColor = CONNECT_COLOR;
            tssConnectStateLbl.Text = tssConnectStateLbl.Text =
                string.Format(CONNECT_STR, CONNECT_DESKTOP_STR);

            btnRecnctPrcs.Text = RECONNECT_CAPTION_NAME;
            tsmDisConnect.Enabled = false;
            tsmReConnect.Enabled = true;
            ProcessNameInfo.DisconnectProcess = true;
            TranslateInfo.Init();

            // タイトル情報ファイルを読み込む
            if (!File.Exists(TranslateInfo.DesktopConfigFilePath))
            {
                // 設定ファイルが存在しない場合は中断
                ((DataTable)cboLabel.DataSource)?.Clear();
                TranslateInfo.SettingDs =
                    Util.CreateSettingDataSet(TranslateInfo.PCOT_DESKTOP_CONNECT);

                return;
            }

            TranslateInfo.SettingDs = new DataSet();
            TranslateInfo.SettingDs.ReadXml(TranslateInfo.DesktopConfigFilePath);
            cboLabel.DataSource = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE];
            if (simpleCmd != null && !simpleCmd.IsDisposed)
            {
                simpleCmd.BindComboBoxItem();
            }
        }

        /// <summary>
        /// スキャンフォーム起動
        /// </summary>
        private void RunScanForm()
        {
            TranslateInfo.ToReturnOrginal = chkTargetReturn.Checked;
            TranslateInfo.ToIgnoreReturn = chkIgnoreReturn.Checked;

            Rectangle rect = new Rectangle();
            Opacity = 0;

            try
            {
                // その都度該当プロセスを取得する
                if (ProcessNameInfo.DisconnectProcess)
                {
                    rect.X = Screen.PrimaryScreen.Bounds.X;
                    rect.Y = Screen.PrimaryScreen.Bounds.Y;
                    rect.Width = Screen.PrimaryScreen.Bounds.Width;
                    rect.Height = Screen.PrimaryScreen.Bounds.Height;
                }
                else
                {
                    // 接続中
                    var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                    if (prcs == null)
                    {
                        // なんらかの理由で存在しない場合は中断
                        return;
                    }

                    var hndl = UseWinApi.GetWindowHandle(prcs);

                    if (UseWinApi.GetActiveProcessName() != ProcessNameInfo.SelectedProcessName)
                    {
                        // 該当プロセスをアクティブにする
                        UseWinApi.WakeupWindow(hndl);

                        // 若干ディレイを持たせる(0.2秒)
                        Thread.Sleep(200);
                    }

                    if (isStoppedProcess)
                    {
                        protectionFilm.Opacity = 0.01;
                        protectionFilm.TopMost = true;
                    }

                    var dr = SystemSettingInfo.SystemSettingDt.Rows[0];

                    if ((bool)dr[SettingColumns.StopTargetProcess] && !isStoppedProcess)
                    {
                        // プロセスを停止
                        prcs.Threads.Suspend();
                    }

                    UseWinApi.GetWindowRectContainsFrame(hndl, ref rect);

                    if (rect.X <= 0 && rect.Y <= 0 && rect.Width <= 0 && rect.Height <= 0)
                    {
                        CommonUtil.PutMessage(
                            CommonEnums.MessageType.Warning, "該当アプリの座標及びサイズ取得に失敗しました。");
                        return;
                    }

                    if (rect.X == rect.Width || rect.Y == rect.Height)
                    {
                        if ((bool)dr[SettingColumns.UseSimpleCmd])
                        {
                            IsHide = false;
                            simpleCmd.ClickFreeSelect = false;
                            simpleCmd.ClickTranslate = false;
                            simpleCmd.ClickClipboardTranslate = false;
                            simpleCmd.Hide();
                            Show();
                        }

                        Activate();
                        CommonUtil.PutMessage(CommonEnums.MessageType.Error, Util.CRITICAL_ERR_MES);
                        return;
                    }
                }

                var scan = new frmScan();

                // 既に翻訳範囲が指定されている場合
                if (TranslateInfo.TranslateRect.X != 0 &&
                    TranslateInfo.TranslateRect.Y != 0 &&
                    TranslateInfo.TranslateRect.Width != 0 &&
                    TranslateInfo.TranslateRect.Height != 0)
                {
                    scan.ShowInTaskbar = false;
                }

                scan.Rect = rect;
                scan.FormClosed += frmScan_Closed;
                Hide();

                if (ProcessNameInfo.DisconnectProcess)
                {
                    Thread.Sleep(100);
                }

                TranslateInfo.ShowScanForm = true;

                if (isStoppedProcess)
                {
                    protectionFilm.TopMost = false;
                }

                scan.Show();
            }
            finally
            {
                Opacity = 1;
            }
        }

        /// <summary>
        /// 原文テキストのスクロール
        /// </summary>
        /// <param name="index">検索文字列のインデックス</param>
        private void ScrollToSearchText(int index)
        {
            int[] pos = new int[2];
            int lineHeight = txtOrgText.GetPositionFromCharIndex(
                txtOrgText.GetFirstCharIndexFromLine(1)).Y -
                txtOrgText.GetPositionFromCharIndex(txtOrgText.GetFirstCharIndexFromLine(0)).Y;

            pos[1] =
                (txtOrgText.GetLineFromCharIndex(index) + 1)
                *
                lineHeight - txtOrgText.ClientSize.Height;

            if (txtOrgText.BorderStyle != BorderStyle.None)
            {
                ++pos[1];
            }

            if (pos[1] > 0)
            {
                UseWinApi.ScrollToSearchText(txtOrgText.Handle, pos);
            }
        }

        /// <summary>
        /// 原文全選択＋コピー＋コピー
        /// </summary>
        private async void SelectAllAndCopySendKey(bool useActivateOption = false)
        {
            InputSimulator input = new InputSimulator();
            input.Keyboard.ModifiedKeyStroke(
                WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
            await Task.Delay(200);
            input.Keyboard.ModifiedKeyStroke(
                WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_C);
            await Task.Delay(300);
            input.Keyboard.ModifiedKeyStroke(
                WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_C);

            await Task.Delay(400);

            // DeepLのアクティブ化
            var startTick = Environment.TickCount;
            while (true)
            {
                if ((Environment.TickCount - startTick) > 1000)
                {
                    // 1秒経ったら問答無用で抜ける
                    break;
                }

                var deepL = Util.GetProcess(PROCESS_NAME_OF_DEEPL);
                if (deepL != null)
                {
                    var hndl = UseWinApi.GetWindowHandle(deepL);
                    UseWinApi.WakeupWindow(hndl);
                    break;
                }
            }

            if (useActivateOption)
            {
                // DeepL起動後に対象プロセスをアクティブにする
                AfterTranslatedActivateSelectedProcess();
            }
        }

        /// <summary>
        /// システム設定を反映
        /// </summary>
        private void SetSystemSettingData()
        {
            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];

            // 原文フォント取得
            Color orgColor = Color.Empty;
            var orgFont = Util.GetFont(0, ref orgColor);
            Color rstColor = Color.Empty;
            var rstFont = Util.GetFont(1, ref rstColor);

            txtOrgText.Font = orgFont;
            txtOrgText.ForeColor = orgColor;
            txtResultText.Font = rstFont;
            txtResultText.ForeColor = rstColor;

            // 現在の状態がフリー選択だった場合
            if (cboLabel.SelectedIndex == -1)
            {
                chkTargetReturn.Checked = (bool)dr[SettingColumns.TargetReturn];
                chkIgnoreReturn.Checked = (bool)dr[SettingColumns.IgnoreReturn];
            }

            if ((int)dr[SettingColumns.UsingOcrEngine] == 0)
            {
                Text = string.Format(TITLE_NAME, "Windows10 OCR");
            }
            else
            {
                Text = string.Format(TITLE_NAME, "Tesseract OCR");
            }

            int state = (int)dr[SettingColumns.RelationDeepLState];
            switch (state)
            {
                case 0:
                    chkRelationDeepL.CheckState = CheckState.Checked;
                    break;
                case 1:
                    chkRelationDeepL.CheckState = CheckState.Indeterminate;
                    break;
                case 2:
                    chkRelationDeepL.CheckState = CheckState.Unchecked;
                    break;
            }

            speech.Volume = (int)dr[SettingColumns.SpeechVolume];
            speech.Rate = (int)dr[SettingColumns.SpeechRate];

            chkCopyResult.Checked = (bool)dr[SettingColumns.CopyResult];

            SetShortcutKey();

            if ((bool)dr[SettingColumns.UseShortcut])
            {
                // 簡易コマンドに切替
                btnChangeCmdMode.Text = CHANGE_SIMPLECMD_NAME;
            }
            else
            {
                // ショートカットに切替
                btnChangeCmdMode.Text = CHANGE_SHORTCUT_NAME;
            }
        }

        /// <summary>
        /// 翻訳表示画面を閉じる
        /// </summary>
        private void FormClose(bool isExit)
        {
            var prcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
            if (prcs != null)
            {
                if (!protectionFilm.IsDisposed)
                {
                    // 保護フィルムを閉じる
                    protectionFilm.Close();
                }

                prcs.Threads.Resume();
            }

            isClose = true;
            simpleCmd.Dispose();
            ntfIcon.Dispose();
            speech.Dispose();

            if (isExit)
            {
                // アプリケーション終了
                Application.Exit();
            }
            else
            {
                // 閉じる(プロセス再選択)
                Close();
            }
        }

        /// <summary>
        /// 翻訳品質を設定
        /// </summary>
        /// <param name="quality"></param>
        private void SetTranslateQualityLabel(Util.TranslateQuality quality)
        {
            switch (quality)
            {
                case Util.TranslateQuality.WebAPI:
                    // 通常
                    tssUsingTranslator.ForeColor = GOOGLE_NORMAL_COLOR;
                    tssUsingTranslator.BackColor = GOOGLE_NORMAL_BACK_COLOR;
                    tssUsingTranslator.Text = GOOGLE_WEBAPI_STR;
                    break;
                case Util.TranslateQuality.GAS:
                    // 低品質
                    tssUsingTranslator.ForeColor = GOOGLE_LOW_COLOR;
                    tssUsingTranslator.BackColor = GOOGLE_LOW_BACK_COLOR;
                    tssUsingTranslator.Text = GOOGLE_LOW_STR;
                    break;
                case Util.TranslateQuality.NoWork:
                    // 無効
                    tssUsingTranslator.ForeColor = GOOGLE_NOWORK_COLOR;
                    tssUsingTranslator.BackColor = GOOGLE_NOWORK_BACK_COLOR;
                    tssUsingTranslator.Text = GOOGLE_NOWORK_STR;
                    break;
            }
        }

        /// <summary>
        /// クリップボード翻訳
        /// </summary>
        private async void ClipboardTranslate()
        {
            if (string.IsNullOrEmpty(Clipboard.GetText()))
            {
                return;
            }


            // 翻訳
            orgEditHistoryList = new List<string>();

            if (preOriginalText != Clipboard.GetText())
            {
                // 名詞置換
                var nounList = TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].AsEnumerable()
                    .Where(w => w[NounColumns.IsEnabled].Equals(true))
                    .OrderByDescending(o => o[NounColumns.BeforeNoun].ToString().Length)
                    .Select(s => new
                    {
                        beforeText = s[NounColumns.BeforeNoun].ToString(),
                        afterText = s[NounColumns.AfterNoun].ToString()
                    }).ToList();

                string resultText = string.Empty;
                string inputText = Clipboard.GetText();
                Util.TranslateQuality quality = Util.TranslateQuality.NoWork;

                await Task.Run(() =>
                {
                    quality = Util.TranslateToJa(inputText, ref resultText);
                });

                txtOrgText.Text = inputText;
                SetTranslateQualityLabel(quality);

                foreach (var item in nounList)
                {
                    resultText = resultText.Replace(item.beforeText, item.afterText);
                }

                txtResultText.Text = resultText;
                preOriginalText = inputText;
            }

            // 音声出力
            AutoSpeech();

            Show();
            WindowState = FormWindowState.Normal;
            Activate();

            await Task.Delay(200);

            // 表示後にDeepLを起動
            if (chkRelationDeepL.CheckState == CheckState.Checked)
            {
                if (!string.IsNullOrEmpty(txtOrgText.Text))
                {
                    txtOrgText.Focus();
                    MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Right, 1, -1, -1, 120);
                    txtOrgText_MouseDown(txtOrgText, mouseEvent);
                }
            }
            else
            {
                AfterTranslatedActivateSelectedProcess();
            }

            // 翻訳結果をコピー
            if (chkCopyResult.Checked)
            {
                Clipboard.Clear();
                Clipboard.SetText(txtResultText.Text.Trim());
                Util.OutputTextFile(OUTPUT_RESULTFILE, txtResultText.Text.Trim(), Encoding.UTF8, false);
            }
        }

        /// <summary>
        /// ショートカットキーの登録
        /// </summary>
        private void SetShortcutKey()
        {
            // 有効/無効の切替を最新化するため、一度全部削除する
            foreach (var keyState in ShortcutInfo.ShortcutSettingList)
            {
                HotkeyManager.Current.Remove(keyState.ShortcutName);
            }

            // ショートカット登録
            foreach (var keyState in ShortcutInfo.ShortcutSettingList)
            {
                if (!keyState.IsEnabled)
                {
                    continue;
                }

                // トリガーキーで初期化
                Keys shortcutKey = CommonUtil.ConvertToEnum<Keys>(keyState.Number);

                // Ctrl
                if (keyState.EnabledCtrl)
                {
                    shortcutKey |= Keys.Control;
                }

                // Shift
                if (keyState.EnabledShift)
                {
                    shortcutKey |= Keys.Shift;
                }

                // Alt
                if (keyState.EnabledAlt)
                {
                    shortcutKey |= Keys.Alt;
                }

                try
                {
                    if (keyState.ShortcutName == Util.SHORTCUT_CAPTURE_NAME)
                    {
                        HotkeyManager.Current.AddOrReplace(keyState.ShortcutName, shortcutKey, true, OnCaptureKey);
                    }
                    else
                    {
                        HotkeyManager.Current.AddOrReplace(keyState.ShortcutName, shortcutKey, OnShortcutKey);
                    }
                }
                catch
                {
                    // 登録できなかったショートカットを無効にして設定ファイルに書き込む
                    ShortcutInfo.ShortcutDt.AsEnumerable()
                        .Where(w => w[ShortcutColumns.ShortcutName].Equals(keyState.ShortcutName))
                        .Select(s => s[ShortcutColumns.IsEnabled] = false).ToList();

                    Util.WriteXml(ShortcutInfo.ShortcutDt, ShortcutInfo.ShortcutFilePath);

                    StringBuilder sb = new StringBuilder();
                    var shortcutJpName = ShortcutInfo.GetShortcutJpName(keyState);
                    sb.AppendLine($"「ショートカット：{shortcutJpName}」は既に他のシステムで使用されているので無効です。");
                    sb.AppendLine("使用したい場合は「システム設定」から該当のショートカットを再設定してください。");
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, sb.ToString());
                }
            }
        }

        /// <summary>
        /// 翻訳後に選択プロセスをアクティブにする
        /// </summary>
        private async void AfterTranslatedActivateSelectedProcess()
        {
            // 対象プロセスをアクティブ
            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
            if ((bool)dr[SettingColumns.UseShortcut] && (bool)dr[SettingColumns.ProcessActivate])
            {
                if (TranslateInfo.FromImgTranslate)
                {
                    // 画像翻訳画面をアクティブ
                    imageList.Show();
                    imageList.Activate();
                    TranslateInfo.FromImgTranslate = false;
                }
                else
                {
                    if (!ProcessNameInfo.DisconnectProcess)
                    {
                        // プロセス接続
                        Thread.Sleep(200);
                        var targetPrcs = Util.GetProcess(ProcessNameInfo.SelectedProcessName);
                        if (targetPrcs != null)
                        {
                            var hndl = UseWinApi.GetWindowHandle(targetPrcs);
                            UseWinApi.WakeupWindow(hndl);

                            if (isStoppedProcess)
                            {
                                protectionFilm.TopMost = true;
                                await Task.Delay(1000);
                                protectionFilm.TopMost = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// フリー選択
        /// </summary>
        private void FreeSelect()
        {
            // タイトルとの排他を行う
            cboLabel.SelectedIndex = -1;

            // フリー選択の場合は初期化する
            doUpdate = false;
            TranslateInfo.Init();

            TranslateInfo.IsFreeSelect = true;

            // スキャンフォーム起動
            RunScanForm();
        }

        /// <summary>
        /// 翻訳
        /// </summary>
        private void Translate()
        {
            if (cboLabel.SelectedIndex >= 0)
            {
                // コンボボックスでアイテムが選択されていた場合
                var id = (int)cboLabel.SelectedValue;
                var dr = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Where(w => w[TitleColumns.Id].Equals(id)).FirstOrDefault();

                Rectangle rectWk = new Rectangle();
                rectWk.X = (int)dr[TitleColumns.X];
                rectWk.Y = (int)dr[TitleColumns.Y];
                rectWk.Width = (int)dr[TitleColumns.Width];
                rectWk.Height = (int)dr[TitleColumns.Height];

                TranslateInfo.TranslateRect = rectWk;
                TranslateInfo.UseOcrEngine = (int)dr[TitleColumns.UseOcrEngine];
                TranslateInfo.ReadMultiples = (float)dr[TitleColumns.ReadMultiples];
            }

            TranslateInfo.IsFreeSelect = false;
            doUpdate = false;

            // スキャンフォーム起動
            RunScanForm();
        }

        /// <summary>
        /// 画像翻訳
        /// </summary>
        private void ImageTranslate()
        {
            string processName = string.Empty;
            if (ProcessNameInfo.DisconnectProcess)
            {
                // 切断中
                if (!Directory.Exists("image"))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "imageフォルダが存在しません。");
                    return;
                }

                var dirArray = Directory.GetDirectories("image");
                if (dirArray.Length == 0)
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                        "imageフォルダ内にプロセスフォルダが一つも存在しません。");
                    return;
                }

                using (var form = new frmSelectImgDir())
                {
                    foreach (var item in dirArray)
                    {
                        form.ImageDirNames.Add(item);
                    }

                    form.ShowDialog();

                    if (string.IsNullOrEmpty(form.SelectedImgDir))
                    {
                        return;
                    }

                    processName = form.SelectedImgDir;
                }
            }
            else
            {
                // 接続中
                var dirPath = $@"image\{ProcessNameInfo.SelectedProcessAliasName}";
                if (!Directory.Exists(dirPath) ||
                    Directory.GetFileSystemEntries(
                        dirPath, "*.png", SearchOption.AllDirectories).Length == 0)
                {
                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Warning, $"「{dirPath}」内に画像ファイルが存在しません。");
                    return;
                }

                processName = ProcessNameInfo.SelectedProcessAliasName;
            }

            if (imageList.ProcessName != processName)
            {
                // プロセス名が変わったら一度閉じる
                imageList.Close();

                // 画面の記憶状態をリセットする
                frmImageList.RestoreWindowStateInfo.Reset();
            }

            if (imageList.IsDisposed)
            {
                imageList = new frmImageList();
                imageList.FormClosed += frmImageList_Closed;
                imageList.ScanFormClosed += ScanForm_Closed;
            }

            isShowImgTranslate = true;
            imageList.ProcessName = processName;
            imageList.Show();
            imageList.Activate();
        }

        /// <summary>
        /// フッター表示制御
        /// </summary>
        /// <param name="visible">表示/非表示</param>
        private void FooterVisibleControl(bool visible)
        {
            btnSelectPrcs.Visible = visible;
            btnRecnctPrcs.Visible = visible;
            btnExit.Visible = visible;

            if (visible)
            {
                // 通常表示
                btnImmediateTrans.Location = new Point(5, 1);
                btnImmediateTrans.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                pnlProcessContainer.Controls.Remove(btnImmediateTrans);
                pnlMiddleCmd.Controls.Add(btnImmediateTrans);

                btnChangeCmdMode.Location = new Point(210, 1);
                btnChangeCmdMode.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                btnPlay.Location = new Point(336, 1);
                btnPlay.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                btnPause.Location = new Point(362, 1);
                btnPause.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                btnStop.Location = new Point(388, 1);
                btnStop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
            else
            {
                // シンプル表示
                btnImmediateTrans.Location = new Point(689, 1);
                btnImmediateTrans.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                pnlMiddleCmd.Controls.Remove(btnImmediateTrans);
                pnlProcessContainer.Controls.Add(btnImmediateTrans);

                btnChangeCmdMode.Location = new Point(563, 1);
                btnChangeCmdMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;

                btnPlay.Location = new Point(482, 1);
                btnPlay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btnPause.Location = new Point(508, 1);
                btnPause.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btnStop.Location = new Point(534, 1);
                btnStop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
        }

        /// <summary>
        /// 自動読み上げ
        /// </summary>
        private void AutoSpeech()
        {
            if ((bool)SystemSettingInfo.SystemSettingDt.Rows[0][SettingColumns.SpeechAuto])
            {
                PlaySpeech();
            }
        }

        /// <summary>
        /// 音声読み上げ
        /// </summary>
        private void PlaySpeech()
        {
            if (string.IsNullOrEmpty(txtResultText.Text))
            {
                return;
            }

            if (isCanceledSpeech)
            {
                // クリアされるので設定を反映し直す
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                speech = new SpeechSynthesizer();
                speech.Volume = (int)dr[SettingColumns.SpeechVolume];
                speech.Rate = (int)dr[SettingColumns.SpeechRate];

                isCanceledSpeech = false;
            }

            if (speech.State == SynthesizerState.Ready)
            {
                speech.SpeakAsync(txtResultText.Text);
            }
            else if (speech.State == SynthesizerState.Paused)
            {
                speech.Resume();
            }
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        private void PauseSpeech()
        {
            if (speech.State == SynthesizerState.Speaking)
            {
                speech.Pause();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        private void StopSpeech()
        {
            try
            {
                speech.SpeakAsyncCancelAll();
                isCanceledSpeech = true;
            }
            catch (OperationCanceledException)
            {
                // この例外はどうやっても出るっぽいので無視する
            }
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        /// <returns>true:実行OK　false:実行NG</returns>
        private bool RunCommand()
        {
            try
            {
                TranslateInfo.FromRunCmd = false;

                if (!File.Exists(CMD_FILE_NAME))
                {
                    return false;
                }

                using (StreamReader sr = new StreamReader(CMD_FILE_NAME))
                {
                    var values = sr.ReadLine().Split(',');

                    if (values.Length != 2)
                    {
                        return false;
                    }

                    int num = 0;
                    if (!int.TryParse(values[1].Trim(), out num))
                    {
                        return false;
                    }

                    switch (values[0].Trim())
                    {
                        case "tran":
                            if (num < -1)
                            {
                                // -2以下は不正値
                                return false;
                            }

                            TranslateInfo.FromRunCmd = true;

                            // 翻訳系コマンド
                            switch (num)
                            {
                                case -1:
                                    // フリー選択
                                    FreeSelect();
                                    break;
                                case 0:
                                    // 翻訳
                                    Translate();
                                    break;
                                default:
                                    // 固定翻訳1～
                                    if (num > cboLabel.Items.Count)
                                    {
                                        // 存在しないアイテムの場合はフリー選択
                                        FreeSelect();
                                    }
                                    else
                                    {
                                        // 存在する場合はコンボボックスに値を反映
                                        cboLabel.SelectedIndex = num - 1;
                                        Translate();
                                    }
                                    break;
                            }

                            break;
                        case "spch":
                            // 読み上げ系コマンド
                            switch (num)
                            {
                                case 0:
                                    // 再生
                                    PlaySpeech();
                                    break;
                                case 1:
                                    // 一時停止
                                    PauseSpeech();
                                    break;
                                case 2:
                                    // 停止
                                    StopSpeech();
                                    break;
                                default:
                                    // 上記0～2以外は不正値
                                    return false;
                            }

                            break;
                        default:
                            // コマンド種別不正
                            return false;
                    }

                    return true;
                }
            }
            finally
            {
                if (File.Exists(CMD_FILE_NAME))
                {
                    var file = new FileInfo(CMD_FILE_NAME);
                    if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        file.Attributes = FileAttributes.Normal;
                    }

                    file.Delete();
                }
            }
        }
        #endregion
    }
}
