using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordService
{
    List<WordModel> GetAllWords();
    //bool AddWord(string word);
}