using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// 通知送信のデータを作る人
    /// </summary>
    public class NotificationPublishingMaker
    {
        readonly IKeyScriptMap keyScriptMap;
        readonly ScriptExecutor scriptExecutor = new ScriptExecutor();

        public NotificationPublishingMaker(IKeyScriptMap keyScriptMap)
        {
            this.keyScriptMap = keyScriptMap;
        }

        public async Task<Either<string, IProtobufObject>> MakeNotificationPublishingAsync(string key)
        {
            var scriptPath = keyScriptMap.GetScriptPathFrom(key);
            var arg = new ForPublishNotificationArgs();

            return await scriptPath.ToAsync().MatchAsync(
                Some: x => scriptExecutor.RunScriptAsync<IProtobufObject>(x, arg),
                None: () => $"[Can't Find Key] : {key}");
        }
    }

    public class ForPublishNotificationArgs
    {
        public ProtobufObjectFactory ProtobufObjectFactory { get; } = new ProtobufObjectFactory();
    }
}
