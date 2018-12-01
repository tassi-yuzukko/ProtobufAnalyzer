using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// スクリプト側で ProtobufObject を生成するためのファクトリ。
    /// 本来は静的クラスでよい内容である。
    /// しかし、CSharpScript を使用する場合、スクリプト側にオブジェクトを引数として渡す際に静的クラスは渡せないので、こういう形にしている。
    /// </summary>
    public class ProtobufObjectFactory
    {
        public ProtobufObject<T> Create<T>(T obj) where T : IMessage<T>, new()
        {
            return new ProtobufObject<T>(obj);
        }

        public ProtobufObject<T> Create<T>(IEnumerable<byte> bytes) where T : IMessage<T>, new()
        {
            return new ProtobufObject<T>(bytes);
        }
    }
}
