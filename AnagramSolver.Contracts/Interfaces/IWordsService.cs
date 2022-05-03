namespace AnagramSolver.Contracts.Interfaces;

public interface IWordsService
{
    List<string> GetAllWords();
    bool AddWord(string word);
}