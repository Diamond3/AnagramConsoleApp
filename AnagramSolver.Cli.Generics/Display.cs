using AnagramSolver.Cli.Generics.Interfaces;

namespace AnagramSolver.Cli.Generics;

public class Display : IDisplay
{
    /*public delegate void Print(string message);
    public delegate string FormatString(string message);*/
    Action<string> _printer;

    public Display(string message, Action<string> printer)
    {
        _printer = printer;
        printer(message);
    }

    public void FormattedPrint(string message, Func<string, string> formatter)
    {
        var formattedString = formatter(message);
        _printer(formattedString);
    }
}