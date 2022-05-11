using AnagramSolver.EF.DatabaseFirst.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.EF.DatabaseFirst.Services;

public class WordService
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

    public List<CachedWord> GetAnagramsFromCachedWord(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<CachedWord>();
        var cachedWords = _wordRepository.GetCachedWords();
        return cachedWords.Where(w => w.Word == word).ToList();
    }

    public List<Word> GetAnagrams(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();
        var wordList = new List<Word>();
        var anagrams = GetAnagramsFromCachedWord(word);

        if (anagrams.Count > 0)
        {
            wordList.AddRange(anagrams.Select(a => new Word { FirstForm = word, SecondForm = a.Anagram }));
            return wordList;
        }

        var wordChars = word.ToLower().ToArray();
        Array.Sort(wordChars);
        var sortedWord = new string(wordChars);

        wordList = _wordRepository.GetWords().Where(w => w.SortedForm.ToLower() == sortedWord
                                                         && !string.Equals(w.SecondForm, word,
                                                             StringComparison.CurrentCultureIgnoreCase)).ToList();

        /*if (wordList.Count == 0)
        {
            wordList.Add(new Word()
            {
                FirstForm = word,
                SecondForm = "No Anagrams"
            });
        }
        InsertAnagramsCachedWord(word, wordList);*/
        return wordList;
    }
}