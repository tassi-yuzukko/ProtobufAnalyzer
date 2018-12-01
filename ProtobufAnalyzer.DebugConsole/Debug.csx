// Debug 用スクリプト

#r "ProtobufAnalyzer.DebugConsole.dll"

using System;
using ProtobufAnalyzer.DebugConsole;

var requestProtobuf = ProtobufObjectFactory.Create<Request>(RequestBytes);

var request = requestProtobuf.GetDeepClonedObject();
var response = new Response();

response.Result = request.RequestNumber > 10;
response.Code = Response.Types.Code.GoodBye;
response.Message = "This is Response :" + request.Message;

var responseProtobuf = ProtobufObjectFactory.Create<Response>(response);

return (requestProtobuf, responseProtobuf);