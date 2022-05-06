using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepositoryOld : IWordRepositoryOld
{
    private readonly IDataAccess<HashSet<string>> _data;
    /*private readonly HashSet<string> _words;*/
    private readonly string _filePath;

    public WordRepositoryOld(IDataAccess<HashSet<string>> data, string filePath)
    {
        _filePath = filePath;
        _data = data;
    }

    public List<string> GetWords()
    {
        return _data.ReadFile(_filePath).ToList();
    }

    public void AddWord(string word)
    {
        _data.AddWordToFile(_filePath, word);
    }
}