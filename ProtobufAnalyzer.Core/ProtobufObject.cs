using Google.Protobuf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProtobufAnalyzer.Core
{
    public class ProtobufObject<T> : IProtobufObject
        where T : IMessage<T>, new()
    {
        readonly T obj;
        readonly JsonSerializerSettings jsonSetting = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };

        public ProtobufObject(T obj)
        {
            this.obj = obj;
        }

        public ProtobufObject(IEnumerable<byte> bytes)
        {
            this.obj = Deserialize(bytes);
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(obj, jsonSetting);
        }

        public IEnumerable<byte> Serialize()
        {
            using (var stream = new MemoryStream())
            {
                obj.WriteTo(stream);
                return stream.ToArray();
            }
        }

        public T GetDeepClonedObject()
        {
            return Deserialize(Serialize());
        }

        T Deserialize(IEnumerable<byte> bytes)
        {
            var parser = new MessageParser<T>(() => new T());
            return parser.ParseFrom(new MemoryStream(bytes.ToArray()));
        }
    }
}
