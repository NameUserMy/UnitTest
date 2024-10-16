// See https://aka.ms/new-console-template for more information
using UnitTestDate;



var date = new MyDate();
var time = new Time();

var convert = new StringToInt();


Console.WriteLine("Enter year");
date.SetYear(convert.StrToInt(Console.ReadLine()));

Console.WriteLine("Enter month");
date.SetMonth(convert.StrToInt(Console.ReadLine()));

Console.WriteLine("Enter day");
date.SetDay(convert.StrToInt(Console.ReadLine()));

Console.WriteLine(date.ResultDate());



Console.WriteLine("Enter Time");


Console.WriteLine(time.ResultDate(time.SetHour(convert.StrToInt(Console.ReadLine())),time.SetMinute(convert.StrToInt(Console.ReadLine()))));