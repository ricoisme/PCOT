using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCOT
{
    public class ProcessNameInfo
    {
        #region 定数
        /// <summary>PCOTプロセス管理ファイルパス</summary>
        private const string PCOT_PROCESS_MGR_PATH = @"data\PCOT_ProcessManager.mgr";
        #endregion

        #region プロパティ
        /// <summary>PCOTプロセス管理ファイルパス</summary>
        public static string ProcessManagerFilePath => PCOT_PROCESS_MGR_PATH;
        #endregion

        #region 変数
        /// <summary>現在選択中のプロセス名</summary>
        public static string SelectedProcessName;
        /// <summary>現在選択中のプロセス別名</summary>
        public static string SelectedProcessAliasName;
        /// <summary>プロセス切断</summary>
        public static bool DisconnectProcess = false;
        /// <summary>プロセス名データテーブル</summary>
        public static DataTable ProcessNameDt = Util.CreateProcessNameDataTable();
        #endregion

        #region メソッド
        /// <summary>
        /// プロセス名登録
        /// </summary>
        public static void RegisterProessName()
        {
            if (!ProcessNameDt.AsEnumerable()
                .Any(a => a[ProcessNameColumns.ProcessAliasName]
                .Equals(SelectedProcessAliasName)))
            {
                // 出力
                var dr = ProcessNameDt.NewRow();
                dr[ProcessNameColumns.ProcessName] = SelectedProcessName;
                dr[ProcessNameColumns.ProcessAliasName] = SelectedProcessAliasName;
                dr[ProcessNameColumns.IsAliasProcess] = SelectedProcessName != SelectedProcessAliasName;
                ProcessNameDt.Rows.Add(dr);

                Util.WriteXml(ProcessNameDt, ProcessManagerFilePath);
            }
        }

        /// <summary>
        /// プロセス名削除
        /// </summary>
        public static void DeleteProcessName()
        {
            var item = ProcessNameDt.AsEnumerable()
                .Where(w => w[ProcessNameColumns.ProcessAliasName]
                .Equals(SelectedProcessAliasName)).FirstOrDefault();

            if (item != null)
            {
                // 出力
                ProcessNameDt.Rows.Remove(item);
                Util.WriteXml(ProcessNameDt, ProcessManagerFilePath);
            }
        }

        #endregion
    }
}
