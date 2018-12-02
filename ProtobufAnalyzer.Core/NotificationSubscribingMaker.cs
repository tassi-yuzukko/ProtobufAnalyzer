using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// 通知受信したときの解析用データを作成する人
    /// </summary>
    public class NotificationSubscribingMaker
    {
        readonly IKeyScriptMap keyScriptMap;
        readonly ScriptExecutor scriptExecutor = new ScriptExecutor();

        public NotificationSubscribingMaker(IKeyScriptMap keyScriptMap)
        {
            this.keyScriptMap = keyScriptMap;
        }

        public async Task<Either<string, IProtobufObject>> MakeNotificationSubscribingAsync(string key, IEnumerable<byte> notificationBytes)
        {
            var scriptPath = keyScriptMap.GetScriptPathFrom(key);
            var arg = new ForResponseArgs(notificationBytes);

            return await scriptPath.ToAsync().MatchAsync(
                Some: x => scriptExecutor.RunScriptAsync<IProtobufObject>(x, arg),
                None: () => $"[Can't Find Key] : {key}");
        }
    }

    public class ForNotificationSubscribingArgs
    {
        public ForNotificationSubscribingArgs(IEnumerable<byte> bytes)
        {
            NotificationBytes = bytes;
        }

        public IEnumerable<byte> NotificationBytes { get; }

        public ProtobufObjectFactory ProtobufObjectFactory { get; } = new ProtobufObjectFactory();
    }
}
