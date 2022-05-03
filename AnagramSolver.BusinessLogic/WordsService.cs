using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic;

public class WordsService: IWordsService
{
    private readonly IWordRepository _repo;

    public WordsService(IWordRepository repo)
    {
        _repo = repo;
    }

    public HashSet<string> GetAllWords()
    {
        return _repo.GetWords();
    }
    public void AddWord(string word)
    { 
        _repo.AddWord(word);
    }
}
