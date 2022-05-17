namespace AnagramSolver.Cli.Generics.Interfaces;

public interface IDisplay
{ 
    void FormattedPrint(string input, Func<string, string> formatter);
}