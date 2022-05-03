namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    HashSet<string> GetWords();
    void AddWord(string word);
}