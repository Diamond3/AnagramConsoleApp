namespace AnagramSolver.Contracts.Interfaces;

public interface IWordsService
{
    HashSet<string> GetAllWords();
}