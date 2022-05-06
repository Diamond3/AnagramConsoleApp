namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepositoryOld
{
    List<string> GetWords();
    void AddWord(string word);
}