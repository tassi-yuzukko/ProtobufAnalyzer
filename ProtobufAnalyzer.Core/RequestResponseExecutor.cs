using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// 要求応答を実行する人
    /// </summary>
    public class RequestResponseExecutor
    {
        readonly IKeyScriptMap keyScriptMap;
        readonly ScriptExecutor scriptExecutor = new ScriptExecutor();

        public RequestResponseExecutor(IKeyScriptMap keyScriptMap)
        {
            this.keyScriptMap = keyScriptMap;
        }

        public async Task<Either<string, (IProtobufObject requestProto, IProtobufObject responseProto)>> ExecAsync(string key, IEnumerable<byte> data)
        {
            var scriptPath = keyScriptMap.GetScriptPathFrom(key);

            return await scriptPath.ToAsync().MatchAsync(
                Some: x => scriptExecutor.RequestResponseAsync(data, x),
                None: () => $"[Can't Find Key] : {key}");
        }
    }
}
