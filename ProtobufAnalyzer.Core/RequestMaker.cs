using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// 要求送信用データを作成する人
    /// </summary>
    public class RequestMaker
    {
        readonly IKeyScriptMap keyScriptMap;
        readonly ScriptExecutor scriptExecutor = new ScriptExecutor();

        public RequestMaker(IKeyScriptMap keyScriptMap)
        {
            this.keyScriptMap = keyScriptMap;
        }

        /// <summary>
        /// 要求送信用のデータを作る
        /// </summary>
        /// <param name="key"></param>
        /// <returns>成功時はタプルを返す。１つは要求送信用データで、もう１つは応答データ（purotobuf形式のバイナリ）を IProtobufObject に変換するデリゲート</returns>
        public async Task<Either<string, (IProtobufObject requestProto, Func<IEnumerable<byte>, IProtobufObject> createResponseProto)>> MakeRequestAsync(string key)
        {
            var scriptPath = keyScriptMap.GetScriptPathFrom(key);
            var arg = new ForRequestArgs();

            return await scriptPath.ToAsync().MatchAsync(
                Some: x => scriptExecutor.RunScriptAsync<(IProtobufObject requestProto, Func<IEnumerable<byte>, IProtobufObject> createResponseProto)>(x, arg),
                None: () => $"[Can't Find Key] : {key}");
        }
    }

    public class ForRequestArgs
    {
        public ProtobufObjectFactory ProtobufObjectFactory { get; } = new ProtobufObjectFactory();
    }
}
