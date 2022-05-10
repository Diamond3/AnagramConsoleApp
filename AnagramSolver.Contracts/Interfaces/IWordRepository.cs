using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    List<WordModel> GetWords();
    void AddWord(string word);
    List<WordModel> GetAnagramsFromCachedWord(string? word);
    List<WordModel> GetAllWordsBySortedForm(string? sortedWord, string originalWord);
    List<WordModel> GetAllWordsByWordPart(string? wordPart);
    void AddAllWordModels(List<WordModel> models);
    void InsertAnagramsCachedWord(string? word, List<WordModel> models);
    void ClearCachedWord();
}