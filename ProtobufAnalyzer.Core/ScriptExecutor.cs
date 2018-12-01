using LanguageExt;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace ProtobufAnalyzer.Core
{
    public class ScriptExecutor
    {
        readonly ScriptMetadataResolver scriptMetadataResolver;

        public ScriptExecutor()
        {
            scriptMetadataResolver = ScriptMetadataResolver.Default
                .WithBaseDirectory(Environment.CurrentDirectory);
        }

        public async Task<Option<(IProtobufObject requestProto, IProtobufObject responseProto)>> RequestResponseAsync(IEnumerable<byte> requestBytes, string scriptPath)
        {
            var scriptOptions = ScriptOptions.Default
                .WithFilePath(Path.GetFullPath(scriptPath))
                .WithMetadataResolver(scriptMetadataResolver)
                .WithReferences(Assembly.GetEntryAssembly());

            try
            {
                var arg = new ForArgs(requestBytes);
                var script = CSharpScript.Create<(IProtobufObject requestProto, IProtobufObject responseProto)>(
                    File.ReadAllText(scriptPath),
                    options: scriptOptions,
                    globalsType: typeof(ForArgs));
                var ret = await script.RunAsync(globals: arg);

                return ret.ReturnValue;
            }
            catch (CompilationErrorException ex)
            {
                Console.WriteLine("[Compile Error]");
                Console.WriteLine(ex.Message);

                return None;
            }
        }
    }

    /// <summary>
    /// CSharpScript に渡す引数はクラスとして定義しなければならない決まりになっているので、
    /// レガシーな感じするけどラッパークラス作る。
    /// 本来はこういうときにこそタプルを使いところだけど。
    /// </summary>
    public class ForArgs
    {
        public ForArgs(IEnumerable<byte> bytes)
        {
            RequestBytes = bytes;
            ProtobufObjectFactory = new ProtobufObjectFactory();
        }

        public IEnumerable<byte> RequestBytes { get; }

        public ProtobufObjectFactory ProtobufObjectFactory { get; }
    }
}
