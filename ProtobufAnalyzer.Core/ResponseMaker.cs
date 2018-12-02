using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// 応答を作成する人
    /// </summary>
    public class ResponseMaker
    {
        readonly IKeyScriptMap keyScriptMap;
        readonly ScriptExecutor scriptExecutor = new ScriptExecutor();

        public ResponseMaker(IKeyScriptMap keyScriptMap)
        {
            this.keyScriptMap = keyScriptMap;
        }

        public async Task<Either<string, (IProtobufObject requestProto, IProtobufObject responseProto)>> MakeResponseAsync(string key, IEnumerable<byte> requestBytes)
        {
            var scriptPath = keyScriptMap.GetScriptPathFrom(key);
            var arg = new ForResponseArgs(requestBytes);

            return await scriptPath.ToAsync().MatchAsync(
                Some: x => scriptExecutor.RunScriptAsync<(IProtobufObject requestProto, IProtobufObject responseProto)>(x, arg),
                None: () => $"[Can't Find Key] : {key}");
        }
    }

    /// <summary>
    /// CSharpScript に渡す引数はクラスとして定義しなければならない決まりになっているので、
    /// レガシーな感じするけどラッパークラス作る。
    /// 本来はこういうときにこそタプルを使いところだけど。
    /// また、このクラスは CSharpScript から参照できる必要があるため、public にしないといけない
    /// </summary>
    public class ForResponseArgs
    {
        public ForResponseArgs(IEnumerable<byte> bytes)
        {
            RequestBytes = bytes;
        }

        public IEnumerable<byte> RequestBytes { get; }

        public ProtobufObjectFactory ProtobufObjectFactory { get; } = new ProtobufObjectFactory();
    }
}
