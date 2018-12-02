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
    class ScriptExecutor
    {
        readonly ScriptMetadataResolver scriptMetadataResolver;

        public ScriptExecutor()
        {
            scriptMetadataResolver = ScriptMetadataResolver.Default
                .WithBaseDirectory(Environment.CurrentDirectory);
        }

        /// <summary>
        /// スクリプト処理実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asyncAction"></param>
        /// <returns></returns>
        public async Task<Either<string, T>> RunScriptAsync<T>(string scriptPath, object arg)
        {
            try
            {
                var scriptOptions = GetScriptOptions(scriptPath);

                var script = CSharpScript.Create<T>(
                    File.ReadAllText(scriptPath),
                    options: scriptOptions,
                    globalsType: arg.GetType());
                var ret = await script.RunAsync(globals: arg);

                return ret.ReturnValue;
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

        ScriptOptions GetScriptOptions(string scriptPath)
        {
            return ScriptOptions.Default
                .WithFilePath(Path.GetFullPath(scriptPath))
                .WithMetadataResolver(scriptMetadataResolver)
                .WithReferences(Assembly.GetEntryAssembly());
        }
    }
}
