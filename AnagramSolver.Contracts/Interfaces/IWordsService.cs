namespace AnagramSolver.Contracts.Interfaces;

public interface IWordsService
{
    HashSet<string> GetAllWords();
    void AddWord(string word);
}