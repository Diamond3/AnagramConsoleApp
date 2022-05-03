namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    List<string> GetWords();
    void AddWord(string word);
}