using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProtobufAnalyzer.Core.KeyScriptMaps
{
    /// <summary>
    /// キーとスクリプトパスのマッピングを JSON 形式のファイルで実現するためのもの
    /// </summary>
    public class KeyScriptJsonMap : IKeyScriptMap
    {
        readonly string jsonData;

        public KeyScriptJsonMap(string jsonData)
        {
            this.jsonData = jsonData;
        }

        public Option<string> GetScriptPathFrom(string key)
        {
            var jObject = JObject.Parse(jsonData);
            return jObject.SelectToken(key)?.ToString() ?? Option<string>.None;
        }
    }
}
