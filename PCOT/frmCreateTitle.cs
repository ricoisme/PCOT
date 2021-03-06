using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmCreateTitle : Form
    {
        #region 定数
        /// <summary>ラベルタイトル設定結果</summary>
        public enum LabelResult
        {
            Create,
            Edit,
            Close
        }

        /// <summary>OCRエンジン：Windows10</summary>
        private const string WINDOWS10_OCR_NAME = "Windows10 OCR";
        /// <summary>OCRエンジン：Tesseract</summary>
        private const string TESSERACT_OCR_NAME = "Tesseract OCR";
        #endregion

        #region 変数
        /// <summary>ラベルタイトル設定結果</summary>
        public LabelResult Result;
        /// <summary>選択ID</summary>
        public int SelectedId = -1;
        /// <summary>ロードフラグ</summary>
        public bool isLoad = true;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmCreateTitle()
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
        private void frmCreateTitle_Load(object sender, EventArgs e)
        {
            try
            {
                // ラベル初期化
                lblX.Text = "0";
                lblY.Text = "0";
                lblW.Text = "0";
                lblH.Text = "0";

                SetUseOcrEngineComboBoxItem();

                // タイトル情報ファイルを読み込む
                if (ProcessNameInfo.DisconnectProcess)
                {
                    if (!File.Exists(TranslateInfo.DesktopConfigFilePath))
                    {
                        // 設定ファイルが存在しない場合は新しく作成する
                        TranslateInfo.SettingDs =
                            Util.CreateSettingDataSet(TranslateInfo.PCOT_DESKTOP_CONNECT);

                        var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                        if ((int)dr[SettingColumns.UsingOcrEngine] == 0)
                        {
                            nudReadMultiples.Value = 3m;
                        }
                        else
                        {
                            nudReadMultiples.Value = 2m;
                        }

                        cboUseOcrEngine.SelectedValue = (int)dr[SettingColumns.UsingOcrEngine];

                        return;
                    }

                    TranslateInfo.SettingDs.Clear();
                    TranslateInfo.SettingDs.ReadXml(TranslateInfo.DesktopConfigFilePath);
                }
                else
                {
                    if (!File.Exists(TranslateInfo.ConfigFilePath))
                    {
                        // 設定ファイルが存在しない場合は新しく作成する
                        TranslateInfo.SettingDs =
                            Util.CreateSettingDataSet(ProcessNameInfo.SelectedProcessAliasName);

                        var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                        if ((int)dr[SettingColumns.UsingOcrEngine] == 0)
                        {
                            nudReadMultiples.Value = 3m;
                        }
                        else
                        {
                            nudReadMultiples.Value = 2m;
                        }

                        cboUseOcrEngine.SelectedValue = (int)dr[SettingColumns.UsingOcrEngine];

                        return;
                    }

                    TranslateInfo.SettingDs.Clear();
                    TranslateInfo.SettingDs.ReadXml(TranslateInfo.ConfigFilePath);
                }

                cboLabel.DataSource = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE];
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// コンボボックス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLabel.SelectedIndex == -1)
                {
                    return;
                }

                // タイトル
                txtLabel.Text = ((DataRowView)cboLabel.SelectedItem)[TitleColumns.Title].ToString();

                // 使用OCRエンジン
                cboUseOcrEngine.SelectedValue = 
                    (int)((DataRowView)cboLabel.SelectedItem)[TitleColumns.UseOcrEngine];

                // 読取倍率
                nudReadMultiples.Value =
                    Convert.ToDecimal(((DataRowView)cboLabel.SelectedItem)[TitleColumns.ReadMultiples]);

                // 翻訳範囲
                lblX.Text = ((DataRowView)cboLabel.SelectedItem)[TitleColumns.X].ToString();
                lblY.Text = ((DataRowView)cboLabel.SelectedItem)[TitleColumns.Y].ToString();
                lblW.Text = ((DataRowView)cboLabel.SelectedItem)[TitleColumns.Width].ToString();
                lblH.Text = ((DataRowView)cboLabel.SelectedItem)[TitleColumns.Height].ToString();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// UpDownボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudReadMultiples_UpDown(object sender, ExtendedNumericUpDown.UpDownEventArgs e)
        {
            try
            {
                if (e.IsUp)
                {
                    if (nudReadMultiples.Value < 1)
                    {
                        // 1未満の場合は0.1ずつ増加
                        nudReadMultiples.Value += 0.1m;
                        e.Handled = true;
                    }
                }
                else
                {
                    if (nudReadMultiples.Value <= 1 && nudReadMultiples.Value > 0.1m)
                    {
                        // 1以下の場合は0.1ずつ減少
                        nudReadMultiples.Value -= 0.1m;
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // 未入力
                if (string.IsNullOrEmpty(txtLabel.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "タイトルを入力してください。");
                    return;
                }

                // 重複
                if (TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Rows.Count > 0)
                {
                    if (TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                        .Any(a => a[TitleColumns.Title].ToString() == txtLabel.Text))
                    {
                        CommonUtil.PutMessage(
                            CommonEnums.MessageType.Error, "入力したタイトルはすでに存在します。");
                        return;
                    }
                }

                // 採番
                var Ids = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Select(s => (int)s[TitleColumns.Id]).ToArray();
                var newId = CommonUtil.GetSerialNumber(Ids);

                // データ作成
                var dr = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].NewRow();
                dr[TitleColumns.Id] = newId;
                dr[TitleColumns.Title] = txtLabel.Text;
                dr[TitleColumns.UseOcrEngine] = (int)cboUseOcrEngine.SelectedValue;
                dr[TitleColumns.ReadMultiples] = decimal.ToSingle(nudReadMultiples.Value);
                dr[TitleColumns.X] = 0;
                dr[TitleColumns.Y] = 0;
                dr[TitleColumns.Width] = 0;
                dr[TitleColumns.Height] = 0;
                dr[TitleColumns.TargetReturn] = false;
                dr[TitleColumns.IgnoreReturn] = false;
                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Rows.Add(dr);

                var sortedDt = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .OrderBy(o => o[TitleColumns.Title]).ToArray().CopyToDataTable();

                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Clear();

                foreach (DataRow row in sortedDt.Rows)
                {
                    TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].ImportRow(row);
                }

                // 出力
                if (ProcessNameInfo.DisconnectProcess)
                {
                    // デスクトップ接続
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.DesktopConfigFilePath);
                }
                else
                {
                    // 対象プロセス
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                    ProcessNameInfo.RegisterProessName();
                }

                SelectedId = newId;
                TranslateInfo.UseOcrEngine = (int)cboUseOcrEngine.SelectedValue;
                TranslateInfo.ReadMultiples = decimal.ToSingle(nudReadMultiples.Value);
                Result = LabelResult.Create;
                Close();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // 件数確認
                Result = LabelResult.Close;
                if (cboLabel.Items.Count == 0)
                {
                    return;
                }

                // 未入力
                if (string.IsNullOrEmpty(txtLabel.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "タイトルを入力してください。");
                    return;
                }

                // 重複
                var id = (int)((DataRowView)cboLabel.SelectedItem)[TitleColumns.Id];
                var preTitle = ((DataRowView)cboLabel.SelectedItem)[TitleColumns.Title].ToString();
                var currentTitle = txtLabel.Text.Trim();
                var ocrEngine = (int)cboUseOcrEngine.SelectedValue;
                var readMultiples = decimal.ToSingle(nudReadMultiples.Value);

                if (TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Any(
                    a => a[TitleColumns.Title].ToString() != preTitle &&
                    a[TitleColumns.Title].ToString() == currentTitle))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "入力したタイトルはすでに存在します。");
                    return;
                }

                // 編集
                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Where(w => (int)w[TitleColumns.Id] == id)
                    .Select(s => s[TitleColumns.UseOcrEngine] = ocrEngine).ToList();

                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Where(w => (int)w[TitleColumns.Id] == id)
                    .Select(s => s[TitleColumns.ReadMultiples] = readMultiples).ToList();

                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .Where(w => w[TitleColumns.Title].ToString() == preTitle)
                    .Select(s => s[TitleColumns.Title] = currentTitle).ToList();

                var sortedDt = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].AsEnumerable()
                    .OrderBy(o => o[TitleColumns.Title]).ToArray().CopyToDataTable();

                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Clear();

                foreach (DataRow row in sortedDt.Rows)
                {
                    TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].ImportRow(row);
                }

                // 出力
                if (ProcessNameInfo.DisconnectProcess)
                {
                    // デスクトップ接続
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.DesktopConfigFilePath);
                }
                else
                {
                    // 対象プロセス
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                }

                // 範囲修正確認
                if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                    "翻訳範囲を編集しますか？") == DialogResult.Yes)
                {
                    SelectedId = id;
                    TranslateInfo.UseOcrEngine = (int)cboUseOcrEngine.SelectedValue;
                    TranslateInfo.ReadMultiples = decimal.ToSingle(nudReadMultiples.Value);
                    Result = LabelResult.Edit;
                    Close();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 件数確認
                if (cboLabel.Items.Count == 0)
                {
                    // コンボボックスに価が存在しない場合は中断
                    return;
                }

                // 削除確認
                if (CommonUtil.PutMessage(
                    CommonEnums.MessageType.Confirm,
                    "選択中の項目を削除します。\n設定されたデータが全て削除されます。よろしいですか？") ==
                    DialogResult.No)
                {
                    // 削除しない場合は中断
                    return;
                }

                // アイテムを削除
                TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE]
                    .Rows.Remove(((DataRowView)cboLabel.SelectedItem).Row);
                txtLabel.Text = string.Empty;
                nudReadMultiples.Value = 2;
                lblX.Text = "0";
                lblY.Text = "0";
                lblW.Text = "0";
                lblH.Text = "0";

                // タイトルテーブルか名詞テーブルのどちらかに1件でもデータが存在する場合
                if (TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Rows.Count > 0 ||
                    TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].Rows.Count > 0)
                {
                    // 削除後もアイテムが残っている場合は設定ファイルを更新
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                }
                else
                {
                    // アイテムが1件も存在しない場合は設定ファイルごと削除
                    File.Delete(TranslateInfo.ConfigFilePath);
                    if (!Util.IsExistsProcessData())
                    {
                        ProcessNameInfo.DeleteProcessName();
                    }
                }

                // 他にアイテムが存在する場合は先頭を選択
                if (cboLabel.Items.Count > 0)
                {
                    cboLabel.SelectedIndex = -1;
                    cboLabel.SelectedIndex = 0;
                }
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
                Result = LabelResult.Close;
                Close();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 使用OCRエンジンコンボボックスアイテム設定
        /// </summary>
        private void SetUseOcrEngineComboBoxItem()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(cboUseOcrEngine.ValueMember, typeof(int));
            dt.Columns.Add(cboUseOcrEngine.DisplayMember, typeof(string));

            if (Util.IsEnabledWindows10OcrWithWindowsVersion())
            {
                if (Util.IsEnabledWindows10Ocr())
                {
                    dr = dt.NewRow();
                    dr[cboUseOcrEngine.ValueMember] = 0;
                    dr[cboUseOcrEngine.DisplayMember] = WINDOWS10_OCR_NAME;
                    dt.Rows.Add(dr);
                }
            }

            dr = dt.NewRow();
            dr[cboUseOcrEngine.ValueMember] = 1;
            dr[cboUseOcrEngine.DisplayMember] = TESSERACT_OCR_NAME;
            dt.Rows.Add(dr);

            cboUseOcrEngine.DataSource = dt;

            // コンボボックスの初期選択
            if (cboUseOcrEngine.Items.Count > 1)
            {
                cboUseOcrEngine.SelectedIndex = 1;
            }
            else
            {
                cboUseOcrEngine.SelectedIndex = 0;
            }
        }
        #endregion
    }
}
