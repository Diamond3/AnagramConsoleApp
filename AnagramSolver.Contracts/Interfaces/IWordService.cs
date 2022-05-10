using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordService
{
    List<WordModel> GetAllWords();

    //bool AddWord(string word);
    List<WordModel> GetAnagramsFromCachedWord(string? word);
    List<WordModel> GetAnagrams(string? word);
    List<WordModel> GetWordsByWordPart(string? wordPart);
    void InsertAnagramsCachedWord(string? word, List<WordModel> models);
    void InsertAllWordModels(List<WordModel> models);
    void ClearCachedWord();
}