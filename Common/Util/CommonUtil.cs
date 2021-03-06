using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static GameUtil.CommonEnums;

namespace GameUtil
{
    public class CommonUtil
    {
        #region ダイアログ関連
        /// <summary>
        /// メッセージボックス表示
        /// </summary>
        /// <param name="type">メッセージタイプ</param>
        /// <param name="message">メッセージ</param>
        /// <returns>ダイアログの結果</returns>
        public static DialogResult PutMessage(MessageType type, string message, bool optCancel = false)
        {
            string caption = string.Empty;
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;

            switch (type)
            {
                case MessageType.Info:
                    caption = "情報";
                    button = MessageBoxButtons.OK;
                    icon = MessageBoxIcon.Information;
                    break;
                case MessageType.Confirm:
                    caption = "確認";
                    if (optCancel)
                    {
                        button = MessageBoxButtons.YesNoCancel;
                    }
                    else
                    {
                        button = MessageBoxButtons.YesNo;
                    }
                    icon = MessageBoxIcon.Question;
                    break;
                case MessageType.Warning:
                    caption = "警告";
                    button = MessageBoxButtons.OK;
                    icon = MessageBoxIcon.Warning;
                    break;
                case MessageType.WarningConfirm:
                    caption = "警告";
                    if (optCancel)
                    {
                        button = MessageBoxButtons.YesNoCancel;
                    }
                    else
                    {
                        button = MessageBoxButtons.YesNo;
                    }
                    icon = MessageBoxIcon.Warning;
                    break;
                case MessageType.Error:
                    caption = "エラー";
                    button = MessageBoxButtons.OK;
                    icon = MessageBoxIcon.Error;
                    break;
            }

            return MessageBox.Show(message, caption, button, icon);
        }
        #endregion

        #region その他共通部品
        /// <summary>
        /// 文字列のバイト数を取得
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>バイト数</returns>
        public static int GetByteCount(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            return sjisEnc.GetByteCount(str);
        }

        /// <summary>
        /// 矩形内の中央X座標を取得
        /// </summary>
        /// <param name="offsetX">開始オフセット座標X</param>
        /// <param name="boxW">矩形横幅</param>
        /// <param name="w">オブジェクトの横幅</param>
        /// <returns>中央X座標</returns>
        public static int GetCenterX(int offsetX, int boxW, int w)
        {
            int centerX = boxW / 2 - w / 2;
            return centerX + offsetX;
        }

        /// <summary>
        /// 矩形内の中央Y座標を取得
        /// </summary>
        /// <param name="offsetY">開始オフセット座標Y</param>
        /// <param name="boxH">矩形縦幅</param>
        /// <param name="h">オブジェクトの縦幅</param>
        /// <returns>中央X座標</returns>
        public static int GetCenterY(int offsetY, int boxH, int h)
        {
            int centerY = boxH / 2 - h / 2;
            return centerY + offsetY;
        }

        /// <summary>
        /// 最小値、最大値に丸めた値を取得
        /// </summary>
        /// <param name="value">対象の値</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <returns>最小値、最大値に丸めた値</returns>
        public static int GetLimitValue(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 最小値、最大値に丸めた値を取得
        /// </summary>
        /// <param name="value">対象の値</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <returns>最小値、最大値に丸めた値</returns>
        public static float GetLimitValue(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 連番の中で使用出来る番号を取得
        /// </summary>
        /// <param name="values">既存の連番配列</param>
        /// <returns>使用出来る番号</returns>
        public static int GetSerialNumber(int[] values)
        {
            if (!values.Contains(1))
            {
                return 1;
            }
            else
            {
                return values
                    .Where(w => !values.Contains(w + 1))
                    .Select(s => s + 1)
                    .Min();
            }
        }

        /// <summary>
        /// データテーブルから任意のリストへ変換
        /// </summary>
        /// <typeparam name="T">任意のリスト</typeparam>
        /// <param name="table">変換対象のデータテーブル</param>
        /// <returns>変換後のリスト</returns>
        public static T ConvertToList<T>(DataTable table) where T : IList, new()
        {
            var list = new T();
            foreach (DataRow row in table.Rows)
            {
                var item = Activator.CreateInstance(typeof(T).GetGenericArguments()[0]);
                list.GetType().GetGenericArguments()[0].GetProperties().ToList().
                    ForEach(p =>
                    {
                        if (row[p.Name] == DBNull.Value)
                        {
                            // DBNullの場合
                            p.SetValue(item, string.Empty, null);
                        }
                        else
                        {
                            // DBNullじゃない場合
                            p.SetValue(item, row[p.Name], null);
                        }
                    });
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// int型の値をEnumに変換
        /// </summary>
        /// <typeparam name="T">Enumの型</typeparam>
        /// <param name="item">値</param>
        /// <returns>変換後のEnum値</returns>
        public static T ConvertToEnum<T>(int item)
        {
            return (T)Enum.ToObject(typeof(T), item);
        }
        #endregion
    }
}
