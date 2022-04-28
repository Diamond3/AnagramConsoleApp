using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepository : IWordRepository
{
    private readonly IDataAccess<HashSet<string>> _data;

    public WordRepository(IDataAccess<HashSet<string>> data)
    {
        _data = data;
    }

    public HashSet<string> GetWords(string filePath)
    {
        return _data.ReadFile(filePath);
    }
}