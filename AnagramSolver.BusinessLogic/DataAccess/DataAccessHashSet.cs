using System.Text;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DataAccess;

public class DataAccessHashSet : IDataAccess<HashSet<string>>
{
    private readonly char _separator = '\t';

    public HashSet<string> ReadFile(string filePath)
    {
        var hs = new HashSet<string>();
        string line;

        var sr = new StreamReader(filePath, Encoding.UTF8);
        while ((line = sr.ReadLine()) != null)
        {
            var words = line.Split(_separator);

            hs.Add(words[2]);
            hs.Add(words[0]);
        }

        return hs;
    }
}