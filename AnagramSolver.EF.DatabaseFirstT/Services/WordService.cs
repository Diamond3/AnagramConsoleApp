using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst.Models;
using Word = AnagramSolver.Contracts.Models.Word;

namespace AnagramSolver.EF.DatabaseFirst.Services;

public class WordService: IWordService<Word>
{
    private readonly AnagramDBContext _anagramDbContext;
    public WordService(AnagramDBContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }
    public List<Word> GetAllWords()
    {
        return _anagramDbContext.Words.ToList();
    }

    public List<CachedWord> GetAnagramsFromCachedWord(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<CachedWord>();
        return _anagramDbContext.CachedWords.Where(w => w.Word == word).ToList();
    }

    public List<Word> GetAnagrams(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();
        var wordList = new List<Word>();
        var anagrams = GetAnagramsFromCachedWord(word);

        if (anagrams.Count > 0)
        {
            wordList.AddRange(anagrams.Select(a => new Word() { FirstForm = word, SecondForm = a.Anagram }));
            return wordList;
        }

        var wordChars = word.ToLower().ToArray();
        Array.Sort(wordChars);
        var sortedWord = new string(wordChars);

        wordList = _anagramDbContext.Words.Where(w => w.SortedForm.ToLower() == sortedWord
                    && !string.Equals(w.SecondForm, word, StringComparison.CurrentCultureIgnoreCase)).ToList();

        if (wordList.Count == 0)
        {
            wordList.Add(new Word()
            {
                FirstForm = word,
                SecondForm = "No Anagrams"
            });
        }
        InsertAnagramsCachedWord(word, wordList);
        return wordList;
    }

    public List<Word> GetWordsByWordPart(string? wordPart)
    {
        throw new NotImplementedException();
    }

    public void InsertAnagramsCachedWord(string? word, List<Word> wordList)
    {
        var cachedWords = new List<CachedWord>();
        cachedWords.AddRange(wordList.Select(w => new CachedWord() { Word = word, Anagram = w.SecondForm }));
        _anagramDbContext.CachedWords.AddRange(cachedWords);
        _anagramDbContext.SaveChanges();
    }

    public void InsertAllWordModels(List<Word> models)
    {
        _anagramDbContext.Words.AddRange(models);
        _anagramDbContext.SaveChanges();
    }

    public void ClearCachedWord()
    {
        throw new NotImplementedException();
    }
}