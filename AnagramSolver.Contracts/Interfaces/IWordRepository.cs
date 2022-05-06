using AnagramSolver.Contracts.Models;


namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    List<WordModel> GetWords();
    void AddWord(string word);
}