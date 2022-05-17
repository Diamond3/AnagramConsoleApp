using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using Word = AnagramSolver.Contracts.Models.Word;

namespace AnagramSolver.EF.DatabaseFirst.Repositories;

public class WordRepository : IWordRepository
{
    private readonly AnagramDBContext _anagramDbContext;

    public WordRepository(AnagramDBContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }

    public void AddWord(string word)
    {
    }

    public void UpdateWord(int id, string word)
    {
    }

    public void DeleteWord(int id)
    {
    }

    public List<Word> GetAnagramsFromCachedWord(string? word)
    {
        return new List<Word>();
    }

    public List<Word> GetAllWordsBySortedForm(string? sortedWord, string originalWord)
    {
        return _anagramDbContext.Words.AsEnumerable().Where(w => w.SortedForm.ToLower() == sortedWord
                                                                 && !string.Equals(w.SecondForm, originalWord,
                                                                     StringComparison.CurrentCultureIgnoreCase))
            .Select(word => new Word
            {
                WordId = word.WordId,
                FirstForm = word.FirstForm,
                SecondForm = word.SecondForm,
                SortedForm = word.SortedForm
            }).ToList();
    }

    public List<Word> GetAllWordsByWordPart(string? wordPart)
    {
        throw new NotImplementedException();
    }

    public void AddAllWordModels(List<Word> models)
    {
    }

    public void InsertAnagramsCachedWord(string? word, List<Word> models)
    {
    }

    public void ClearCachedWord()
    {
    }

    List<Word> IWordRepository.GetWords()
    {
        return _anagramDbContext.Words.Select(word => new Word
        {
            WordId = word.WordId,
            FirstForm = word.FirstForm,
            SecondForm = word.SecondForm
        }).ToList();
    }
}