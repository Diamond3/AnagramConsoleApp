namespace AnagramSolver.Contracts.Interfaces;

public interface IWordService<T>
{
    List<T> GetAllWords();
    List<T> GetAnagrams(string? word);
    List<T> GetWordsByWordPart(string? wordPart);
    void InsertAnagramsCachedWord(string? word, List<T> wordList);
    void InsertAllWordModels(List<T> models);
    void ClearCachedWord();
}