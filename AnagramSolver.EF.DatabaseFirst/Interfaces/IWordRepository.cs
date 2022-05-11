using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.EF.DatabaseFirst.Interfaces;

public interface IWordRepository
{
    List<Word> GetWords();
    List<CachedWord> GetCachedWords();
}