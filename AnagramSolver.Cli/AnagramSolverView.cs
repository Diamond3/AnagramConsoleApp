using System.Text;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Cli;

public class AnagramSolverView
{
    private readonly IWordsService _service;
    private readonly IAnagramSolverLogic _anagramSolver;

    public AnagramSolverView(IWordsService service, IAnagramSolverLogic anagramSolver)
    {
        _service = service;
        _anagramSolver = anagramSolver;
    }

    public void LoadView(string? env, string? message)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        if (env == null || message == null)
        {
            Console.WriteLine("Environment or message were not found!");
            return;
        }

        Console.WriteLine($"Environment: {env}");
        Console.WriteLine(message);
    }

    public void FindAnagrams(UserSettings? settings, string? filePath)
    {
        while (true)
        {
            if (settings == null)
            {
                Console.WriteLine("Settings were not found!");
                return;
            }

            var input = Console.ReadLine();
            while (input.Length < settings.MinLength)
            {
                Console.WriteLine("Word is too short!");
                input = Console.ReadLine();
            }

            try
            {
                var list = _anagramSolver.Solve(input, _service.GetAllWords());
                if (list.Count > 0)
                {
                    list.Take(settings.AnagramCount).ToList().ForEach(Console.WriteLine);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("No anagrams were found!");
                }

                Console.WriteLine("------------");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File was not found!");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
            }
        }
    }
}