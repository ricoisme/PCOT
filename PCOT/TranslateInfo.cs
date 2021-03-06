using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCOT
{
    public class TranslateInfo
    {
        #region 定数
        /// <summary>コンフィグパスひな型</summary>
        private const string CONFIG_PATH_TEMP = @"data\{0}.cfg";
        /// <summary>デスクトップ接続名</summary>
        public const string PCOT_DESKTOP_CONNECT = "PcotDesktopConnect";
        #endregion

        #region プロパティ
        /// <summary>デスクトップ設定ファイルパス</summary>
        public static string DesktopConfigFilePath => $@"data\{PCOT_DESKTOP_CONNECT}.cfg";
        /// <summary>設定ファイルパス</summary>
        public static string ConfigFilePath { get; set; } = string.Empty;
        /// <summary>原文</summary>
        public static string OriginalText { get; set; } = string.Empty;
        /// <summary>翻訳翻訳範囲</summary>
        public static Rectangle TranslateRect { get; set; } = new Rectangle();
        /// <summary>原文通りに改行</summary>
        public static bool ToReturnOrginal { get; set; } = false;

        /// <summary>改行を無視</summary>
        public static bool ToIgnoreReturn { get; set; } = false;

        /// <summary>使用OCRエンジン（デフォルト：1）</summary>
        public static int UseOcrEngine { get; set; } = 1;

        /// <summary>読取倍率（デフォルト：2）</summary>
        public static float ReadMultiples { get; set; } = 2;
        /// <summary>スキャンフォーム表示フラグ</summary>
        public static bool ShowScanForm { get; set; } = false;

        /// <summary>フリー選択フラグ</summary>
        public static bool IsFreeSelect { get; set; } = false;
        /// <summary>画像翻訳からの翻訳</summary>
        public static bool FromImgTranslate { get; set; } = false;
        /// <summary>コマンドからの翻訳</summary>
        public static bool FromRunCmd { get; set; } = false;
        #endregion

        #region 変数
        /// <summary>設定格納用データセット</summary>
        public static DataSet SettingDs = new DataSet();
        #endregion

        #region メソッド
        /// <summary>
        /// 初期化
        /// </summary>
        public static void Init()
        {
            OriginalText = string.Empty;
            TranslateRect = new Rectangle();
            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
            UseOcrEngine = (int)dr[SettingColumns.UsingOcrEngine];

            if ((int)dr[SettingColumns.UsingOcrEngine] == 0)
            {
                // Windows10 OCRはデフォルト3倍
                ReadMultiples = 3;
            }
            else
            {
                // Tesseractはデフォルト2倍
                ReadMultiples = 2;
            }
        }

        /// <summary>
        /// 設定ファイルパスを取得
        /// </summary>
        /// <returns>設定ファイルパス</returns>
        public static string GetConfigFilePath()
        {
            return string.Format(CONFIG_PATH_TEMP, ProcessNameInfo.SelectedProcessAliasName);
        }

        /// <summary>
        /// デスクトップ接続設定ファイルパスを取得
        /// </summary>
        /// <param name="desktopConnectName">デスクトップ接続名</param>
        /// <returns>設定ファイルパス</returns>
        public static string GetDesktopConfigFilePath(string desktopConnectName)
        {
            return string.Format(CONFIG_PATH_TEMP, desktopConnectName);
        }
        #endregion
    }
}
