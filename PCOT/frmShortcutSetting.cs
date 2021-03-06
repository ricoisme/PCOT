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
using NHotkey;
using NHotkey.WindowsForms;

namespace PCOT
{
    public partial class frmShortcutSetting : Form
    {
        #region 変数
        /// <summary>辞書リスト表示用データテーブル</summary>
        private DataTable shortcutDispDt = new DataTable();
        /// <summary>オリジナル比較用データテーブル</summary>
        private DataTable shortcutOrgDt = new DataTable();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmShortcutSetting()
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
        private void frmShortcutSetting_Load(object sender, EventArgs e)
        {
            try
            {
                // データグリッドビューの列ヘッダーを改行させない
                dgvShortcutList.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

                // コンテキストメニューのアイテム設定
                SetTriggerKeyContextMenuItem();

                // ショートカット設定ファイルの存在確認
                if (!File.Exists(ShortcutInfo.ShortcutFilePath))
                {
                    Util.WriteXml(ShortcutInfo.ShortcutDt, ShortcutInfo.ShortcutFilePath);
                }

                DataTable dt = new DataTable();

                // ショートカットファイル読み込み
                dt.ReadXml(ShortcutInfo.ShortcutFilePath);

                SetDispDataTable(dt);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 有効選択/解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsEnabledAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[IsEnabled.Name].Value = chkIsEnabledAll.Checked).ToList();

                dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Where(w => w.Cells[IsEnabled.Name].Value.Equals(false))
                    .Select(s => s.DefaultCellStyle.BackColor = Color.LightGray).ToList();

                dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Where(w => w.Cells[IsEnabled.Name].Value.Equals(true))
                    .Select(s => s.DefaultCellStyle.BackColor = Color.Empty).ToList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// CTRL選択/解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCtrlAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[EnabledCtrl.Name].Value = chkCtrlAll.Checked).ToList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// SHIFT選択/解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShiftAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[EnabledShift.Name].Value = chkShiftAll.Checked).ToList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ALT選択/解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAltAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[EnabledAlt.Name].Value = chkAltAll.Checked).ToList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// セルの値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShortcutList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvShortcutList.Rows.Count == 0)
            {
                return;
            }

            if (dgvShortcutList.Columns[IsEnabled.Name].Index == e.ColumnIndex)
            {
                if (dgvShortcutList[e.ColumnIndex, e.RowIndex].Value.Equals(false))
                {
                    dgvShortcutList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
                else
                {
                    dgvShortcutList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
        }

        /// <summary>
        /// セルの変更を即時反映
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShortcutList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                var dgv = (DataGridView)sender;
                if (dgv.IsCurrentCellDirty)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 右クリックメニューセル選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShortcutList_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                int dummy = 0;
                var rowIndexAry = dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Where(w => !int.TryParse(w.Cells[DispNumber.Name].Value.ToString(), out dummy))
                    .Select(s => s.Cells[DispNumber.Name].RowIndex).ToArray();

                if (e.ColumnIndex == dgvShortcutList.Columns[DispNumber.Name].Index)
                {
                    if (rowIndexAry.Any(a => a == e.RowIndex))
                    {
                        dgvShortcutList.ClearSelection();
                        var cell = dgvShortcutList[e.ColumnIndex, e.RowIndex];
                        cell.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 右クリックメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsTriggerKey_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = dgvShortcutList.SelectedCells.Cast<DataGridViewCell>()
                    .FirstOrDefault().RowIndex;

                var menuText = ((ToolStripMenuItem)sender).Text;

                if (dgvShortcutList.Rows.Cast<DataGridViewRow>()
                    .Any(a => a.Index != rowIndex && a.Cells[DispNumber.Name].Value.Equals(menuText)))
                {
                    // 重複する場合はエラー
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "トリガーキーは重複できません。");
                    return;
                }

                // 名称とキーコードの両方を設定
                dgvShortcutList[DispNumber.Name, rowIndex].Value = menuText;

                foreach (Keys value in Enum.GetValues(typeof(Keys)))
                {
                    var keyName = Enum.GetName(typeof(Keys), value);
                    if (menuText == keyName)
                    {
                        dgvShortcutList[Number.Name, rowIndex].Value = value;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// リセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = Util.CreateShortcutDataTable();
                SetDispDataTable(dt, true);
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
                // オリジナルデータと比較
                if (Util.IsSameValue(shortcutOrgDt, shortcutDispDt))
                {
                    // データが同じだったら閉じる
                    Close();
                    return;
                }

                // 確認メッセージ
                var result = CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                    "編集されたデータが存在します。保存しますか？", true);

                switch (result)
                {
                    case DialogResult.Yes:
                        // 入力チェック
                        var requiredErr = shortcutDispDt.AsEnumerable()
                            .Where(w =>
                            w[ShortcutDispColumns.IsEnabled].Equals(true) &&
                            w[ShortcutDispColumns.EnabledCtrl].Equals(false) &&
                            w[ShortcutDispColumns.EnabledShift].Equals(false) &&
                            w[ShortcutDispColumns.EnabledAlt].Equals(false)
                            )
                            .Select(s => new { err = true, index = shortcutDispDt.Rows.IndexOf(s) })
                            .FirstOrDefault();

                        if (requiredErr != null && requiredErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                                "装飾キーはどれか一つを必ず指定してください。");
                            dgvShortcutList.ClearSelection();
                            dgvShortcutList.FirstDisplayedScrollingRowIndex = requiredErr.index;
                            dgvShortcutList[EnabledCtrl.Name, requiredErr.index].Selected = true;
                            return;
                        }

                        // ショートカット登録チェック
                        for (int i = 0; i < shortcutDispDt.Rows.Count; i++)
                        {
                            if (!(bool)shortcutDispDt.Rows[i][ShortcutColumns.IsEnabled])
                            {
                                continue;
                            }

                            var shortcutName = shortcutDispDt
                                .Rows[i][ShortcutDispColumns.ShortcutName].ToString();

                            var shortcutDispName = shortcutDispDt
                                .Rows[i][ShortcutDispColumns.ShortcutDispName].ToString();

                            Keys keys = CommonUtil.ConvertToEnum<Keys>(
                                (int)shortcutDispDt.Rows[i][ShortcutDispColumns.Number]);

                            if ((bool)shortcutDispDt.Rows[i][ShortcutDispColumns.EnabledCtrl])
                            {
                                keys |= Keys.Control;
                            }

                            if ((bool)shortcutDispDt.Rows[i][ShortcutDispColumns.EnabledShift])
                            {
                                keys |= Keys.Shift;
                            }

                            if ((bool)shortcutDispDt.Rows[i][ShortcutDispColumns.EnabledAlt])
                            {
                                keys |= Keys.Alt;
                            }

                            try
                            {
                                // ダミー登録
                                HotkeyManager.Current.AddOrReplace(
                                    shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutName].ToString(),
                                    keys, OnDummy);
                            }
                            catch
                            {
                                // 登録失敗
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine($"「{shortcutDispName}」は既に別のシステムで登録されています。");
                                sb.AppendLine("別の組み合わせを登録してください。");
                                var errMessage = sb.ToString();

                                CommonUtil.PutMessage(CommonEnums.MessageType.Error, errMessage);
                                return;
                            }
                            finally
                            {
                                // 登録に成功しても失敗しても削除する
                                HotkeyManager.Current.Remove(shortcutDispDt
                                    .Rows[i][ShortcutDispColumns.ShortcutName].ToString());
                            }
                        }

                        // 保存処理
                        var shortcutColumns = Util.CreateShortcutDataTable()
                            .Columns.Cast<DataColumn>()
                            .Select(s => s.ColumnName).ToArray();

                        // 書き込み用に列を抽出
                        var dt = shortcutDispDt.DefaultView
                            .ToTable(Util.SHORTCUT_TABLE, false, shortcutColumns);

                        // 出力
                        Util.WriteXml(dt, ShortcutInfo.ShortcutFilePath);
                        ShortcutInfo.ShortcutDt = dt;

                        Close();
                        break;
                    case DialogResult.No:
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ホットキー登録確認用ダミーメソッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDummy(object sender, HotkeyEventArgs e)
        {
            // ダミーメソッドなので何もしない
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 表示用データテーブルにデータを設定(バインドも同時に行う)
        /// </summary>
        /// <param name="dt">対象データ</param>
        /// <param name="reset">リセットフラグ</param>
        private void SetDispDataTable(DataTable dt, bool reset = false)
        {
            shortcutDispDt = Util.CreateShortcutDispDataTable();

            // それぞれのデータテーブルにキーを設定(Number)
            dt.PrimaryKey = new DataColumn[] { dt.Columns[ShortcutColumns.Number] };
            shortcutDispDt.PrimaryKey = new DataColumn[] { shortcutDispDt.Columns[ShortcutDispColumns.Number] };

            // 二つのデータテーブルをマージ
            shortcutDispDt.Merge(dt);

            // ショートカット名と番号を表示用に直す
            for (int i = 0; i < shortcutDispDt.Rows.Count; i++)
            {
                // ショートカット名を表示用に変換
                switch (i)
                {
                    case 0:
                        // 翻訳ボタン
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_TRANSLATE_JPNAME;
                        break;
                    case 10:
                        // フリー選択
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_FREE_JPNAME;
                        break;
                    case 11:
                        // 画像翻訳
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_IMAGE_JPNAME;
                        break;
                    case 12:
                        // ｸﾘｯﾌﾟﾎﾞｰﾄﾞ翻訳
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_CLIPBOARD_JPNAME;
                        break;
                    case 13:
                        // 画面ｷｬﾌﾟﾁｬｰ
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_CAPTURE_JPNAME;
                        break;
                    case 14:
                        // プロセス停止/再開
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_PROCESS_STOP_START_JPNAME;
                        break;
                    default:
                        // 固定翻訳
                        shortcutDispDt.Rows[i][ShortcutDispColumns.ShortcutDispName] =
                            Util.SHORTCUT_FIXEDTRANSLATE_JPNAME + i;
                        break;
                }

                // キーコードからアルファベットのみを抽出
                var az = Enum.GetValues(typeof(Keys)).Cast<Keys>()
                    .Where(w => (int)w >= (int)Keys.A && (int)w <= (int)Keys.Z).ToList();


                bool isExists = false;
                foreach (var item in az)
                {
                    var value = Enum.GetValues(typeof(Keys)).Cast<Keys>()
                        .Where(w => w == item)
                        .Select(s => (int)s)
                        .FirstOrDefault();

                    if ((int)shortcutDispDt.Rows[i][ShortcutDispColumns.Number] == value)
                    {
                        // 番号を表示用に変換
                        shortcutDispDt.Rows[i][ShortcutDispColumns.DispNumber] = item.ToString();
                        isExists = true;
                        break;
                    }
                }

                // アルファベットが存在しなかった場合
                if (!isExists)
                {
                    // 番号を表示用に変換
                    shortcutDispDt.Rows[i][ShortcutDispColumns.DispNumber] = i;
                }
            }

            // 閉じる時の比較用にオリジナルデータを退避
            if (!reset)
            {
                shortcutOrgDt = shortcutDispDt.Copy();
            }

            // バインド
            dgvShortcutList.DataSource = shortcutDispDt;

            // バインドの前に特定のセルに対してコンテキストメニューを紐づける
            int dummy = 0;
            dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .Where(w => !int.TryParse(w.Cells[DispNumber.Name].Value.ToString(), out dummy))
                .Select(s => s.Cells[DispNumber.Name].ContextMenuStrip = cmsTriggerKey).ToList();

            // 編集可能なセルの背景色を変更
            dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .Where(w => !int.TryParse(w.Cells[DispNumber.Name].Value.ToString(), out dummy))
                .Select(s => s.Cells[DispNumber.Name].Style.BackColor = Color.LightCyan).ToList();

            // 初期チェック状態の同期
            if (dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .All(a => a.Cells[IsEnabled.Name].Value.Equals(true)))
            {
                chkIsEnabledAll.Checked = true;
            }
            else
            {
                chkIsEnabledAll.Checked = false;
            }

            // 無効のものは背景色を設定
            dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .Where(w => w.Cells[IsEnabled.Name].Value.Equals(false))
                .Select(s => s.DefaultCellStyle.BackColor = Color.LightGray).ToList();

            if (dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .All(a => a.Cells[EnabledCtrl.Name].Value.Equals(true)))
            {
                chkCtrlAll.Checked = true;
            }
            else
            {
                chkCtrlAll.Checked = false;
            }

            if (dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .All(a => a.Cells[EnabledShift.Name].Value.Equals(true)))
            {
                chkShiftAll.Checked = true;
            }
            else
            {
                chkShiftAll.Checked = false;
            }

            if (dgvShortcutList.Rows.Cast<DataGridViewRow>()
                .All(a => a.Cells[EnabledAlt.Name].Value.Equals(true)))
            {
                chkAltAll.Checked = true;
            }
            else
            {
                chkAltAll.Checked = false;
            }
        }

        /// <summary>
        /// トリガーキーのコンテキストメニューアイテム設定
        /// </summary>
        private void SetTriggerKeyContextMenuItem()
        {
            cmsTriggerKey.Items.Clear();
            char[] az = Enumerable.Range('A', 'Z' - 'A' + 1).Select(s => (char)s).ToArray();
            foreach (var c in az)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = c.ToString();
                item.Click += cmsTriggerKey_Click;
                cmsTriggerKey.Items.Add(item);
            }
        }
        #endregion
    }
}
