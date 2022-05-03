using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepository : IWordRepository
{
    private readonly IDataAccess<HashSet<string>> _data;
    /*private readonly HashSet<string> _words;*/
    private readonly string _filePath;

    public WordRepository(IDataAccess<HashSet<string>> data, string filePath)
    {
        _filePath = filePath;
        _data = data;
    }

    public HashSet<string> GetWords()
    {
        return _data.ReadFile(_filePath);;
    }

    public void AddWord(string word)
    {
        _data.AddWordToFile(_filePath, word);
    }
}