namespace AnagramSolver.Contracts.Interfaces;

public interface IWordsServiceOld
{
    List<string> GetAllWords();
    bool AddWord(string word);
}