using GameUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public class ShortcutKeyState
    {
        #region プロパティ
        /// <summary>有効/無効</summary>
        public bool IsEnabled { get; set; } = false;
        /// <summary>ショートカット名</summary>
        public string ShortcutName { get; set; } = string.Empty;
        /// <summary>Ctrlキー</summary>
        public bool EnabledCtrl { get; set; } = false;
        /// <summary>Shiftキー</summary>
        public bool EnabledShift { get; set; } = false;
        /// <summary>Altキー</summary>
        public bool EnabledAlt { get; set; } = false;
        /// <summary>トリガーキー(0～9A～Z)</summary>
        public int Number { get; set; }
        #endregion
    }

    public class ShortcutInfo
    {
        #region 定数
        /// <summary>PCOT設定ファイルパス</summary>
        private const string PCOT_SHORTCUT_PATH = @"data\Shortcut.KeyConfig";
        #endregion

        #region プロパティ
        /// <summary>ショートカットファイルパス</summary>
        public static string ShortcutFilePath => PCOT_SHORTCUT_PATH;
        /// <summary>ショートカット設定リスト</summary>
        public static List<ShortcutKeyState> ShortcutSettingList => GetList();
        #endregion

        #region 変数
        /// <summary>システム設定データテーブル</summary>
        public static DataTable ShortcutDt = Util.CreateShortcutDataTable();
        /// <summary>キーの入力状態格納用</summary>
        public static ShortcutKeyState KeyState = new ShortcutKeyState();
        #endregion

        #region メソッド
        /// <summary>
        /// ショートカットの物理名からショートカットの論理名(日本語名)を取得
        /// </summary>
        /// <param name="keyState">キー状態オブジェクト</param>
        /// <returns>該当するショートカットの論理名(日本語名)</returns>
        public static string GetShortcutJpName(ShortcutKeyState keyState)
        {
            var retJpName = string.Empty;

            switch (keyState.ShortcutName)
            {
                case Util.SHORTCUT_TRANSLATE_NAME:
                    // 翻訳ボタン
                    retJpName = Util.SHORTCUT_TRANSLATE_JPNAME;
                    break;
                case Util.SHORTCUT_FREE_NAME:
                    // フリー選択
                    retJpName = Util.SHORTCUT_FREE_JPNAME;
                    break;
                case Util.SHORTCUT_IMAGE_NAME:
                    // 画像翻訳
                    retJpName = Util.SHORTCUT_IMAGE_JPNAME;
                    break;
                case Util.SHORTCUT_CLIPBOARD_NAME:
                    // ｸﾘｯﾌﾟﾎﾞｰﾄﾞ翻訳
                    retJpName = Util.SHORTCUT_CLIPBOARD_JPNAME;
                    break;
                case Util.SHORTCUT_CAPTURE_NAME:
                    // 画面ｷｬﾌﾟﾁｬｰ
                    retJpName = Util.SHORTCUT_CAPTURE_JPNAME;
                    break;
                default:
                    // 固定翻訳1～9
                    retJpName = keyState.ShortcutName.Replace(
                        Util.SHORTCUT_FIXEDTRANSLATE_NAME, Util.SHORTCUT_FIXEDTRANSLATE_JPNAME);
                    break;
            }

            return retJpName;
        }
        /// <summary>
        /// データテーブルからリストを取得
        /// </summary>
        /// <returns>取得したリスト</returns>
        private static List<ShortcutKeyState> GetList()
        {
            return CommonUtil.ConvertToList<List<ShortcutKeyState>>(ShortcutDt);
        }
        #endregion
    }
}
