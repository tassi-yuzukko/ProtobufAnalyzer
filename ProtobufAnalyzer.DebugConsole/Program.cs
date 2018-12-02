using LanguageExt;
using ProtobufAnalyzer.Core;
using ProtobufAnalyzer.Core.KeyScriptMaps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.DebugConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);

            var jsonData = ReadJson("scriptPaths.json");

            await jsonData.ToAsync().MatchAsync(
                Right: async json =>
                {
                    var keyScriptJsonMap = new KeyScriptJsonMap(json);
                    var requestResponseExecutor = new RequestResponseExecutor(keyScriptJsonMap);

                    while (true)
                    {
                        Console.WriteLine("Enter rquest topic. If you want to quit this app, press 'quit'.");
                        Console.Write("Enter here : ");

                        var input = Console.ReadLine();

                        if (input == "quit") break;

                        var ret = await requestResponseExecutor.ExecAsync(input, MakeRequest());

                        ret.Match(
                            Right: x =>
                            {
                                Console.WriteLine(x.requestProto.ToJsonString());
                                Console.WriteLine(x.responseProto.ToJsonString());
                            },
                            Left: x =>
                            {
                                Console.WriteLine(x);
                            });
                    }
                },
                LeftAsync: ex =>
                {
                    Console.WriteLine(ex);
                    Console.ReadKey();
                    return Task.CompletedTask;
                });
        }

        static Either<Exception, string> ReadJson(string path)
        {
            try
            {
                using (var stream = File.OpenText(path))
                {
                    return stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        static IEnumerable<byte> MakeRequest()
        {
            var request = new Request
            {
                RequestNumber = 99,
                Message = "Hello World !",
            };

            var protobuf = new ProtobufObject<Request>(request);

            return protobuf.Serialize();
        }
    }
}
