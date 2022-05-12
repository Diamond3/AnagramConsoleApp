using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;

namespace AnagramSolver.EF.CodeFirst.Repositories;

public class WordRepository : IWordRepository
{
    private readonly AnagramDBContext _anagramDbContext;

    public WordRepository(AnagramDBContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }

    public void AddWord(string word)
    {
        return;
    }

    public List<Contracts.Models.Word> GetAnagramsFromCachedWord(string? word)
    {

        return new List<Contracts.Models.Word>();
    }

    public List<Contracts.Models.Word> GetAllWordsBySortedForm(string? sortedWord, string originalWord)
    {
        return _anagramDbContext.Words.AsEnumerable().Where(w => w.SortedForm.ToLower() == sortedWord 
                                                              && !string.Equals(w.SecondForm, originalWord, StringComparison.CurrentCultureIgnoreCase))
            .Select(word => new Contracts.Models.Word()
                {
                    WordId = word.Id,
                    FirstForm = word.FirstForm,
                    SecondForm = word.SecondForm,
                    SortedForm = word.SortedForm
                }).ToList();
    }

    public List<Contracts.Models.Word> GetAllWordsByWordPart(string? wordPart)
    {
        throw new NotImplementedException();
    }

    public void AddAllWordModels(List<Contracts.Models.Word> models)
    {
        return;
    }

    public void InsertAnagramsCachedWord(string? word, List<Contracts.Models.Word> models)
    {
        return;
    }

    public void ClearCachedWord()
    {
        return;
    }

    List<Contracts.Models.Word> IWordRepository.GetWords()
    {
        return _anagramDbContext.Words.Select(word => new Contracts.Models.Word()
        {
            WordId = word.Id,
            FirstForm = word.FirstForm,
            SecondForm = word.SecondForm,
        }).ToList();
    }
}