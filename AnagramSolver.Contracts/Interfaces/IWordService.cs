namespace AnagramSolver.Contracts.Interfaces;

public interface IWordService<T>
{
    List<T> GetAllWords();
    List<T> GetAnagrams(string? word);
    List<T> GetWordsByWordPart(string? wordPart);
    void InsertAnagramsCachedWord(string? word, List<T> wordList);
    void InsertAllWordModels(List<T> models);
    void ClearCachedWord();
    bool AddWord(string word);
    void UpdateWord(int id, string word);
    void DeleteWord(int id);
    Task<List<T>> GetAnagramsAsync(string? word);
}