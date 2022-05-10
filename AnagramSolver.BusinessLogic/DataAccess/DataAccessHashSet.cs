using System.Text;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.DataAccess;

public class DataAccessHashSet : IDataAccess<HashSet<string>>
{
    private const char Separator = '\t';

    public HashSet<string> ReadFile(string filePath)
    {
        var wordsSet = new HashSet<string>();
        string line;

        using var streamReader = new StreamReader(filePath, Encoding.UTF8);
        while ((line = streamReader.ReadLine()) != null)
        {
            var words = line.Split(Separator);

            wordsSet.Add(words[2]);
            wordsSet.Add(words[0]);
        }

        return wordsSet;
    }

    public void AddWordToFile(string filePath, string word)
    {
        using var streamWriter = new StreamWriter(filePath, true);
        streamWriter.WriteLine($"{word}{Separator}filler{Separator}{word}{Separator}{1}");
    }

    public List<WordModel> ReadFileToList(string filePath)
    {
        var wordsSet = new List<WordModel>();
        string line;

        using var streamReader = new StreamReader(filePath, Encoding.UTF8);
        while ((line = streamReader.ReadLine()) != null)
        {
            var words = line.Split(Separator);

            wordsSet.Add(new WordModel
            {
                FirstForm = words[0],
                Form = words[1],
                SecondForm = words[2]
            });
        }

        return wordsSet;
    }
}