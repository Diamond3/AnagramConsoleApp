using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepository : IWordRepository
{
    private readonly IDataAccess<HashSet<string>> _data;
    private readonly HashSet<string> _words;

    public WordRepository(IDataAccess<HashSet<string>> data)
    {
        var filePath = "zodynas.txt";
        _data = data;
        _words = _data.ReadFile(filePath);
    }

    public HashSet<string> GetWords()
    {
        return _words;
    }
}