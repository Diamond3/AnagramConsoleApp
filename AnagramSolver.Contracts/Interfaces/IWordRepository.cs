using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    Task<List<Word>> GetWords();
    Task AddWord(string word);
    Task UpdateWord(int id, string word);
    Task DeleteWord(int id);
    Task<List<Word>> GetAnagramsFromCachedWord(string? word);
    Task<List<Word>> GetAllWordsBySortedForm(string? sortedWord, string originalWord);
    Task<List<Word>> GetAllWordsByWordPart(string? wordPart);
    Task AddAllWordModels(List<Word> models);
    Task InsertAnagramsCachedWord(string? word, List<Word> models);
    Task ClearCachedWord();
}