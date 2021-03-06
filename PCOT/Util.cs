using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleTranslateFreeApi;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using GameUtil;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Net;
using System.Runtime.InteropServices;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Globalization;
using Windows.Media.Ocr;

namespace PCOT
{
    public class Util
    {
        #region 定数
        /// <summary>プロセス別名テーブル名</summary>
        public const string PROCESS_ALIAS_NAME_TABLE = "ProcessAliasNameTable";
        /// <summary>システム設定テーブル名</summary>
        public const string SETTING_TABLE = "PcotSettingTable";
        /// <summary>フォント設定テーブル名</summary>
        public const string FONT_SETTING_TABLE = "FontSettingTable";
        /// <summary>ショートカットテーブル名</summary>
        public const string SHORTCUT_TABLE = "PcotShortcutTable";
        /// <summary>ショートカットリストテーブル名</summary>
        public const string SHORTCUT_DISP_TABLE = "PcotShortcutDispTable";
        /// <summary>タイトル定義情報テーブル名</summary>
        public const string TITLE_TABLE = "TitleTable";
        /// <summary>履歴定義情報テーブル名</summary>
        public const string HISTORY_TABLE = "HistoryTable";
        /// <summary>履歴リストテーブル名</summary>
        public const string HISOTRY_DISP_TABLE = "HistoryDispTable";
        /// <summary>名詞定義情報テーブル名</summary>
        public const string NOUN_TABLE = "NounTable";
        /// <summary>名詞リストテーブル名</summary>
        public const string NOUN_DISP_TABLE = "NounDispTable";
        /// <summary>辞書テーブル名</summary>
        public const string DICTIONARY_TABLE = "DictionaryTable";
        /// <summary>辞書リストテーブル名</summary>
        public const string DICTIONARY_DISP_TABLE = "DictionaryDispTable";
        /// <summary>ショートカット名：翻訳</summary>
        public const string SHORTCUT_TRANSLATE_NAME = "Translate";
        /// <summary>ショートカット名(日本語)：翻訳</summary>
        public const string SHORTCUT_TRANSLATE_JPNAME = "翻訳ボタン";
        /// <summary>ショートカット名：固定翻訳</summary>
        public const string SHORTCUT_FIXEDTRANSLATE_NAME = "FixedTranslate";
        /// <summary>ショートカット名(日本語)：固定翻訳</summary>
        public const string SHORTCUT_FIXEDTRANSLATE_JPNAME = "固定翻訳";
        /// <summary>ショートカット名：フリー選択</summary>
        public const string SHORTCUT_FREE_NAME = "FreeSelect";
        /// <summary>ショートカット名(日本語)：フリー選択</summary>
        public const string SHORTCUT_FREE_JPNAME = "フリー選択";
        /// <summary>ショートカット名：画像翻訳</summary>
        public const string SHORTCUT_IMAGE_NAME = "ImageTranslate";
        /// <summary>ショートカット名(日本語)：画像翻訳</summary>
        public const string SHORTCUT_IMAGE_JPNAME = "画像翻訳";
        /// <summary>ショートカット名：クリップボード</summary>
        public const string SHORTCUT_CLIPBOARD_NAME = "ClipboardTranslate";
        /// <summary>ショートカット名(日本語)：クリップボード</summary>
        public const string SHORTCUT_CLIPBOARD_JPNAME = "ｸﾘｯﾌﾟﾎﾞｰﾄﾞ翻訳";
        /// <summary>ショートカット名：画面キャプチャー</summary>
        public const string SHORTCUT_CAPTURE_NAME = "WindowCapture";
        /// <summary>ショートカット名(日本語)：画面キャプチャー</summary>
        public const string SHORTCUT_CAPTURE_JPNAME = "画面ｷｬﾌﾟﾁｬｰ";
        /// <summary>ショートカット名：プロセス停止/再開</summary>
        public const string SHORTCUT_PROCESS_STOP_START_NAME = "ProcessStopStart";
        /// <summary>ショートカット名(日本語)：プロセス停止/再開</summary>
        public const string SHORTCUT_PROCESS_STOP_START_JPNAME = "対象ﾌﾟﾛｾｽ停止/再開";
        /// <summary>辞書ファイルパス</summary>
        private const string DICTIONARY_PATH = @"data\wordlist.dat";
        /// <summary>履歴ファイルパス 0:プロセス名</summary>
        private const string HISTORY_PATH = @"history\{0}.cfg";
        /// <summary>致命的なエラーメッセージ</summary>
        public const string CRITICAL_ERR_MES = "申し訳ありません。対象のプロセスはPCOTに対応していません。\r\n接続解除ボタンを押してデスクトップ接続をご使用ください。";
        /// <summary>
        /// 翻訳クオリティ
        /// </summary>
        public enum TranslateQuality
        {
            WebAPI,
            GAS,
            NoWork
        }
        #endregion

        #region 変数
        /// <summary>Google翻訳オブジェクト</summary>
        private static readonly GoogleTranslator translator = new GoogleTranslator();
        /// <summary>フォントダイアログ</summary>
        private static FontDialog fontDialog = new FontDialog();

        #endregion

        #region メソッド
        /// <summary>
        /// プロセス名データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateProcessNameDataTable()
        {
            DataTable dt = new DataTable(PROCESS_ALIAS_NAME_TABLE);
            dt.Columns.Add(ProcessNameColumns.ProcessName, typeof(string));
            dt.Columns.Add(ProcessNameColumns.ProcessAliasName, typeof(string));
            dt.Columns.Add(ProcessNameColumns.IsAliasProcess, typeof(bool));

            return dt;
        }

        /// <summary>
        /// 設定用データセット作成
        /// </summary>
        /// <param name="processName">プロセス名</param>
        /// <returns>作成したデータセット</returns>
        public static DataSet CreateSettingDataSet(string processName)
        {
            DataSet ds = new DataSet(processName);
            ds.Tables.Add(CreateTitleDataTable());
            ds.Tables.Add(CreateNounDataTable());

            return ds;
        }

        /// <summary>
        /// システム設定登録用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreatePcotSettingTable()
        {
            DataTable dt = new DataTable(SETTING_TABLE);
            dt.Columns.Add(SettingColumns.TargetReturn, typeof(bool));
            dt.Columns.Add(SettingColumns.IgnoreReturn, typeof(bool));
            dt.Columns.Add(SettingColumns.UsingOcrEngine, typeof(int));
            dt.Columns.Add(SettingColumns.StopTargetProcess, typeof(bool));
            dt.Columns.Add(SettingColumns.RelationDeepLState, typeof(int));
            dt.Columns.Add(SettingColumns.SpeechAuto, typeof(bool));
            dt.Columns.Add(SettingColumns.SpeechVolume, typeof(int));
            dt.Columns.Add(SettingColumns.SpeechRate, typeof(int));
            dt.Columns.Add(SettingColumns.CopyResult, typeof(bool));
            dt.Columns.Add(SettingColumns.UseShortcut, typeof(bool));
            dt.Columns.Add(SettingColumns.ProcessActivate, typeof(bool));
            dt.Columns.Add(SettingColumns.UseSimpleCmd, typeof(bool));
            dt.Columns.Add(SettingColumns.Transparency, typeof(int));

            // 初期値の設定
            dt.Columns[SettingColumns.TargetReturn].DefaultValue = false;
            dt.Columns[SettingColumns.IgnoreReturn].DefaultValue = false;
            dt.Columns[SettingColumns.UsingOcrEngine].DefaultValue = 1;
            dt.Columns[SettingColumns.StopTargetProcess].DefaultValue = false;
            dt.Columns[SettingColumns.RelationDeepLState].DefaultValue = 0;
            dt.Columns[SettingColumns.SpeechAuto].DefaultValue = false;
            dt.Columns[SettingColumns.SpeechVolume].DefaultValue = 100;
            dt.Columns[SettingColumns.SpeechRate].DefaultValue = 0;
            dt.Columns[SettingColumns.CopyResult].DefaultValue = false;
            dt.Columns[SettingColumns.UseShortcut].DefaultValue = true;
            dt.Columns[SettingColumns.ProcessActivate].DefaultValue = false;
            dt.Columns[SettingColumns.UseSimpleCmd].DefaultValue = false;
            dt.Columns[SettingColumns.Transparency].DefaultValue = 50;

            return dt;
        }

        /// <summary>
        /// フォント設定データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateFontSettingTable()
        {
            DataTable dt = new DataTable(FONT_SETTING_TABLE);
            dt.Columns.Add(FontColumns.Id, typeof(int));
            dt.Columns.Add(FontColumns.FontName, typeof(string));
            dt.Columns.Add(FontColumns.FontSize, typeof(int));
            dt.Columns.Add(FontColumns.FontColor, typeof(string));
            dt.Columns.Add(FontColumns.FontStyle, typeof(int));

            // 初期状態
            dt.Columns[FontColumns.FontName].DefaultValue = "ＭＳ ゴシック";
            dt.Columns[FontColumns.FontSize].DefaultValue = 10;
            dt.Columns[FontColumns.FontColor].DefaultValue = "0x000000";
            dt.Columns[FontColumns.FontStyle].DefaultValue = 0;

            // 原文フォント行
            var orgDr = dt.NewRow();
            orgDr[FontColumns.Id] = 0;
            dt.Rows.Add(orgDr);

            // 訳文フォント行
            var rstDr = dt.NewRow();
            rstDr[FontColumns.Id] = 1;
            dt.Rows.Add(rstDr);

            return dt;
        }

        /// <summary>
        /// ショートカット登録用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateShortcutDataTable()
        {
            DataTable dt = new DataTable(SHORTCUT_TABLE);
            dt.Columns.Add(ShortcutColumns.IsEnabled, typeof(bool));
            dt.Columns.Add(ShortcutColumns.ShortcutName, typeof(string));
            dt.Columns.Add(ShortcutColumns.EnabledCtrl, typeof(bool));
            dt.Columns.Add(ShortcutColumns.EnabledShift, typeof(bool));
            dt.Columns.Add(ShortcutColumns.EnabledAlt, typeof(bool));
            dt.Columns.Add(ShortcutColumns.Number, typeof(int));

            // 初期値の設定
            dt.Columns[ShortcutColumns.IsEnabled].DefaultValue = true;
            dt.Columns[ShortcutColumns.EnabledCtrl].DefaultValue = true;
            dt.Columns[ShortcutColumns.EnabledShift].DefaultValue = true;
            dt.Columns[ShortcutColumns.EnabledAlt].DefaultValue = false;

            // キーコードから0～9のみを抽出
            var keys0To9 = Enum.GetValues(typeof(Keys)).Cast<Keys>()
                .Where(w => (int)w >= (int)Keys.D0 && (int)w <= (int)Keys.D9).ToList();

            for (int i = 0; i < keys0To9.Count; i++)
            {
                var dr = dt.NewRow();
                if (i == 0)
                {
                    dr[ShortcutColumns.ShortcutName] = SHORTCUT_TRANSLATE_NAME;
                }
                else
                {
                    dr[ShortcutColumns.ShortcutName] = SHORTCUT_FIXEDTRANSLATE_NAME + i;
                }

                dr[ShortcutColumns.Number] = (int)keys0To9[i];
                dt.Rows.Add(dr);
            }

            var freeSelectRow = dt.NewRow();
            freeSelectRow[ShortcutColumns.ShortcutName] = SHORTCUT_FREE_NAME;
            freeSelectRow[ShortcutColumns.Number] = (int)Keys.F;
            dt.Rows.Add(freeSelectRow);

            var imgTranRow = dt.NewRow();
            imgTranRow[ShortcutColumns.ShortcutName] = SHORTCUT_IMAGE_NAME;
            imgTranRow[ShortcutColumns.Number] = (int)Keys.I;
            dt.Rows.Add(imgTranRow);

            var clipTranRow = dt.NewRow();
            clipTranRow[ShortcutColumns.ShortcutName] = SHORTCUT_CLIPBOARD_NAME;
            clipTranRow[ShortcutColumns.Number] = (int)Keys.Z;
            dt.Rows.Add(clipTranRow);

            var captureRow = dt.NewRow();
            captureRow[ShortcutColumns.ShortcutName] = SHORTCUT_CAPTURE_NAME;
            captureRow[ShortcutColumns.Number] = (int)Keys.P;
            dt.Rows.Add(captureRow);

            var processStopStartRow = dt.NewRow();
            processStopStartRow[ShortcutColumns.ShortcutName] = SHORTCUT_PROCESS_STOP_START_NAME;
            processStopStartRow[ShortcutColumns.Number] = (int)Keys.S;
            dt.Rows.Add(processStopStartRow);

            return dt;
        }

        /// <summary>
        /// ショートカット登録表示用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateShortcutDispDataTable()
        {
            DataTable dt = new DataTable(SHORTCUT_DISP_TABLE);
            dt.Columns.Add(ShortcutDispColumns.ShortcutDispName, typeof(string));
            dt.Columns.Add(ShortcutDispColumns.EnabledCtrl, typeof(bool));
            dt.Columns.Add(ShortcutDispColumns.EnabledShift, typeof(bool));
            dt.Columns.Add(ShortcutDispColumns.EnabledAlt, typeof(bool));
            dt.Columns.Add(ShortcutDispColumns.DispNumber, typeof(string));
            dt.Columns.Add(ShortcutDispColumns.ShortcutName, typeof(string));
            dt.Columns.Add(ShortcutDispColumns.Number, typeof(int));

            return dt;
        }

        /// <summary>
        /// タイトル登録用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateTitleDataTable()
        {
            DataTable dt = new DataTable(TITLE_TABLE);
            dt.Columns.Add(TitleColumns.Id, typeof(int));
            dt.Columns.Add(TitleColumns.Title, typeof(string));
            dt.Columns.Add(TitleColumns.UseOcrEngine, typeof(int));
            dt.Columns.Add(TitleColumns.ReadMultiples, typeof(float));
            dt.Columns.Add(TitleColumns.X, typeof(int));
            dt.Columns.Add(TitleColumns.Y, typeof(int));
            dt.Columns.Add(TitleColumns.Width, typeof(int));
            dt.Columns.Add(TitleColumns.Height, typeof(int));
            dt.Columns.Add(TitleColumns.TargetReturn, typeof(bool));
            dt.Columns.Add(TitleColumns.IgnoreReturn, typeof(bool));

            dt.Columns[TitleColumns.UseOcrEngine].DefaultValue = 1;
            dt.Columns[TitleColumns.TargetReturn].DefaultValue = false;
            dt.Columns[TitleColumns.IgnoreReturn].DefaultValue = false;

            return dt;
        }

        /// <summary>
        /// 履歴登録用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateHistoryDataTable()
        {
            DataTable dt = new DataTable(HISTORY_TABLE);
            dt.Columns.Add(HistoryColumns.Id, typeof(int));
            dt.Columns.Add(HistoryColumns.Label, typeof(string));
            dt.Columns.Add(HistoryColumns.OriginalText, typeof(string));
            dt.Columns.Add(HistoryColumns.ResultText, typeof(string));

            return dt;
        }

        /// <summary>
        /// 履歴登録表示用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateHistoryDispDataTable()
        {
            DataTable dt = new DataTable(HISOTRY_DISP_TABLE);
            dt.Columns.Add(HistoryDispColumns.Id, typeof(int));
            dt.Columns.Add(HistoryDispColumns.Delete, typeof(string));
            dt.Columns.Add(HistoryDispColumns.Label, typeof(string));
            dt.Columns.Add(HistoryDispColumns.OriginalText, typeof(string));
            dt.Columns.Add(HistoryDispColumns.ResultText, typeof(string));

            // デフォルト値
            dt.Columns[HistoryDispColumns.Delete].DefaultValue = "削除";

            return dt;
        }

        /// <summary>
        /// 名詞登録用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateNounDataTable()
        {
            DataTable dt = new DataTable(NOUN_TABLE);
            dt.Columns.Add(NounColumns.Id, typeof(int));
            dt.Columns.Add(NounColumns.IsEnabled, typeof(bool));
            dt.Columns.Add(NounColumns.BeforeNoun, typeof(string));
            dt.Columns.Add(NounColumns.AfterNoun, typeof(string));

            return dt;
        }

        /// <summary>
        /// 名詞登録表示用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateNounDispDataTable()
        {
            DataTable dt = new DataTable(NOUN_DISP_TABLE);
            dt.Columns.Add(NounDispColumns.Id, typeof(int));
            dt.Columns.Add(NounDispColumns.IsEnabled, typeof(bool));
            dt.Columns.Add(NounDispColumns.Delete, typeof(string));
            dt.Columns.Add(NounDispColumns.BeforeNoun, typeof(string));
            dt.Columns.Add(NounDispColumns.AfterNoun, typeof(string));

            // デフォルト値
            dt.Columns[NounDispColumns.Delete].DefaultValue = "削除";

            return dt;
        }

        /// <summary>
        /// 辞書登録用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateDicDataTable()
        {
            DataTable dt = new DataTable(DICTIONARY_TABLE);
            dt.Columns.Add(DicColumns.Id, typeof(int));
            dt.Columns.Add(DicColumns.IsEnabled, typeof(bool));
            dt.Columns.Add(DicColumns.RegistProcess, typeof(string));
            dt.Columns.Add(DicColumns.DicTypeCd, typeof(int));
            dt.Columns.Add(DicColumns.IsWordUnit, typeof(bool));
            dt.Columns.Add(DicColumns.IsUpperAndLower, typeof(bool));
            dt.Columns.Add(DicColumns.BeforeText, typeof(string));
            dt.Columns.Add(DicColumns.AfterText, typeof(string));

            // デフォルト値
            dt.Columns[DicColumns.IsEnabled].DefaultValue = true;
            dt.Columns[DicColumns.RegistProcess].DefaultValue = "共通";
            dt.Columns[DicColumns.IsWordUnit].DefaultValue = true;
            dt.Columns[DicColumns.IsUpperAndLower].DefaultValue = false;

            return dt;
        }

        /// <summary>
        /// 辞書登録表示用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateDicDispDataTable()
        {
            DataTable dt = new DataTable(DICTIONARY_DISP_TABLE);
            dt.Columns.Add(DicDispColumns.Id, typeof(int));
            dt.Columns.Add(DicDispColumns.IsEnabled, typeof(bool));
            dt.Columns.Add(DicDispColumns.Delete, typeof(string));
            dt.Columns.Add(DicDispColumns.RegistProcess, typeof(string));
            dt.Columns.Add(DicDispColumns.DicTypeCd, typeof(int));
            dt.Columns.Add(DicDispColumns.DicTypeName, typeof(string));
            dt.Columns.Add(DicDispColumns.IsWordUnit, typeof(bool));
            dt.Columns.Add(DicDispColumns.IsUpperAndLower, typeof(bool));
            dt.Columns.Add(DicDispColumns.BeforeText, typeof(string));
            dt.Columns.Add(DicDispColumns.AfterText, typeof(string));

            // デフォルト値
            dt.Columns[DicDispColumns.IsEnabled].DefaultValue = true;
            dt.Columns[DicDispColumns.Delete].DefaultValue = "削除";
            dt.Columns[DicDispColumns.IsWordUnit].DefaultValue = true;
            dt.Columns[DicDispColumns.IsUpperAndLower].DefaultValue = false;
            return dt;
        }

        /// <summary>
        /// 辞書リストの有効/無効一括切替用データテーブル作成
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateDicEnabledDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(DicEnabledColumns.ProcessName, typeof(string));
            dt.Columns.Add(DicEnabledColumns.CheckState, typeof(CheckState));
            dt.Columns.Add(DicEnabledColumns.IsIndeterminate, typeof(bool));

            return dt;
        }

        /// <summary>
        /// 履歴ラベルフィルター用データテーブル
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateHistoryFilterDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(HistoryFilterColumns.LabelName, typeof(string));
            dt.Columns.Add(HistoryFilterColumns.Checked, typeof(bool));

            return dt;
        }

        /// <summary>
        /// 画像ファイル操作用データテーブル
        /// </summary>
        /// <returns>作成したデータテーブル</returns>
        public static DataTable CreateOperationImgDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(OperationImgColumns.Delete, typeof(bool));
            dt.Columns.Add(OperationImgColumns.ShowImg, typeof(bool));
            dt.Columns.Add(OperationImgColumns.ImgFileName, typeof(string));
            dt.Columns.Add(OperationImgColumns.ImgFilePath, typeof(string));

            return dt;
        }

        /// <summary>
        /// 設定ファイルの同期をとる
        /// </summary>
        public static void SyncConfigData()
        {
            // TODO: プロセス以外の設定ファイルに修正が入ったら処理を追加

            if (!Directory.Exists("data"))
            {
                return;
            }

            // プロセス名データファイルを同期
            SyncDataTableFromFile(ProcessNameInfo.ProcessManagerFilePath, CreateProcessNameDataTable());

            // システム設定ファイルを同期
            SyncDataTableFromFile(SystemSettingInfo.SystemSettingFilePath, CreatePcotSettingTable());

            // フォント設定ファイルを同期
            SyncDataTableFromFile(SystemSettingInfo.FontSettingFilePath, CreateFontSettingTable());

            // ショートカット設定ファイルを同期
            SyncDataTableFromFile(
                ShortcutInfo.ShortcutFilePath, CreateShortcutDataTable(), ShortcutColumns.ShortcutName);

            // 辞書データファイルを同期
            SyncDataTableFromFile(Util.GetDictionaryFilePath(), CreateDicDataTable());

            // 履歴データファイルを同期
            if (Directory.Exists("history"))
            {
                foreach (var cfg in Directory.GetFileSystemEntries("history", "*.cfg"))
                {
                    SyncDataTableFromFile(cfg, CreateHistoryDataTable());
                }
            }

            // プロセス設定ファイルを全て取得
            var cfgFiles = Directory.GetFileSystemEntries("data", "*.cfg")
                .Where(w =>
                w != SystemSettingInfo.SystemSettingFilePath &&
                w != SystemSettingInfo.FontSettingFilePath).ToArray();

            foreach (var cfg in cfgFiles)
            {
                DataSet valueDs = new DataSet();
                var trueDs = CreateSettingDataSet(Path.GetFileNameWithoutExtension(cfg));
                valueDs.ReadXml(cfg);
                bool isWrite = false;

                // テーブル数比較
                if (trueDs.Tables.Count < valueDs.Tables.Count)
                {
                    isWrite = true;
                    List<string> diffTableNames = new List<string>();
                    var trueTableNames = trueDs.Tables.Cast<DataTable>().Select(s => s.TableName).ToArray();
                    var valueTableNames = valueDs.Tables.Cast<DataTable>().Select(s => s.TableName).ToArray();

                    foreach (string tableName in valueTableNames)
                    {
                        if (Array.IndexOf(trueTableNames, tableName) < 0)
                        {
                            diffTableNames.Add(tableName);
                        }
                    }

                    for (int i = valueDs.Tables.Count - 1; i >= 0; i--)
                    {
                        if (diffTableNames.Any(a => a == valueDs.Tables[i].TableName))
                        {
                            valueDs.Tables.RemoveAt(i);
                        }
                    }
                }

                foreach (DataTable dt in trueDs.Tables)
                {
                    // 各テーブルの列を比較
                    var trueColNames = dt.Columns
                        .Cast<DataColumn>().Select(s => s.ColumnName).ToArray();
                    var valueColNames = valueDs.Tables[dt.TableName]
                        .Columns.Cast<DataColumn>().Select(s => s.ColumnName).ToArray();

                    if (!trueColNames.All(a => valueColNames.Contains(a)))
                    {
                        isWrite = true;
                    }

                    // 列数の違い
                    if (trueColNames.Length < valueColNames.Length)
                    {
                        isWrite = true;
                        List<string> diffColNames = new List<string>();
                        foreach (string valueColName in valueColNames)
                        {
                            bool isExists = trueColNames.Any(a => a == valueColName);

                            if (!isExists)
                            {
                                diffColNames.Add(valueColName);
                            }
                        }

                        for (int i = valueColNames.Length - 1; i >= 0; i--)
                        {
                            if (diffColNames.Any(a => a == valueDs.Tables[dt.TableName].Columns[i].ColumnName))
                            {
                                valueDs.Tables[dt.TableName].Columns.RemoveAt(i);
                            }
                        }
                    }

                    // データタイプの同期
                    var clonedDt = valueDs.Tables[dt.TableName].Clone();
                    foreach (DataColumn col in clonedDt.Columns)
                    {
                        if (col.DataType != dt.Columns[col.ColumnName].DataType)
                        {
                            col.DataType = dt.Columns[col.ColumnName].DataType;
                            isWrite = true;
                        }
                    }

                    if (isWrite)
                    {
                        foreach (DataRow row in valueDs.Tables[dt.TableName].Rows)
                        {
                            clonedDt.ImportRow(row);
                        }

                        // 設定ファイル側のデータテーブルを削除
                        valueDs.Tables.Remove(dt.TableName);

                        // 直したものに置き換え
                        valueDs.Tables.Add(clonedDt);
                    }
                }

                if (isWrite)
                {
                    trueDs.Merge(valueDs);
                    WriteXml(trueDs, cfg);
                }
            }
        }

        /// <summary>
        /// プロセス名登録を同期
        /// </summary>
        public static void SyncRegisterProcessName()
        {
            bool isUpdate = false;
            ProcessNameInfo.ProcessNameDt.Clear();

            if (File.Exists(ProcessNameInfo.ProcessManagerFilePath))
            {
                // ファイルが存在する = データが存在する
                ProcessNameInfo.ProcessNameDt.ReadXml(ProcessNameInfo.ProcessManagerFilePath);
            }

            // dataフォルダ配下の設定を取得
            if (Directory.Exists("data"))
            {
                var processNames = Directory.GetFileSystemEntries("data", "*.cfg")
                    .Where(w =>
                    w != SystemSettingInfo.SystemSettingFilePath &&
                    w != SystemSettingInfo.FontSettingFilePath
                    )
                    .Select(s => Path.GetFileNameWithoutExtension(s))
                    .ToArray();

                if (processNames.Length > 0)
                {
                    var update = RegisterProcessName(processNames);
                    if (!isUpdate && update)
                    {
                        isUpdate = true;
                    }
                }
            }

            // imageフォルダ配下のプロセス名を取得
            if (Directory.Exists("image"))
            {
                var processNames = Directory.GetFileSystemEntries("image")
                    .Select(s => Path.GetFileNameWithoutExtension(s))
                    .ToArray();

                if (processNames.Length > 0)
                {
                    var update = RegisterProcessName(processNames);
                    if (!isUpdate && update)
                    {
                        isUpdate = true;
                    }
                }
            }

            // historyフォルダ配下のプロセス名を取得
            if (Directory.Exists("history"))
            {
                var processNames = Directory.GetFileSystemEntries("history", "*.cfg")
                    .Select(s => Path.GetFileNameWithoutExtension(s))
                    .ToArray();

                if (processNames.Length > 0)
                {
                    var update = RegisterProcessName(processNames);
                    if (!isUpdate && update)
                    {
                        isUpdate = true;
                    }
                }
            }

            // データが存在する場合は書き込む
            if (ProcessNameInfo.ProcessNameDt.Rows.Count > 0 && isUpdate)
            {
                WriteXml(ProcessNameInfo.ProcessNameDt, ProcessNameInfo.ProcessManagerFilePath);
            }
        }

        /// <summary>
        /// プロセス名登録
        /// </summary>
        /// <param name="processNames">プロセス名配列</param>
        /// <returns>True:更新あり　False:更新なし</returns>
        private static bool RegisterProcessName(string[] processNames)
        {
            bool ret = false;
            foreach (string processName in processNames)
            {
                bool isExists = false;
                foreach (DataRow dr in ProcessNameInfo.ProcessNameDt.Rows)
                {
                    if (dr[ProcessNameColumns.ProcessAliasName].Equals(processName))
                    {
                        isExists = true;
                        break;
                    }
                }

                if (!isExists)
                {
                    var prcsNameDr = ProcessNameInfo.ProcessNameDt.NewRow();
                    prcsNameDr[ProcessNameColumns.ProcessName] = processName;
                    prcsNameDr[ProcessNameColumns.ProcessAliasName] = processName;
                    prcsNameDr[ProcessNameColumns.IsAliasProcess] = false;
                    ProcessNameInfo.ProcessNameDt.Rows.Add(prcsNameDr);
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// 独立した設定ファイルの同期を取る
        /// </summary>
        /// <param name="filePath">対象ファイルパス</param>
        /// <param name="trueDt">正しいデータテーブル</param>
        /// <param name="keyName">キー名</param>
        private static void SyncDataTableFromFile(string filePath, DataTable trueDt, string keyName = "")
        {
            if (File.Exists(filePath))
            {
                DataTable valueDt = new DataTable();
                valueDt.ReadXml(filePath);
                bool isWrite = false;

                if (trueDt.TableName == SHORTCUT_TABLE)
                {
                    // Numberの値を直す
                    for (int i = 0; i < valueDt.Rows.Count; i++)
                    {
                        if ((int)valueDt.Rows[i][ShortcutColumns.Number] >= 0 &&
                            (int)valueDt.Rows[i][ShortcutColumns.Number] <= 9)
                        {
                            valueDt.Rows[i][ShortcutColumns.Number] = trueDt.Rows[i][ShortcutColumns.Number];
                        }
                    }
                }

                // 各テーブルの列を比較
                var trueColNames = trueDt.Columns
                    .Cast<DataColumn>().Select(s => s.ColumnName).ToArray();
                var valueColNames = valueDt.Columns
                    .Cast<DataColumn>().Select(s => s.ColumnName).ToArray();

                if (!trueColNames.All(a => valueColNames.Contains(a)))
                {
                    isWrite = true;
                }

                List<string> diffColNames = new List<string>();
                foreach (string valueColName in valueColNames)
                {
                    bool isExists = trueColNames.Any(a => a == valueColName);

                    if (!isExists)
                    {
                        diffColNames.Add(valueColName);
                        isWrite = true;
                    }
                }

                if (diffColNames.Count > 0)
                {
                    for (int i = valueColNames.Length - 1; i >= 0; i--)
                    {
                        if (diffColNames.Any(a => a == valueDt.Columns[i].ColumnName))
                        {
                            valueDt.Columns.RemoveAt(i);
                        }
                    }
                }

                if (isWrite)
                {
                    if (!string.IsNullOrEmpty(keyName))
                    {
                        trueDt.PrimaryKey = new DataColumn[] { trueDt.Columns[keyName] };
                        valueDt.PrimaryKey = new DataColumn[] { valueDt.Columns[keyName] };
                    }

                    trueDt.Merge(valueDt);
                    WriteXml(trueDt, filePath);
                }
            }
        }

        /// <summary>
        /// データテーブル同士が同じ値かどうかを判定
        /// </summary>
        /// <param name="orgDtTbl">退避データテーブル</param>
        /// <param name="editDtTbl">編集データテーブル</param>
        /// <returns>True:同じ　False：異なる</returns>
        public static bool IsSameValue(DataTable orgDtTbl, DataTable editDtTbl)
        {
            bool ret = true;

            // 両方共に0件の場合は同じ
            if (orgDtTbl.Rows.Count == 0 && editDtTbl.Rows.Count == 0)
            {
                return true;
            }

            // 両方でデータ件数が異なる場合は異なる
            if (orgDtTbl.Rows.Count != editDtTbl.Rows.Count)
            {
                return false;
            }

            for (int i = 0; i < orgDtTbl.Rows.Count; i++)
            {
                for (int j = 0; j < orgDtTbl.Columns.Count; j++)
                {
                    if (!orgDtTbl.Rows[i][j].Equals(editDtTbl.Rows[i][j]))
                    {
                        // 異なる場合はその場で処理を中断
                        ret = false;
                        break;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// プロセスを取得
        /// </summary>
        /// <param name="prcsName">プロセス名</param>
        /// <returns>取得プロセス</returns>
        public static Process GetProcess(string prcsName)
        {
            // 自分自身は含まない
            return Process.GetProcessesByName(prcsName)
                .Where(w => (int)w.MainWindowHandle > 0).FirstOrDefault();
        }

        /// <summary>
        /// 自身のプロセス名を取得
        /// </summary>
        /// <returns>自身のプロセス名</returns>
        public static string GetCurrentProcessName()
        {
            return (Process.GetCurrentProcess()).ProcessName;
        }

        /// <summary>
        /// 辞書ファイルパスを取得
        /// </summary>
        /// <returns></returns>
        public static string GetDictionaryFilePath()
        {
            return DICTIONARY_PATH;
        }

        /// <summary>
        /// 履歴ファイルパスを取得
        /// </summary>
        /// <param name="processName">プロセス名</param>
        /// <returns></returns>
        public static string GetHistoryFilePath(string processName)
        {
            return string.Format(HISTORY_PATH, processName);
        }

        /// <summary>
        /// 日本語へ翻訳
        /// </summary>
        /// <param name="inputText">翻訳対象テキスト</param>
        /// <param name="outputText">翻訳結果テキスト</param>
        /// <returns>翻訳クオリティー</returns>
        public static TranslateQuality TranslateToJa(string inputText, ref string outputText)
        {
            GoogleTranslateFreeApi.Language auto = GoogleTranslateFreeApi.Language.Auto;
            GoogleTranslateFreeApi.Language ja = GoogleTranslateFreeApi.Language.Japanese;

            try
            {
                outputText = inputText;

                // 規制されるまではWebAPIで翻訳
                TranslationResult result =
                    Task.Run(() => translator.TranslateAsync(inputText, auto, ja)).Result;

                outputText = result?.MergedTranslation.Replace('\u200B', ' ').Replace(" ", "");

                return TranslateQuality.WebAPI;
            }
            catch
            {
                try
                {
                    // WebAPIで失敗した場合はGASで翻訳
                    return GASTranslate.RunGasTranslateToJa(inputText, ref outputText);
                }
                catch
                {
                    // GASで失敗したらお手上げ
                    return TranslateQuality.NoWork;
                }
            }
        }

        /// <summary>
        /// XMLファイルの書き込み
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="filePath">ファイルパス</param>
        public static void WriteXml(DataSet ds, string filePath)
        {
            if (!File.Exists(filePath))
            {
                if (!Directory.Exists("data"))
                {
                    // なければ作る
                    Directory.CreateDirectory("data");
                }
            }

            ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// XMLファイルの書き込み
        /// </summary>
        /// <param name="dt">データテーブル</param>
        /// <param name="filePath">ファイルパス</param>
        public static void WriteXml(DataTable dt, string filePath)
        {
            if (!File.Exists(filePath))
            {
                var dirPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirPath))
                {
                    // なければ作る
                    Directory.CreateDirectory(dirPath);
                }
            }

            dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// <summary>
        /// Bitmap画像データのリサイズ
        /// </summary>
        /// <param name="original">元のBitmapクラスオブジェクト</param>
        /// <param name="width">リサイズ後の幅</param>
        /// <param name="height">リサイズ後の高さ</param>
        /// <param name="interpolationMode">補間モード</param>
        /// <returns>リサイズされたBitmap</returns>
        public static Bitmap ResizeBitmap(Bitmap original, int width, int height, System.Drawing.Drawing2D.InterpolationMode interpolationMode)
        {
            Bitmap bmpResize;
            Bitmap bmpResizeColor;
            Graphics graphics = null;

            try
            {
                PixelFormat pf = original.PixelFormat;

                if (original.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    // モノクロの時は仮に24bitとする
                    pf = PixelFormat.Format24bppRgb;
                }

                bmpResizeColor = new Bitmap(width, height, pf);
                var dstRect = new RectangleF(0, 0, width, height);
                var srcRect = new RectangleF(-0.5f, -0.5f, original.Width, original.Height);
                graphics = Graphics.FromImage(bmpResizeColor);
                graphics.Clear(Color.Transparent);
                graphics.InterpolationMode = interpolationMode;
                graphics.DrawImage(original, dstRect, srcRect, GraphicsUnit.Pixel);

            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }

            if (original.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                // モノクロ画像のとき、24bit→8bitへ変換

                // モノクロBitmapを確保
                bmpResize = new Bitmap(
                    bmpResizeColor.Width,
                    bmpResizeColor.Height,
                    PixelFormat.Format8bppIndexed
                    );

                var pal = bmpResize.Palette;
                for (int i = 0; i < bmpResize.Palette.Entries.Length; i++)
                {
                    pal.Entries[i] = original.Palette.Entries[i];
                }
                bmpResize.Palette = pal;

                // カラー画像のポインタへアクセス
                var bmpDataColor = bmpResizeColor.LockBits(
                        new Rectangle(0, 0, bmpResizeColor.Width, bmpResizeColor.Height),
                        ImageLockMode.ReadWrite,
                        bmpResizeColor.PixelFormat
                        );

                // モノクロ画像のポインタへアクセス
                var bmpDataMono = bmpResize.LockBits(
                        new Rectangle(0, 0, bmpResize.Width, bmpResize.Height),
                        ImageLockMode.ReadWrite,
                        bmpResize.PixelFormat
                        );

                int colorStride = bmpDataColor.Stride;
                int monoStride = bmpDataMono.Stride;

                unsafe
                {
                    var pColor = (byte*)bmpDataColor.Scan0;
                    var pMono = (byte*)bmpDataMono.Scan0;
                    for (int y = 0; y < bmpDataColor.Height; y++)
                    {
                        for (int x = 0; x < bmpDataColor.Width; x++)
                        {
                            // R,G,B同じ値のため、Bの値を代表してモノクロデータへ代入
                            pMono[x + y * monoStride] = pColor[x * 3 + y * colorStride];
                        }
                    }
                }

                bmpResize.UnlockBits(bmpDataMono);
                bmpResizeColor.UnlockBits(bmpDataColor);

                //　解放
                bmpResizeColor.Dispose();
            }
            else
            {
                // カラー画像のとき
                bmpResize = bmpResizeColor;
            }

            return bmpResize;
        }

        /// <summary>
        /// 画面キャプチャーを保存(デスクトップ)
        /// </summary>
        public static void SavePrintScreenImageForDesktop()
        {
            Rectangle rect = new Rectangle(
                Screen.PrimaryScreen.Bounds.Location, Screen.PrimaryScreen.Bounds.Size);

            int dpi = 0;
            float dpiFactor = 0f;
            UseWinApi.GetDpiInfo(ref dpi, ref dpiFactor);

            if (dpiFactor > 1)
            {
                rect.X = Convert.ToInt32(rect.X * dpiFactor);
                rect.Y = Convert.ToInt32(rect.Y * dpiFactor);
                rect.Width = Convert.ToInt32(rect.Width * dpiFactor);
                rect.Height = Convert.ToInt32(rect.Height * dpiFactor);
            }

            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

                // ビットマップ画像として保存して表示
                if (!Directory.Exists("image"))
                {
                    // ない場合は作成
                    Directory.CreateDirectory("image");
                }

                // プロセス名フォルダの存在確認
                string processName = TranslateInfo.PCOT_DESKTOP_CONNECT;
                if (!Directory.Exists($@"image\{processName}"))
                {
                    // ない場合は作成
                    Directory.CreateDirectory($@"image\{processName}");
                }

                string filePath =
                    $@"image\{processName}\{processName}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss_fff}.png";
                bmp.Save(filePath, ImageFormat.Png);
            }
        }

        /// <summary>
        /// 画面キャプチャーを保存
        /// </summary>
        public static void SavePrintScreenImage()
        {
            // その都度該当プロセスを取得する
            var prcs = GetProcess(ProcessNameInfo.SelectedProcessName);
            if (prcs == null)
            {
                // なんらかの理由で存在しない場合は中断
                return;
            }

            var hndl = UseWinApi.GetWindowHandle(prcs);

            if (prcs.ProcessName != UseWinApi.GetActiveProcessName())
            {
                // 該当プロセスをアクティブにする
                UseWinApi.WakeupWindow(hndl);

                // 若干ディレイを持たせる(0.3秒)
                Thread.Sleep(300);
            }

            Rectangle rect = new Rectangle();

            int dpi = 0;
            float dpiFactor = 0f;
            UseWinApi.GetDpiInfo(ref dpi, ref dpiFactor);

            if (dpiFactor > 1)
            {
                // DPI > 100%
                UseWinApi.GetWindowRectContainsFrame(hndl, ref rect);
            }
            else
            {
                // DPI == 100%
                UseWinApi.GetWindowRectWithoutFrame(hndl, ref rect);
            }

            if (rect.X <= 0 && rect.Y <= 0 && rect.Width <= 0 && rect.Height <= 0)
            {
                CommonUtil.PutMessage(
                    CommonEnums.MessageType.Warning, "該当アプリの座標及びサイズ取得に失敗しました。");
                return;
            }

            if (rect.X == rect.Width || rect.Y == rect.Height)
            {
                throw new Exception(CRITICAL_ERR_MES);
            }

            // モニターからはみ出した場合はモニターサイズで切る
            if (rect.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                rect.Width = Screen.PrimaryScreen.Bounds.Width;
            }

            if (rect.Height > Screen.PrimaryScreen.Bounds.Height)
            {
                rect.Height = Screen.PrimaryScreen.Bounds.Height;
            }

            // サイズ調整
            rect.Width -= rect.X;
            rect.Height -= rect.Y;

            if (dpiFactor > 1)
            {
                rect.X = Convert.ToInt32(rect.X * dpiFactor);
                rect.Y = Convert.ToInt32(rect.Y * dpiFactor);
                rect.Width = Convert.ToInt32(rect.Width * dpiFactor);
                rect.Height = Convert.ToInt32(rect.Height * dpiFactor);
            }

            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

                // ビットマップ画像として保存して表示
                if (!Directory.Exists("image"))
                {
                    // ない場合は作成
                    Directory.CreateDirectory("image");
                }

                // プロセス名フォルダの存在確認
                string processName = ProcessNameInfo.SelectedProcessAliasName;
                if (!Directory.Exists($@"image\{processName}"))
                {
                    // ない場合は作成
                    Directory.CreateDirectory($@"image\{processName}");
                }

                string filePath =
                    $@"image\{processName}\{processName}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss_fff}.png";
                bmp.Save(filePath, ImageFormat.Png);
                ProcessNameInfo.RegisterProessName();
            }
        }

        /// <summary>
        /// 画像を開く
        /// </summary>
        /// <param name="path">画像ファイルパス</param>
        /// <returns>開いたImage</returns>
        public static Image GetImage(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            return img;
        }

        /// <summary>
        /// 画像を32bitから24bitへ変換
        /// </summary>
        /// <param name="img">イメージ</param>
        /// <returns>変換後のビットマップ</returns>
        public static Bitmap ConvertTo24bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        /// <summary>
        /// ソフトウェアビットマップの取得
        /// </summary>
        /// <param name="bitmap">ビットマップ</param>
        /// <returns>ソフトウェアビットマップ</returns>
        public static async Task<SoftwareBitmap> GetSoftwareBitmap(Bitmap bitmap)
        {
            // 上で取得したキャプチャ画像をファイルとして保存
            var folder = Directory.GetCurrentDirectory();
            var imageName = "work.png";
            StorageFolder appFolder = await StorageFolder.GetFolderFromPathAsync(@folder);

            bitmap.Save(imageName, ImageFormat.Png);
            SoftwareBitmap softwareBitmap;
            var bmpFile = await appFolder.GetFileAsync(imageName);

            // 保存した画像をSoftwareBitmap形式で読み込み
            using (IRandomAccessStream stream = await bmpFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }

            // 保存した画像ファイルの削除
            File.Delete(imageName);

            // SoftwareBitmap形式の画像を返す
            return softwareBitmap;
        }

        /// <summary>
        /// Windows単位でWindows10OCRが使えるかどうかをチェック
        /// </summary>
        /// <returns>True:使用可能　False:使用不可</returns>
        public static bool IsEnabledWindows10OcrWithWindowsVersion()
        {
            var osVer = Environment.OSVersion;
            if (osVer.Version.Major != 10 && osVer.Version.Build < 15063)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Windows10 OCRが使用できるかどうか判定
        /// </summary>
        /// <returns>True:使用可能　False:使用不可</returns>
        public static bool IsEnabledWindows10Ocr()
        {
            Windows.Globalization.Language language = new Windows.Globalization.Language("en");

            var ocrEngine = OcrEngine.TryCreateFromLanguage(language);
            if (ocrEngine == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 画像を2値化する（24ビット画像のみ）
        /// </summary>
        /// <param name="srcImg">変換する画像</param>
        /// <param name="threshold">閾値</param>
        public static void ConvertTo2Color(Bitmap srcImg, int threshold)
        {
            BitmapData srcData = null;
            try
            {
                //=====================================================================
                // 変換する画像の１ピクセルあたりのバイト数を取得
                //=====================================================================
                PixelFormat pixelFormat = srcImg.PixelFormat;
                int pixelSize = Image.GetPixelFormatSize(pixelFormat) / 8;

                //=====================================================================
                // 変換する画像データをアンマネージ配列にコピー
                //=====================================================================
                srcData = srcImg.LockBits(
                    new Rectangle(0, 0, srcImg.Width, srcImg.Height),
                    ImageLockMode.ReadWrite,
                    pixelFormat);
                byte[] buf = new byte[srcData.Stride * srcData.Height];
                Marshal.Copy(srcData.Scan0, buf, 0, buf.Length);

                //=====================================================================
                // ２値化
                //=====================================================================
                for (int y = 0; y < srcData.Height; y++)
                {
                    for (int x = 0; x < srcData.Width; x++)
                    {
                        // ピクセルで考えた場合の開始位置を計算する
                        int pos = y * srcData.Stride + x * pixelSize;

                        // ピクセルの輝度を算出
                        int gray = (int)(0.299 * buf[pos + 2] + 0.587 * buf[pos + 1] + 0.114 * buf[pos]);

                        if (gray > threshold)
                        {
                            // 閾値を超えた場合、白
                            buf[pos] = 0xFF;
                            buf[pos + 1] = 0xFF;
                            buf[pos + 2] = 0xFF;
                        }
                        else
                        {
                            // 閾値以下の場合、黒
                            buf[pos] = 0x0;
                            buf[pos + 1] = 0x0;
                            buf[pos + 2] = 0x0;
                        }
                    }
                }

                Marshal.Copy(buf, 0, srcData.Scan0, buf.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (srcImg != null && srcData != null)
                {
                    srcImg.UnlockBits(srcData);
                }
            }
        }

        /// <summary>
        /// 画像を4値化する（24ビット画像のみ）
        /// </summary>
        /// <param name="srcImg">変換する画像</param>
        public static void ConvertTo4Color(Bitmap srcImg)
        {
            BitmapData srcData = null;
            try
            {
                //=====================================================================
                // 変換する画像の１ピクセルあたりのバイト数を取得
                //=====================================================================
                PixelFormat pixelFormat = srcImg.PixelFormat;
                int pixelSize = Image.GetPixelFormatSize(pixelFormat) / 8;

                //=====================================================================
                // 変換する画像データをアンマネージ配列にコピー
                //=====================================================================
                srcData = srcImg.LockBits(
                    new Rectangle(0, 0, srcImg.Width, srcImg.Height),
                    ImageLockMode.ReadWrite,
                    pixelFormat);
                byte[] buf = new byte[srcData.Stride * srcData.Height];
                Marshal.Copy(srcData.Scan0, buf, 0, buf.Length);

                //=====================================================================
                // ４値化
                //=====================================================================
                for (int y = 0; y < srcData.Height; y++)
                {
                    for (int x = 0; x < srcData.Width; x++)
                    {
                        // ピクセルで考えた場合の開始位置を計算する
                        int pos = y * srcData.Stride + x * pixelSize;

                        // ピクセルの輝度を算出
                        int gray = (int)(0.299 * buf[pos + 2] + 0.587 * buf[pos + 1] + 0.114 * buf[pos]);

                        if (gray < 64)
                        {
                            buf[pos] = 0x40;
                            buf[pos + 1] = 0x40;
                            buf[pos + 2] = 0x40;
                        }
                        else if (gray < 128)
                        {
                            buf[pos] = 0x80;
                            buf[pos + 1] = 0x80;
                            buf[pos + 2] = 0x80;
                        }
                        else if (gray < 192)
                        {
                            buf[pos] = 0xC0;
                            buf[pos + 1] = 0xC0;
                            buf[pos + 2] = 0xC0;
                        }
                        else
                        {
                            buf[pos] = 0xFF;
                            buf[pos + 1] = 0xFF;
                            buf[pos + 2] = 0xFF;
                        }
                    }
                }

                Marshal.Copy(buf, 0, srcData.Scan0, buf.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (srcImg != null && srcData != null)
                {
                    srcImg.UnlockBits(srcData);
                }
            }
        }

        /// <summary>
        /// 判別分析法により閾値を求める
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static int GetThreshold(Bitmap img)
        {
            BitmapData imgData = null;
            try
            {
                //=====================================================================
                // 変換する画像の１ピクセルあたりのバイト数を取得
                //=====================================================================
                PixelFormat pixelFormat = img.PixelFormat;
                int pixelSize = Image.GetPixelFormatSize(pixelFormat) / 8;

                //=====================================================================
                // 変換する画像データをアンマネージ配列にコピー
                //=====================================================================
                imgData = img.LockBits(
                    new Rectangle(0, 0, img.Width, img.Height),
                    ImageLockMode.ReadWrite,
                    pixelFormat);
                byte[] buf = new byte[imgData.Stride * imgData.Height];
                Marshal.Copy(imgData.Scan0, buf, 0, buf.Length);

                //=====================================================================
                // ヒストグラム算出
                //=====================================================================
                int[] hist = new int[256];
                int sum = 0;
                int cnt = 0;

                for (int y = 0; y < imgData.Height; y++)
                {
                    for (int x = 0; x < imgData.Width; x++)
                    {
                        // ピクセルで考えた場合の開始位置を計算する
                        int pos = y * imgData.Stride + x * pixelSize;

                        // ピクセルの輝度を算出
                        int gray = (int)(0.299 * buf[pos + 2] + 0.587 * buf[pos + 1] + 0.114 * buf[pos]);

                        hist[gray]++;
                        sum += gray;
                        cnt++;
                    }
                }

                // 全体の輝度の平均値
                double ave = sum / cnt;

                //=====================================================================
                // 閾値算出
                //=====================================================================
                int sh = 0;
                double sMax = 0;

                for (int i = 0; i < 256; i++)
                {
                    // クラス1とクラス2のピクセル数とピクセル値の合計値を算出
                    int n1 = 0;
                    int n2 = 0;
                    int sum1 = 0;
                    int sum2 = 0;

                    for (int j = 0; j < 256; j++)
                    {
                        if (j <= i)
                        {
                            n1 += hist[j];
                            sum1 += hist[j] * j;
                        }
                        else
                        {
                            n2 += hist[j];
                            sum2 += hist[j] * j;
                        }
                    }

                    // クラス1とクラス2のピクセル値の平均を計算
                    double ave1 = sum1 == 0 ? 0 : sum1 / n1;
                    double ave2 = sum2 == 0 ? 0 : sum2 / n2;

                    // クラス間分散の分子を計算
                    double s = n1 * n2 * Math.Pow((ave1 - ave2), 2);

                    // クラス間分散の分子が最大のとき、クラス間分散の分子と閾値を記録
                    if (s > sMax)
                    {
                        sh = i;
                        sMax = s;
                    }
                }
                return sh - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (img != null && imgData != null)
                {
                    img.UnlockBits(imgData);
                }
            }
        }

        /// <summary>
        /// 指定した画像からグレースケール画像を作成する
        /// </summary>
        /// <param name="img">基の画像</param>
        /// <returns>作成されたグレースケール画像</returns>
        public static Bitmap CreateGrayscaleImage(Bitmap img)
        {
            //グレースケールの描画先となるImageオブジェクトを作成
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            //newImgのGraphicsオブジェクトを取得
            using (Graphics g = Graphics.FromImage(newImg))
            {
                //ColorMatrixオブジェクトの作成
                //グレースケールに変換するための行列を指定する
                ColorMatrix cm =
                    new ColorMatrix(
                        new float[][]{
                new float[]{0.299f, 0.299f, 0.299f, 0 ,0},
                new float[]{0.587f, 0.587f, 0.587f, 0, 0},
                new float[]{0.114f, 0.114f, 0.114f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
                        });
                //ImageAttributesオブジェクトの作成
                ImageAttributes ia = new ImageAttributes();
                //ColorMatrixを設定する
                ia.SetColorMatrix(cm);

                //ImageAttributesを使用してグレースケールを描画
                g.DrawImage(img,
                    new Rectangle(0, 0, img.Width, img.Height),
                    0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }

            return newImg;
        }

        /// <summary>
        /// 画像比較
        /// </summary>
        /// <param name="img1">画像1</param>
        /// <param name="img2">画像2</param>
        /// <returns>True:同じ画像　False:違う画像</returns>
        public static bool ImageCompare(Bitmap img1, Bitmap img2)
        {
            //高さや幅が違えばfalse
            if (img1.Width != img2.Width || img1.Height != img2.Height) return false;
            //ImageConverterで配列に変換
            ImageConverter ic = new ImageConverter();
            byte[] byte1 = (byte[])ic.ConvertTo(img1, typeof(byte[]));
            byte[] byte2 = (byte[])ic.ConvertTo(img2, typeof(byte[]));

            //配列を比較
            return byte1.SequenceEqual(byte2);
        }

        /// <summary>
        /// 改行文字をスペースに変換
        /// </summary>
        /// <param name="value">対象文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ConvertReturnToSpace(string value)
        {
            var values = value.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            if (values.Length == 1)
            {
                // 改行文字が\nの場合
                values = value.Split('\n');
            }

            var ret = ConvertContinuousSpaceToSingleSpace(string.Join(" ", values));
            return ret;
        }

        /// <summary>
        /// 連続したスペースを一つのスペースに直す
        /// </summary>
        /// <param name="value">対象文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ConvertContinuousSpaceToSingleSpace(string value)
        {
            string ret = value.Replace("　", " ");
            var reg = new Regex(@"(\s)\1{1,}");
            ret = reg.Replace(ret, " ");
            return ret;
        }

        /// <summary>
        /// テキストファイル出力
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="writeText">書き出しテキスト</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="append">True:追加 False:上書き</param>
        public static void OutputTextFile(string filePath, string writeText, Encoding encoding, bool append)
        {
            bool isLocked = true;
            if (File.Exists(filePath))
            {
                // ファイルが存在する場合はロック確認(10回)
                for (int i = 0; i < 10; i++)
                {
                    if (!IsFileLocked(filePath))
                    {
                        isLocked = false;
                        break;
                    }
                }
            }
            else
            {
                // ファイルが存在しない場合はロックなし
                isLocked = false;
            }

            if (isLocked)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                    $"{Path.GetFileName(filePath)}にアクセスできません。");
                return;
            }

            using (var sw = new StreamWriter(filePath, append, encoding))
            {
                if (string.IsNullOrEmpty(writeText))
                {
                    sw.Write(writeText);
                }
                else
                {
                    sw.WriteLine(writeText);
                }
            }
        }

        /// <summary>
        /// 指定されたファイルがロックされているかどうかを返します。
        /// </summary>
        /// <param name="path">検証したいファイルへのフルパス</param>
        /// <returns>ロックされているかどうか</returns>
        public static bool IsFileLocked(string path)
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch
            {
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return false;
        }

        /// <summary>
        /// 指定したディレクトリとその中身を全て削除する
        /// </summary>
        public static void DeleteDirectory(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                return;
            }

            //ディレクトリ以外の全ファイルを削除
            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, System.IO.FileAttributes.Normal);
                File.Delete(filePath);
            }

            //ディレクトリの中のディレクトリも再帰的に削除
            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths)
            {
                DeleteDirectory(directoryPath);
            }

            //中が空になったらディレクトリ自身も削除
            Directory.Delete(targetDirectoryPath, false);
        }

        /// <summary>
        /// プロセスに紐づくデータの存在有無を確認
        /// </summary>
        /// <returns>True:データあり　False:データなし</returns>
        public static bool IsExistsProcessData()
        {
            // data
            string dataFilePath = $@"data\{ProcessNameInfo.SelectedProcessAliasName}.cfg";
            if (File.Exists(dataFilePath))
            {
                return true;
            }

            // image
            string imgDirPath = $@"image\{ProcessNameInfo.SelectedProcessAliasName}";
            if (Directory.Exists(imgDirPath))
            {
                return true;
            }

            // history
            string historyFilePath = $@"history\{ProcessNameInfo.SelectedProcessAliasName}.cfg";
            if (File.Exists(historyFilePath))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// フォントダイアログ
        /// </summary>
        /// <param name="initFont">初期化用の既存フォント</param>
        /// <param name="initColor">初期化用の既存カラー</param>
        /// <param name="isOrgFont">原文設定フラグ</param>
        /// <returns>フォントダイアログで指定したフォント</returns>
        public static void OpenFontDialog(Font initFont, Color initColor, ref Font retFont, ref Color retColor)
        {
            retFont = initFont;
            fontDialog.Font = initFont;
            fontDialog.Color = initColor;
            fontDialog.MinSize = 10;
            fontDialog.MaxSize = 16;
            fontDialog.FontMustExist = true;
            fontDialog.AllowVerticalFonts = false;
            fontDialog.ShowColor = true;
            fontDialog.ScriptsOnly = true;

            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                retFont = fontDialog.Font;
                retColor = fontDialog.Color;
            }
        }

        /// <summary>
        /// 設定したフォントを取得
        /// </summary>
        /// <param name="rowNum">行番号(0：原文　1：訳文)</param>
        /// <param name="foreColor">フォントの色(フォント自体は色を持たないので別で取得)</param>
        /// <returns>取得したフォント</returns>
        public static Font GetFont(int rowNum, ref Color foreColor)
        {
            var dr = SystemSettingInfo.FontSettingDt.Rows[rowNum];

            string fontName = dr[FontColumns.FontName].ToString();
            float fontSize = Convert.ToSingle((int)dr[FontColumns.FontSize]);
            FontStyle fontStyle = CommonUtil.ConvertToEnum<FontStyle>((int)dr[FontColumns.FontStyle]);
            string colorName = dr[FontColumns.FontColor].ToString();
            foreColor = ColorTranslator.FromHtml(colorName);

            return new Font(fontName, fontSize, fontStyle);
        }
        #endregion
    }
}
