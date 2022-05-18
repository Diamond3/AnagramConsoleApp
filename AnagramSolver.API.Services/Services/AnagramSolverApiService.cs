using AnagramSolver.API.Services.Models;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Newtonsoft.Json;

namespace AnagramSolver.API.Services.Services;

public class AnagramSolverApiService: IWordService<Word>
{
    private readonly IWordRepository _wordRepository;
    private static readonly HttpClient client = new();
    
    public AnagramSolverApiService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    
    public List<Word> GetAllWords()
    {
        return _wordRepository.GetWords();
    }

    public List<Word> GetAnagrams(string? word)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Word>> GetAnagramsAsync(string? word)
    {
        if (string.IsNullOrEmpty(word)) return new List<Word>();
        
        var responseBody = await client.GetStringAsync("http://www.anagramica.com/best/" + word);
        responseBody = responseBody
            .Replace("\n", "")
            .Replace("\r", "")
            .Replace(" ", "");

        var bestResponseModel = JsonConvert.DeserializeObject<BestResponseModel>(responseBody);
        if (bestResponseModel == null) return new List<Word>();
        if (bestResponseModel.Words == null) return new List<Word>();
        
        return bestResponseModel.Words
            .Select(word => new Word
            {
                FirstForm = word,
                SecondForm = word
            }).ToList();
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