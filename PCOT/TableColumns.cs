namespace PCOT
{
    /// <summary>
    /// プロセス名列定義
    /// </summary>
    public static class ProcessNameColumns
    {
        #region プロパティ
        /// <summary>プロセス名</summary>
        public static string ProcessName => nameof(ProcessName);
        /// <summary>プロセス別名</summary>
        public static string ProcessAliasName => nameof(ProcessAliasName);
        /// <summary>プロセス別名フラグ</summary>
        public static string IsAliasProcess => nameof(IsAliasProcess);
        #endregion
    }

    /// <summary>
    /// システム設定列定義
    /// </summary>
    public static class SettingColumns
    {
        #region プロパティ
        /// <summary>対象通りに改行</summary>
        public static string TargetReturn => nameof(TargetReturn);
        /// <summary>改行を無視</summary>
        public static string IgnoreReturn => nameof(IgnoreReturn);
        /// <summary>使用OCRエンジン</summary>
        public static string UsingOcrEngine => nameof(UsingOcrEngine);
        /// <summary>対象プロセスのスレッドを停止</summary>
        public static string StopTargetProcess => nameof(StopTargetProcess);
        /// <summary>DeepLの連携状態</summary>
        public static string RelationDeepLState => nameof(RelationDeepLState);
        /// <summary>翻訳後に自動音声出力</summary>
        public static string SpeechAuto => nameof(SpeechAuto);
        /// <summary>音声出力の音量</summary>
        public static string SpeechVolume => nameof(SpeechVolume);
        /// <summary>音声出力の読み上げ速度</summary>
        public static string SpeechRate => nameof(SpeechRate);
        /// <summary>翻訳結果をコピー</summary>
        public static string CopyResult => nameof(CopyResult);
        /// <summary>ショートカット使用</summary>
        public static string UseShortcut => nameof(UseShortcut);
        /// <summary>翻訳後にプロセスをアクティブ（ショートカット使用時のみ）</summary>
        public static string ProcessActivate => nameof(ProcessActivate);
        /// <summary>簡易コマンド画面使用</summary>
        public static string UseSimpleCmd => nameof(UseSimpleCmd);
        /// <summary>簡易コマンド透過率</summary>
        public static string Transparency => nameof(Transparency);
        #endregion
    }

    /// <summary>
    /// フォント設定列定義
    /// </summary>
    public static class FontColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        /// <remarks>
        /// 0:原文フォント 1:訳文フォント
        /// </remarks>
        public static string Id => nameof(Id);
        /// <summary>フォント名</summary>
        public static string FontName => nameof(FontName);
        /// <summary>フォントサイズ</summary>
        public static string FontSize => nameof(FontSize);
        /// <summary>フォントカラー</summary>
        public static string FontColor => nameof(FontColor);
        /// <summary>フォントスタイル</summary>
        public static string FontStyle => nameof(FontStyle);
        #endregion
    }

    /// <summary>
    /// ショートカット列定義
    /// </summary>
    public static class ShortcutColumns
    {
        #region プロパティ
        /// <summary>有効/無効</summary>
        public static string IsEnabled => nameof(IsEnabled);
        /// <summary>ショートカット名</summary>
        public static string ShortcutName => nameof(ShortcutName);
        /// <summary>Ctrlキー使用</summary>
        public static string EnabledCtrl => nameof(EnabledCtrl);
        /// <summary>Shiftキー使用</summary>
        public static string EnabledShift => nameof(EnabledShift);
        /// <summary>Altキー使用</summary>
        public static string EnabledAlt => nameof(EnabledAlt);
        /// <summary>ナンバー(0～9)</summary>
        public static string Number => nameof(Number);
        #endregion
    }

    /// <summary>
    /// 表示用ショートカット列定義
    /// </summary>
    public static class ShortcutDispColumns
    {
        #region プロパティ
        /// <summary>有効/無効</summary>
        public static string IsEnabled => nameof(IsEnabled);
        /// <summary>ショートカット名</summary>
        public static string ShortcutName => nameof(ShortcutName);
        public static string EnabledCtrl => nameof(EnabledCtrl);
        /// <summary>Shiftキー使用</summary>
        public static string EnabledShift => nameof(EnabledShift);
        /// <summary>Altキー使用</summary>
        public static string EnabledAlt => nameof(EnabledAlt);
        /// <summary>ナンバー(0～9)</summary>
        public static string Number => nameof(Number);
        /// <summary>ショートカット名(表示用)</summary>
        public static string ShortcutDispName => nameof(ShortcutDispName);
        /// <summary>ナンバー(表示用)</summary>
        public static string DispNumber => nameof(DispNumber);
        #endregion
    }

    /// <summary>
    /// タイトル設定列定義
    /// </summary>
    public static class TitleColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>タイトル</summary>
        public static string Title => nameof(Title);
        /// <summary>使用OCRエンジン</summary>
        public static string UseOcrEngine => nameof(UseOcrEngine);
        /// <summary>読取倍率</summary>
        public static string ReadMultiples => nameof(ReadMultiples);
        /// <summary>X座標</summary>
        public static string X => nameof(X);
        /// <summary>Y座標</summary>
        public static string Y => nameof(Y);
        /// <summary>横幅</summary>
        public static string Width => nameof(Width);
        /// <summary>縦幅</summary>
        public static string Height => nameof(Height);
        /// <summary>対象通りに改行</summary>
        public static string TargetReturn => nameof(TargetReturn);
        /// <summary>改行を無視</summary>
        public static string IgnoreReturn => nameof(IgnoreReturn);
        #endregion
    }

    /// <summary>
    /// 履歴テーブル列定義
    /// </summary>
    public static class HistoryColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>ラベル</summary>
        public static string Label => nameof(Label);
        /// <summary>原文</summary>
        public static string OriginalText => nameof(OriginalText);
        /// <summary>訳文</summary>
        public static string ResultText => nameof(ResultText);
        #endregion
    }

    /// <summary>
    /// 表示用履歴テーブル列定義
    /// </summary>
    public static class HistoryDispColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>削除</summary>
        public static string Delete => nameof(Delete);
        /// <summary>ラベル</summary>
        public static string Label => nameof(Label);
        /// <summary>原文</summary>
        public static string OriginalText => nameof(OriginalText);
        /// <summary>訳文</summary>
        public static string ResultText => nameof(ResultText);
        #endregion
    }

    /// <summary>
    /// 名詞テーブル列定義
    /// </summary>
    public static class NounColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>有効/無効</summary>
        public static string IsEnabled => nameof(IsEnabled);
        /// <summary>置換前名詞</summary>
        public static string BeforeNoun => nameof(BeforeNoun);
        /// <summary>置換後名詞</summary>
        public static string AfterNoun => nameof(AfterNoun);
        #endregion
    }

    /// <summary>
    /// 表示用名詞テーブル列定義
    /// </summary>
    public static class NounDispColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>有効/無効</summary>
        public static string IsEnabled => nameof(IsEnabled);
        /// <summary>削除</summary>
        public static string Delete => nameof(Delete);
        /// <summary>置換前名詞</summary>
        public static string BeforeNoun => nameof(BeforeNoun);
        /// <summary>置換後名詞</summary>
        public static string AfterNoun => nameof(AfterNoun);
        #endregion
    }

    /// <summary>
    /// 辞書テーブル列定義
    /// </summary>
    public static class DicColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>有効/無効</summary>
        public static string IsEnabled => nameof(IsEnabled);
        /// <summary>登録プロセス(共通 or 対象プロセス)</summary>
        public static string RegistProcess => nameof(RegistProcess);
        /// <summary>辞書タイプコード</summary>
        public static string DicTypeCd => nameof(DicTypeCd);
        /// <summary>単語単位</summary>
        public static string IsWordUnit => nameof(IsWordUnit);
        /// <summary>大文字小文字を区別</summary>
        public static string IsUpperAndLower => nameof(IsUpperAndLower);
        /// <summary>置換前テキスト</summary>
        public static string BeforeText => nameof(BeforeText);
        /// <summary>置換後テキスト</summary>
        public static string AfterText => nameof(AfterText);
        #endregion
    }

    /// <summary>
    /// 表示用辞書テーブル列定義
    /// </summary>
    public static class DicDispColumns
    {
        #region プロパティ
        /// <summary>ID</summary>
        public static string Id => nameof(Id);
        /// <summary>有効/無効</summary>
        public static string IsEnabled => nameof(IsEnabled);
        /// <summary>削除</summary>
        public static string Delete => nameof(Delete);
        /// <summary>登録プロセス(共通 or 対象プロセス)</summary>
        public static string RegistProcess => nameof(RegistProcess);
        /// <summary>辞書タイプコード</summary>
        public static string DicTypeCd => nameof(DicTypeCd);
        /// <summary>辞書タイプ名</summary>
        public static string DicTypeName => nameof(DicTypeName);
        /// <summary>単語単位</summary>
        public static string IsWordUnit => nameof(IsWordUnit);
        /// <summary>大文字小文字を区別</summary>
        public static string IsUpperAndLower => nameof(IsUpperAndLower);
        /// <summary>置換前テキスト</summary>
        public static string BeforeText => nameof(BeforeText);
        /// <summary>置換後テキスト</summary>
        public static string AfterText => nameof(AfterText);
        #endregion
    }

    /// <summary>
    /// 有効/無効一括切替用テーブル列定義
    /// </summary>
    public static class DicEnabledColumns
    {
        #region プロパティ
        /// <summary>プロセス名</summary>
        public static string ProcessName => nameof(ProcessName);
        /// <summary>チェック状態</summary>
        public static string CheckState => nameof(CheckState);
        /// <summary>不確定チェック</summary>
        public static string IsIndeterminate => nameof(IsIndeterminate);
        #endregion
    }

    /// <summary>
    /// 履歴ラベルフィルター用テーブル定義
    /// </summary>
    public static class HistoryFilterColumns
    {
        #region プロパティ
        /// <summary>ラベル名</summary>
        public static string LabelName => nameof(LabelName);
        /// <summary>チェック状態</summary>
        public static string Checked => nameof(Checked);
        #endregion
    }

    /// <summary>
    /// 画像ファイル操作用テーブル定義
    /// </summary>
    public static class OperationImgColumns
    {
        #region プロパティ
        /// <summary>削除</summary>
        public static string Delete => nameof(Delete);

        /// <summary>表示</summary>
        public static string ShowImg => nameof(ShowImg);

        /// <summary>画像ファイル名</summary>
        public static string ImgFileName => nameof(ImgFileName);
        /// <summary>画像ファイルパス</summary>
        public static string ImgFilePath => nameof(ImgFilePath);
        #endregion
    }
}
