// See https://aka.ms/new-console-template for more information

using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.Cli.Generics;
using AnagramSolver.Cli.Generics.Interfaces;

var dataAccess = new DataAccess();

Console.WriteLine("#Action in constructor");
//IDisplay display = new Display("zodis", Debug.WriteLine);
//IDisplay display = new Display("zodis", dataAccess.AddOneWordToTestFile);
IDisplay display = new Display("zodis", Console.WriteLine);
Console.WriteLine();


Console.WriteLine("#With Func and Action");
string MakeFirstLetterToUpperCase(string word)
{
    return char.ToUpper(word[0]) + word[1..];
}

var formatStringFunc = MakeFirstLetterToUpperCase;

display.FormattedPrint("zodis", formatStringFunc);
Console.WriteLine();


Console.WriteLine("#With Events");
var displayWithEvents = new DisplayWithEvents(Console.WriteLine);
displayWithEvents.PrintEvent += (s, args) =>
{
    new Display("zodis", Console.WriteLine);
    new Display("zodis", dataAccess.AddOneWordToTestFile);
    //displayWithEvents.FormattedPrint("naujasZodis", formatStringFunc);
};

displayWithEvents.PrintEvent += (s, args) =>
{
    displayWithEvents.FormattedPrint("naujasZodis", formatStringFunc);
};
displayWithEvents.OnPrint();