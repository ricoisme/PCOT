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
    public partial class frmNounList : Form
    {
        #region 定数
        /// <summary>プロセスタイトル</summary>
        private const string TITLE = "：{0}";
        #endregion

        #region 変数
        /// <summary>編集用データテーブル</summary>
        private DataTable nounDispDt = new DataTable();
        /// <summary>オリジナル比較用データテーブル</summary>
        private DataTable nounOrgDt = new DataTable();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmNounList()
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
        private void frmNounList_Load(object sender, EventArgs e)
        {
            try
            {
                if (ProcessNameInfo.DisconnectProcess)
                {
                    // 切断中
                    Text += string.Format(TITLE, TranslateInfo.PCOT_DESKTOP_CONNECT);
                }
                else
                {
                    // 接続中
                    Text += string.Format(TITLE, ProcessNameInfo.SelectedProcessAliasName);
                }

                dgvNounList.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

                if (TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].Rows.Count == 0)
                {
                    // データが存在しない場合は処理を中断
                    return;
                }

                // 設定ファイルが存在する場合は読み込む
                TranslateInfo.SettingDs.Clear();
                if (ProcessNameInfo.DisconnectProcess)
                {
                    // 切断中
                    TranslateInfo.SettingDs.ReadXml(TranslateInfo.DesktopConfigFilePath);
                }
                else
                {
                    // 接続中
                    TranslateInfo.SettingDs.ReadXml(TranslateInfo.ConfigFilePath);
                }

                // 表示用データテーブルに列定義を作成
                nounDispDt = Util.CreateNounDispDataTable();

                // それぞれのデータテーブルにキーを設定(Id)
                TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].PrimaryKey =
                    new DataColumn[] { TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].Columns[NounColumns.Id] };
                nounDispDt.PrimaryKey = new DataColumn[] { nounDispDt.Columns[NounDispColumns.Id] };

                // 二つのデータテーブルをマージ
                nounDispDt.Merge(TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE]);

                // ID順にソート
                nounDispDt = nounDispDt.AsEnumerable()
                    .OrderBy(o => (int)o[NounDispColumns.Id]).ToArray().CopyToDataTable();
                nounDispDt.TableName = Util.NOUN_DISP_TABLE;

                // 閉じる時の比較用にオリジナルデータを退避
                nounOrgDt = nounDispDt.Copy();

                // バインド
                dgvNounList.DataSource = nounDispDt;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNounList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == dgvNounList.Columns[Delete.Name].Index)
                {
                    // 削除ボタン
                    if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                        "対象データを削除します。よろしいですか？") == DialogResult.No)
                    {
                        // いいえの場合は処理を中断
                        return;
                    }

                    // 削除
                    dgvNounList.Rows.RemoveAt(e.RowIndex);
                    nounDispDt.AcceptChanges();
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
                if (Util.IsSameValue(nounOrgDt, nounDispDt))
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
                        var beforeRequiredErr = nounDispDt.AsEnumerable()
                            .Where(w => string.IsNullOrEmpty(w[NounDispColumns.BeforeNoun].ToString()))
                            .Select(s => new { err = true, index = nounDispDt.Rows.IndexOf(s) }).FirstOrDefault();

                        if (beforeRequiredErr != null && beforeRequiredErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, "置換前名詞の入力は必須です。");
                            dgvNounList.ClearSelection();
                            dgvNounList.FirstDisplayedScrollingRowIndex = beforeRequiredErr.index;
                            dgvNounList[BeforeNoun.Name, beforeRequiredErr.index].Selected = true;
                            return;
                        }

                        var aftereRequiredErr = nounDispDt.AsEnumerable()
                            .Where(w => string.IsNullOrEmpty(w[NounDispColumns.AfterNoun].ToString()))
                            .Select(s => new { err = true, index = nounDispDt.Rows.IndexOf(s) }).FirstOrDefault();

                        if (aftereRequiredErr != null && aftereRequiredErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, "置換後名詞の入力は必須です。");
                            dgvNounList.ClearSelection();
                            dgvNounList.FirstDisplayedScrollingRowIndex = aftereRequiredErr.index;
                            dgvNounList[AfterNoun.Name, aftereRequiredErr.index].Selected = true;
                            return;
                        }

                        // 保存処理
                        var nounColumns = Util.CreateNounDataTable()
                            .Columns.Cast<DataColumn>()
                            .Select(s => s.ColumnName).ToArray();

                        // 書き込み用に列を抽出
                        TranslateInfo.SettingDs.Tables.Remove(Util.NOUN_TABLE);
                        var dt = nounDispDt.DefaultView.ToTable(Util.NOUN_TABLE, false, nounColumns);
                        TranslateInfo.SettingDs.Tables.Add(dt);

                        // タイトルテーブルも名詞テーブルもデータが存在しない場合
                        if (TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Rows.Count == 0 &&
                            TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE].Rows.Count == 0)
                        {
                            // ファイルが存在する場合はファイルを削除
                            if (File.Exists(TranslateInfo.ConfigFilePath))
                            {
                                File.Delete(TranslateInfo.ConfigFilePath);
                            }
                        }
                        else
                        {
                            // 出力
                            Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                        }

                        Close();
                        break;
                    case DialogResult.No:
                        // 保存せずに終了
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion
    }
}
