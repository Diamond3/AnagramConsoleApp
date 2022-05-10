using System.Text;
using Newtonsoft.Json;

namespace AnagramSolver.Cli;

public class AnagramSolverHttpClientView
{
    private static readonly HttpClient client = new();

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

    public async Task FindAnagrams(UserSettings? settings, string? filePath)
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
                //var list = _anagramSolver.Solve(input, _service.GetAllWords());
                var list = await GetAnagramsRequest($"https://localhost:7147/api/homeapi/{input}");
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

    private static async Task<List<string>> GetAnagramsRequest(string uri)
    {
        var responseBody = await client.GetStringAsync(uri);
        var anagramsList = JsonConvert.DeserializeObject<List<string>>(responseBody);
        return anagramsList ?? new List<string>();
    }
}