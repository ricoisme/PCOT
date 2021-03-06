using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCOT.Properties;

namespace PCOT
{
    [JsonObject]
    public class JsonSetObject
    {
        #region プロパティ
        [JsonProperty]
        /// <summary>原文</summary>
        public string text { get; set; }
        [JsonProperty]
        /// <summary>原文の言語(自動検出)</summary>
        public string source => "";
        [JsonProperty]
        /// <summary>訳文の言語(固定値：日本語)</summary>
        public string target => "ja";
        #endregion
    }

    [JsonObject]
    public class JsonGetObject
    {
        #region プロパティ
        [JsonProperty]
        /// <summary>訳文</summary>
        public string result { get; set; }
        #endregion
    }

    public class GASTranslate
    {
        #region 変数
        /// <summary>プロパティ設定</summary>
        private static Properties.Settings settings = new Properties.Settings();
        #endregion

        #region メソッド
        /// <summary>
        /// GASで日本語に翻訳
        /// </summary>
        /// <param name="inputText">入力テキスト</param>
        /// <returns>翻訳クオリティー</returns>
        public static Util.TranslateQuality RunGasTranslateToJa(string inputText, ref string outputText)
        {
            // 予め原文を訳文に入れておく
            outputText = inputText;

            // Jsonオブジェクト作成
            var jsonSet = new JsonSetObject
            {
                text = inputText
            };

            var setJson = JsonConvert.SerializeObject(jsonSet);
            using (var client = new HttpClient())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Resources.GAS_URL);

                request.Method = "POST";
                request.ContentType = "text/json";
                request.SendChunked = true;
                request.Timeout = 5000;

                try
                {
                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.WriteLine(setJson);
                    }

                    var response = request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var getJson = JsonConvert.DeserializeObject<JsonGetObject>(responseString);

                    if (!string.IsNullOrEmpty(getJson.result))
                    {
                        outputText = getJson.result.Replace('\u200B', ' ').Replace(" ", "");
                    }

                    return Util.TranslateQuality.GAS;
                }
                catch
                {
                    return Util.TranslateQuality.NoWork;
                }
            }
        }
        #endregion
    }
}
