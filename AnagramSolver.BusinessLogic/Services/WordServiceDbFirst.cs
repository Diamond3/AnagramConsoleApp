using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using IWordRepository = AnagramSolver.EF.DatabaseFirst.Interfaces.IWordRepository;

namespace AnagramSolver.BusinessLogic.Services;

public class WordServiceDbFirst : IWordService<Word>
{
    private readonly IWordRepository _wordRepository;

    public WordServiceDbFirst(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public List<Word> GetAllWords()
    {
        var repoModelList = _wordRepository.GetWords();

        var wordList = new List<Word>();
        wordList.AddRange(repoModelList.Select(w =>
            new Word
            {
                WordId = w.WordId,
                FirstForm = w.FirstForm,
                SecondForm = w.SecondForm,
                SortedForm = w.SortedForm
            }));
        return wordList;
    }

    public List<Word> GetAnagrams(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();

        var wordChars = word.ToLower().ToArray();
        Array.Sort(wordChars);
        var sortedWord = new string(wordChars);

        var repoModelList = _wordRepository.GetWords().Where(w =>
                w.SortedForm.ToLower() == sortedWord
                && !string.Equals(w.SecondForm, word, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        var wordList = new List<Word>();
        wordList.AddRange(repoModelList.Select(w =>
            new Word
            {
                WordId = w.WordId,
                FirstForm = w.FirstForm,
                SecondForm = w.SecondForm,
                SortedForm = w.SortedForm
            }));
        return wordList;
    }

    public List<Word> GetWordsByWordPart(string? wordPart)
    {
        var repoModelList = _wordRepository.GetWords();

        var wordList = new List<Word>();
        wordList.AddRange(repoModelList.Select(w =>
            new Word
            {
                WordId = w.WordId,
                FirstForm = w.FirstForm,
                SecondForm = w.SecondForm,
                SortedForm = w.SortedForm
            }));
        return wordList.Take(100).ToList();
    }

    public void InsertAnagramsCachedWord(string? word, List<Word> wordList)
    {
        throw new NotImplementedException();
    }

    public void InsertAllWordModels(List<Word> models)
    {
    }

    public void ClearCachedWord()
    {
    }
}