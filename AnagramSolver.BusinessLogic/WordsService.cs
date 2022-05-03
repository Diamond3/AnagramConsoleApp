using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic;

public class WordsService: IWordsService
{
    private readonly IWordRepository _repo;

    public WordsService(IWordRepository repo)
    {
        _repo = repo;
    }

    public List<string> GetAllWords()
    {
        return _repo.GetWords();
    }
    
    public bool AddWord(string word)
    { 
        if (GetAllWords().Contains(word.ToLower())) return false;
        try
        {
            _repo.AddWord(word);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
