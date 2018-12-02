using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProtobufAnalyzer.Core
{
    public interface IKeyScriptMap
    {
        /// <summary>
        /// キーを受けて、キーに該当するスクリプトファイルのパスを返す
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Option<string> GetScriptPathFrom(string key);
    }
}
