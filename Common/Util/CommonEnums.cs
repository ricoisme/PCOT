using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtil
{
    /// <summary>
    /// 列挙体定義用クラス
    /// </summary>
    public static class CommonEnums
    {
        /// <summary>
        /// メッセージボックスタイプ
        /// </summary>
        public enum MessageType
        {
            Info,
            Confirm,
            Warning,
            WarningConfirm,
            Error
        }

        /// <summary>
        /// 文字の描画位置
        /// </summary>
        public enum TextAlign
        {
            Left,
            Right,
            Center
        }

        #region 拡張メソッド
        /// <summary>
        /// テキストを返す
        /// </summary>
        /// <param name="enumItem"></param>
        /// <returns></returns>
        public static string Text(this Enum enumItem)
        {
            return enumItem.ToString();
        }
        #endregion
    }
}
