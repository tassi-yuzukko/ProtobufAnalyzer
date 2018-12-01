using ProtobufAnalyzer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.DebugConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);

            var scriptExecutor = new ScriptExecutor();

            while (true)
            {
                Console.WriteLine("Enter script path. If you want to quit this app, press 'quit'.");
                Console.Write("Enter here : ");

                var input = Console.ReadLine();

                if (input == "quit") break;

                var ret = await scriptExecutor.RequestResponseAsync(MakeRequest(), input);

                ret.IfSome(x =>
                {
                    Console.WriteLine(x.requestProto.ToJsonString());
                    Console.WriteLine(x.responseProto.ToJsonString());
                });
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
