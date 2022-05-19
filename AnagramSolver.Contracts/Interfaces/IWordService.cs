namespace AnagramSolver.Contracts.Interfaces;

public interface IWordService<T>
{
    Task<List<T>> GetAllWords();
    Task<List<T>> GetWordsByWordPart(string? wordPart);
    Task InsertAnagramsCachedWord(string? word, List<T> wordList);
    Task InsertAllWordModels(List<T> models);
    Task ClearCachedWord();
    Task<bool> AddWord(string word);
    Task UpdateWord(int id, string word);
    Task DeleteWord(int id);
    Task<List<T>> GetAnagrams(string? word);
}