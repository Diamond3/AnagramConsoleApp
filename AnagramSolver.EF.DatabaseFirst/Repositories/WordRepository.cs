using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Word = AnagramSolver.Contracts.Models.Word;

namespace AnagramSolver.EF.DatabaseFirst.Repositories;

public class WordRepository : IWordRepository
{
    private readonly AnagramDBContext _anagramDbContext;

    public WordRepository(AnagramDBContext anagramDbContext)
    {
        _anagramDbContext = anagramDbContext;
    }

    public async Task AddWord(string word)
    {
    }

    public async Task UpdateWord(int id, string word)
    {
    }

    public async Task DeleteWord(int id)
    {
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
                WordId = word.WordId,
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
            WordId = word.WordId,
            FirstForm = word.FirstForm,
            SecondForm = word.SecondForm
        }).ToListAsync();
    }
}