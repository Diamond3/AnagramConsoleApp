using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Repositories;

public class WordRepository : IWordRepository
{
    private readonly AnagramDbContext _anagramDbContext;

    public WordRepository(AnagramDbContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }

    public async Task AddWord(string word)
    {
        var wordBytes = word.ToLower().ToArray();
        Array.Sort(wordBytes);
        
        await _anagramDbContext.AddAsync(new WordEntity
        {
            FirstForm = word,
            SecondForm = word,
            SortedForm = new string(wordBytes)
        });
        
        await _anagramDbContext.SaveChangesAsync();
    }

    public async Task UpdateWord(int id, string word)
    {
        var wordEntity = await _anagramDbContext.Words.FirstOrDefaultAsync(w => w.Id == id);
        if (wordEntity != null)
        {
            wordEntity.FirstForm = word;
            wordEntity.SecondForm = word;

            var wordBytes = word.ToLower().ToArray();
            Array.Sort(wordBytes);
            wordEntity.SortedForm = new string(wordBytes);
        }

        await _anagramDbContext.SaveChangesAsync();
    }

    public async Task DeleteWord(int id)
    {
        var wordEntity = await _anagramDbContext.Words.FirstOrDefaultAsync(w => w.Id == id);
        if (wordEntity != null) _anagramDbContext.Remove(wordEntity);
        await _anagramDbContext.SaveChangesAsync();
    }

    public async Task<List<Word>> GetAnagramsFromCachedWord(string? word)
    {
        return new List<Word>();
    }

    public async Task<List<Word>> GetAllWordsBySortedForm(string? sortedWord, string originalWord)
    {
        return await _anagramDbContext.Words.Where(w => w.SortedForm.ToLower() == sortedWord
                                                        && w.SecondForm.ToLower() != sortedWord.ToLower())
            .Select(word => new Word
            {
                WordId = word.Id,
                FirstForm = word.FirstForm,
                SecondForm = word.SecondForm,
                SortedForm = word.SortedForm
            }).ToListAsync();
    }

    public async Task<List<Word>> GetAllWordsByWordPart(string? wordPart)
    {
        return new List<Word>();
    }

    public async Task AddAllWordModels(List<Word> models)
    {
        var wordEntities = models.Select(word => new WordEntity
        {
            Id = word.WordId,
            FirstForm = word.FirstForm,
            SecondForm = word.SecondForm,
            SortedForm = word.SortedForm
        }).AsEnumerable();
        
        await _anagramDbContext.Words.AddRangeAsync(wordEntities);
        await _anagramDbContext.SaveChangesAsync();
    }

    public async Task InsertAnagramsCachedWord(string? word, List<Word> models)
    {
    }

    public async Task ClearCachedWord()
    {
    }

    public async Task<List<Word>> GetWords()
    {
        return await _anagramDbContext.Words.Select(word => new Word
        {
            WordId = word.Id,
            FirstForm = word.FirstForm,
            SecondForm = word.SecondForm
        }).ToListAsync();
    }
}