using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    List<Word> GetWords();
    void AddWord(string word);
    List<Word> GetAnagramsFromCachedWord(string? word);
    List<Word> GetAllWordsBySortedForm(string? sortedWord, string originalWord);
    List<Word> GetAllWordsByWordPart(string? wordPart);
    void AddAllWordModels(List<Word> models);
    void InsertAnagramsCachedWord(string? word, List<Word> models);
    void ClearCachedWord();
}