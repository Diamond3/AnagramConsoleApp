using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService: IWordService
{
    private readonly IWordRepository _repo;

    public WordService(IWordRepository repo)
    {
        _repo = repo;
    }
    
    public List<WordModel> GetAllWords()
    {
        return _repo.GetWords();
    }
}