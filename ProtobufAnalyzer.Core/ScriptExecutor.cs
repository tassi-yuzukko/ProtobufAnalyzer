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

        /// <summary>
        /// 要求応答の処理
        /// </summary>
        /// <param name="requestBytes">要求データ（Protobufの形式）</param>
        /// <param name="scriptPath">実行処理をする CSharpScript のパス</param>
        /// <returns></returns>
        public async Task<Either<string, (IProtobufObject requestProto, IProtobufObject responseProto)>> RequestResponseAsync(IEnumerable<byte> requestBytes, string scriptPath)
        {
            var scriptOptions = GetScriptOptions(scriptPath);

            return await RunAsync(async () =>
            {
                var arg = new ForRequestResponseArgs(requestBytes);
                var script = CSharpScript.Create<(IProtobufObject requestProto, IProtobufObject responseProto)>(
                    File.ReadAllText(scriptPath),
                    options: scriptOptions,
                    globalsType: typeof(ForRequestResponseArgs));
                var ret = await script.RunAsync(globals: arg);

                return ret.ReturnValue;
            });
        }

        /// <summary>
        /// 通知の処理
        /// </summary>
        /// <param name="scriptPath">実行処理をする CSharpScript のパス</param>
        /// <returns></returns>
        public async Task<Either<string, IProtobufObject>> NotificationAsync(string scriptPath)
        {
            var scriptOptions = GetScriptOptions(scriptPath);

            return await RunAsync(async () =>
            {
                var arg = new ForNotificationArgs();
                var script = CSharpScript.Create<IProtobufObject>(
                    File.ReadAllText(scriptPath),
                    options: scriptOptions,
                    globalsType: typeof(ForNotificationArgs));
                var ret = await script.RunAsync(globals: arg);

                return ret.ReturnValue;
            });
        }

        ScriptOptions GetScriptOptions(string scriptPath)
        {
            return ScriptOptions.Default
                .WithFilePath(Path.GetFullPath(scriptPath))
                .WithMetadataResolver(scriptMetadataResolver)
                .WithReferences(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// try-catch の共通処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asyncAction"></param>
        /// <returns></returns>
        async Task<Either<string, T>> RunAsync<T>(Func<Task<T>> asyncAction)
        {
            try
            {
                return await asyncAction();
            }
            catch (CompilationErrorException ex)
            {
                return $"[Compile Error]{Environment.NewLine}{ex.Message}";
            }
            catch (FileNotFoundException ex)
            {
                return $"[Can't Find File]{Environment.NewLine}{ex.Message}";
            }
        }
    }

    /// <summary>
    /// CSharpScript に渡す引数はクラスとして定義しなければならない決まりになっているので、
    /// レガシーな感じするけどラッパークラス作る。
    /// 本来はこういうときにこそタプルを使いところだけど。
    /// </summary>
    public class ForRequestResponseArgs
    {
        public ForRequestResponseArgs(IEnumerable<byte> bytes)
        {
            RequestBytes = bytes;
        }

        public IEnumerable<byte> RequestBytes { get; }

        public ProtobufObjectFactory ProtobufObjectFactory { get; } = new ProtobufObjectFactory();
    }

    public class ForNotificationArgs
    {
        public ProtobufObjectFactory ProtobufObjectFactory { get; } = new ProtobufObjectFactory();
    }
}
