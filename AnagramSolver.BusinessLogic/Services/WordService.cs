using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public List<WordModel> GetAllWords()
    {
        return _wordRepository.GetWords();
    }

    public List<WordModel> GetAnagramsFromCachedWord(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<WordModel>();
        return _wordRepository.GetAnagramsFromCachedWord(word);
    }

    public List<WordModel> GetAnagrams(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<WordModel>();

        var anagrams = GetAnagramsFromCachedWord(word);

        if (anagrams.Count > 0) return anagrams;

        var sortedArray = word.ToLower().ToArray();
        Array.Sort(sortedArray);
        var sortedWord = new string(sortedArray);

        anagrams = _wordRepository.GetAllWordsBySortedForm(sortedWord, word);

        _wordRepository.InsertAnagramsCachedWord(word, anagrams);
        return anagrams;
    }

    public List<WordModel> GetWordsByWordPart(string? wordPart)
    {
        if (string.IsNullOrEmpty(wordPart)) return new List<WordModel>();
        return _wordRepository.GetAllWordsByWordPart(wordPart);
    }

    public void InsertAnagramsCachedWord(string? word, List<WordModel> models)
    {
        if (string.IsNullOrEmpty(word)) return;
        _wordRepository.InsertAnagramsCachedWord(word, models);
    }

    public void InsertAllWordModels(List<WordModel> models)
    {
        _wordRepository.AddAllWordModels(models);
    }

    public void ClearCachedWord()
    {
        _wordRepository.ClearCachedWord();
    }
}