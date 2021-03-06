using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCOT
{
    public class SystemSettingInfo
    {
        #region 定数
        /// <summary>PCOT設定ファイルパス</summary>
        private const string PCOT_SETTING_PATH = @"data\PCOT_Setting.cfg";
        /// <summary>PCOTフォント設定ファイルパス</summary>
        private const string FONT_SETTING_PATH = @"data\PCOT_FontSetting.cfg";
        #endregion

        #region プロパティ
        /// <summary>システム設定ファイルパス</summary>
        public static string SystemSettingFilePath => PCOT_SETTING_PATH;
        public static string FontSettingFilePath => FONT_SETTING_PATH;
        #endregion

        #region 変数
        /// <summary>システム設定データテーブル</summary>
        public static DataTable SystemSettingDt = Util.CreatePcotSettingTable();
        /// <summary>フォント設定データテーブル</summary>
        public static DataTable FontSettingDt = Util.CreateFontSettingTable();
        #endregion
    }
}
