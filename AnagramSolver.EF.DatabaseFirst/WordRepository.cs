using AnagramSolver.EF.DatabaseFirst.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.EF.DatabaseFirst;

public class WordRepository : IWordRepository
{
    private readonly AnagramDBContext _anagramDbContext;

    public WordRepository(AnagramDBContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }

    public List<Word> GetWords()
    {
        return _anagramDbContext.Words.ToList();
    }

    public List<CachedWord> GetCachedWords()
    {
        return _anagramDbContext.CachedWords.ToList();
    }
}