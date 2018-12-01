using System;
using System.Collections.Generic;
using System.Text;

namespace ProtobufAnalyzer.Core
{
    /// <summary>
    /// Protobufで使用するオブジェクトを内包するクラス
    /// </summary>
    public interface IProtobufObject
    {
        /// <summary>
        /// json にシリアライズする
        /// </summary>
        /// <returns></returns>
        string ToJsonString();

        /// <summary>
        /// protobuf にシリアライズする
        /// </summary>
        /// <returns></returns>
        IEnumerable<byte> Serialize();
    }
}
