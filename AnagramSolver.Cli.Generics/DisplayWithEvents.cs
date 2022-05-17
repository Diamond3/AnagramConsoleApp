using AnagramSolver.Cli.Generics.Interfaces;

namespace AnagramSolver.Cli.Generics;

public class DisplayWithEvents : IDisplay
{
    public EventHandler PrintEvent;
    Action<string> _printer;

    public DisplayWithEvents(Action<string> printer)
    {
        _printer = printer;
    }

    public void FormattedPrint(string message, Func<string, string> formatter)
    {
        var formattedString = formatter(message);
        _printer(formattedString);
    }

    public void OnPrint()
    {
        PrintEvent.Invoke(this, EventArgs.Empty);
    }
}