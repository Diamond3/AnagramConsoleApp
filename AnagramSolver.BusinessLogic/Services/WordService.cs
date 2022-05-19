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

    public async Task<List<Word>> GetAllWords()
    {
        return await _wordRepository.GetWords();
    }

    public async Task<List<Word>> GetAnagrams(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();

        var anagrams = await GetAnagramsFromCachedWord(word);

        if (anagrams.Count > 0) return anagrams;

        var sortedArray = word.ToLower().ToArray();
        Array.Sort(sortedArray);
        var sortedWord = new string(sortedArray);

        anagrams = await _wordRepository.GetAllWordsBySortedForm(sortedWord, word);

        await _wordRepository.InsertAnagramsCachedWord(word, anagrams);
        return anagrams;
    }

    public async Task<List<Word>> GetWordsByWordPart(string? wordPart)
    {
        if (string.IsNullOrEmpty(wordPart)) return new List<Word>();
        return await _wordRepository.GetAllWordsByWordPart(wordPart);
    }

    public async Task InsertAnagramsCachedWord(string? word, List<Word> wordList)
    {
        if (string.IsNullOrEmpty(word)) return;
        await _wordRepository.InsertAnagramsCachedWord(word, wordList);
    }

    public async Task InsertAllWordModels(List<Word> models)
    {
        foreach (var m in models)
        {
            var wordBytes = m.SecondForm?.ToArray();
            Array.Sort(wordBytes);
            m.SortedForm = new string(wordBytes);
        }

        await _wordRepository.AddAllWordModels(models);
    }

    public async Task ClearCachedWord()
    {
        await _wordRepository.ClearCachedWord();
    }

    public async Task<bool> AddWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;
        var wordList = await _wordRepository.GetWords();
        var exists = wordList.Exists(w =>
            w.FirstForm.ToLower() == word.ToLower()
            || w.FirstForm.ToLower() == word.ToLower());

        if (exists) return false;
        await _wordRepository.AddWord(word);
        
        return true;
    }

    public async Task UpdateWord(int id, string word)
    {
        if (string.IsNullOrEmpty(word)) return;
        await _wordRepository.UpdateWord(id, word);
    }

    public async Task DeleteWord(int id)
    {
        await _wordRepository.DeleteWord(id);
    }

    public async Task<List<Word>> GetAnagramsFromCachedWord(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();
        return await _wordRepository.GetAnagramsFromCachedWord(word);
    }
}