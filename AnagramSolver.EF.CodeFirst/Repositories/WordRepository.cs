using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;

namespace AnagramSolver.EF.CodeFirst.Repositories;

public class WordRepository : IWordRepository
{
    private readonly AnagramDbContext _anagramDbContext;

    public WordRepository(AnagramDbContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }

    public void AddWord(string word)
    {
        var wordBytes = word.ToLower().ToArray();
        Array.Sort(wordBytes);
        _anagramDbContext.Add(new WordEntity()
        {
            FirstForm = word,
            SecondForm = word,
            SortedForm = new string(wordBytes)
        });
        _anagramDbContext.SaveChanges();
    }

    public void UpdateWord(int id, string word)
    {
        var wordEntity = _anagramDbContext.Words.FirstOrDefault(w => w.Id == id);
        if (wordEntity != null)
        {
            wordEntity.FirstForm = word;
            wordEntity.SecondForm = word;
            
            var wordBytes = word.ToLower().ToArray();
            Array.Sort(wordBytes);
            wordEntity.SortedForm = new string(wordBytes);
        }

        _anagramDbContext.SaveChanges();
    }

    public void DeleteWord(int id)
    {
        var wordEntity = _anagramDbContext.Words.FirstOrDefault(w => w.Id == id);
        if (wordEntity != null)
        {
            _anagramDbContext.Remove(wordEntity);
        }
        _anagramDbContext.SaveChanges();
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
        return new List<Contracts.Models.Word>();
    }

    public void AddAllWordModels(List<Contracts.Models.Word> models)
    {
        var wordEntities = models.Select(word => new WordEntity()
        {
            Id = word.WordId,
            FirstForm = word.FirstForm,
            SecondForm = word.SecondForm,
            SortedForm = word.SortedForm
        }).AsEnumerable();
        _anagramDbContext.Words.AddRange(wordEntities);
        _anagramDbContext.SaveChanges();
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