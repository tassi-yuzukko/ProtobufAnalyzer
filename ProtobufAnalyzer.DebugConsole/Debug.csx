// Debug 用スクリプト

#r "ProtobufAnalyzer.DebugConsole.dll"

using System;
using ProtobufAnalyzer.DebugConsole;

var person = new Person
{
    Name = "tashima",
    Id = 2,
    Emails = "aaaaa.com",
};

var x = ProtobufObjectFactory.Create<Person>(person);

return (x, x);