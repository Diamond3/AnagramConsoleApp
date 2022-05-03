using System.Text;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DataAccess;

public class DataAccessHashSet : IDataAccess<HashSet<string>>
{
    private const char Separator = '\t';

    public HashSet<string> ReadFile(string filePath)
    {
        var hs = new HashSet<string>();
        string line;

        var sr = new StreamReader(filePath, Encoding.UTF8);
        while ((line = sr.ReadLine()) != null)
        {
            var words = line.Split(Separator);

            hs.Add(words[2]);
            hs.Add(words[0]);
        }
        sr.Close();
        return hs;
    }
    public void AddWordToFile(string filePath, string word)
    {
        StreamWriter sw = new StreamWriter(filePath, true);
        sw.WriteLine($"{word}{Separator}filler{Separator}{word}{Separator}{1}");
        sw.Close();
    }
}