using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IWordService<Word>
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public List<Word> GetAllWords()
    {
        return _wordRepository.GetWords();
    }

    public List<Word> GetAnagrams(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();

        var anagrams = GetAnagramsFromCachedWord(word);

        if (anagrams.Count > 0) return anagrams;

        var sortedArray = word.ToLower().ToArray();
        Array.Sort(sortedArray);
        var sortedWord = new string(sortedArray);

        anagrams = _wordRepository.GetAllWordsBySortedForm(sortedWord, word);

        _wordRepository.InsertAnagramsCachedWord(word, anagrams);
        return anagrams;
    }

    public List<Word> GetWordsByWordPart(string? wordPart)
    {
        if (string.IsNullOrEmpty(wordPart)) return new List<Word>();
        return _wordRepository.GetAllWordsByWordPart(wordPart);
    }

    public void InsertAnagramsCachedWord(string? word, List<Word> wordList)
    {
        if (string.IsNullOrEmpty(word)) return;
        _wordRepository.InsertAnagramsCachedWord(word, wordList);
    }

    public void InsertAllWordModels(List<Word> models)
    {
        foreach (var m in models)
        {
            var wordBytes = m.SecondForm?.ToArray();
            Array.Sort(wordBytes);
            m.SortedForm = new string(wordBytes);
        }
        _wordRepository.AddAllWordModels(models);
    }

    public void ClearCachedWord()
    {
        _wordRepository.ClearCachedWord();
    }

    public bool AddWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;
        var exists = _wordRepository.GetWords()
            .Exists(w => w.FirstForm.ToLower() == word.ToLower()
                         || w.FirstForm.ToLower() == word.ToLower());
        
        if (exists) return false;
        _wordRepository.AddWord(word);
        return true;
    }

    public void UpdateWord(int id, string word)
    {
        if (string.IsNullOrEmpty(word)) return;
        _wordRepository.UpdateWord(id, word);
    }

    public void DeleteWord(int id)
    {
        _wordRepository.DeleteWord(id);
    }

    public List<Word> GetAnagramsFromCachedWord(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();
        return _wordRepository.GetAnagramsFromCachedWord(word);
    }
}