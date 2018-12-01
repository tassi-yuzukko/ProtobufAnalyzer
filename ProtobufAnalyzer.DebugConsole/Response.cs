// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Response.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ProtobufAnalyzer.DebugConsole {

  /// <summary>Holder for reflection information generated from Response.proto</summary>
  public static partial class ResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for Response.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5SZXNwb25zZS5wcm90byJpCghSZXNwb25zZRIOCgZyZXN1bHQYCiABKAgS",
            "HAoEY29kZRgUIAEoDjIOLlJlc3BvbnNlLkNvZGUSDwoHbWVzc2FnZRgeIAEo",
            "CSIeCgRDb2RlEgkKBUhlbGxvEAASCwoHR29vZEJ5ZRABQiCqAh1Qcm90b2J1",
            "ZkFuYWx5emVyLkRlYnVnQ29uc29sZWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtobufAnalyzer.DebugConsole.Response), global::ProtobufAnalyzer.DebugConsole.Response.Parser, new[]{ "Result", "Code", "Message" }, null, new[]{ typeof(global::ProtobufAnalyzer.DebugConsole.Response.Types.Code) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Response : pb::IMessage<Response> {
    private static readonly pb::MessageParser<Response> _parser = new pb::MessageParser<Response>(() => new Response());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Response> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtobufAnalyzer.DebugConsole.ResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response(Response other) : this() {
      result_ = other.result_;
      code_ = other.code_;
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response Clone() {
      return new Response(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 10;
    private bool result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "code" field.</summary>
    public const int CodeFieldNumber = 20;
    private global::ProtobufAnalyzer.DebugConsole.Response.Types.Code code_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtobufAnalyzer.DebugConsole.Response.Types.Code Code {
      get { return code_; }
      set {
        code_ = value;
      }
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 30;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Response);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Response other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (Code != other.Code) return false;
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != false) hash ^= Result.GetHashCode();
      if (Code != 0) hash ^= Code.GetHashCode();
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != false) {
        output.WriteRawTag(80);
        output.WriteBool(Result);
      }
      if (Code != 0) {
        output.WriteRawTag(160, 1);
        output.WriteEnum((int) Code);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(242, 1);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != false) {
        size += 1 + 1;
      }
      if (Code != 0) {
        size += 2 + pb::CodedOutputStream.ComputeEnumSize((int) Code);
      }
      if (Message.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Response other) {
      if (other == null) {
        return;
      }
      if (other.Result != false) {
        Result = other.Result;
      }
      if (other.Code != 0) {
        Code = other.Code;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 80: {
            Result = input.ReadBool();
            break;
          }
          case 160: {
            code_ = (global::ProtobufAnalyzer.DebugConsole.Response.Types.Code) input.ReadEnum();
            break;
          }
          case 242: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Response message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Code {
        [pbr::OriginalName("Hello")] Hello = 0,
        [pbr::OriginalName("GoodBye")] GoodBye = 1,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code