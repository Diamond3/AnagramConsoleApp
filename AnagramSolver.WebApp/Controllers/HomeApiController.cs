using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeApiController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IWordService<Word> _wordService;

    public HomeApiController(IWordService<Word> service, IFileService fileService)
    {
        _wordService = service;
        _fileService = fileService;
    }

    // api/homeapi/clearCachedWord
    [HttpPost("clearCachedWord")]
    public void ClearTable()
    {
        _wordService.ClearCachedWord();
    }

    // api/homeapi/MigrateWordsToDatabase
    [HttpPost("MigrateWordsToDatabase")]
    public void MigrateWordsToDatabase()
    {
        var dataAccess = new DataAccess();
        var models = dataAccess.ReadFileToList("zodynas.txt");
        _wordService.InsertAllWordModels(models);
    }

    // api/homeapi
    [HttpGet]
    public IEnumerable<Word> Get()
    {
        return _wordService.GetAllWords().Take(50);
    }

    // api/homeapi/word
    [HttpGet("{word}")]
    public IEnumerable<Word> GetAnagrams(string word)
    {
        try
        {
            return _wordService.GetAnagrams(word);
        }
        catch (Exception)
        {
            return new List<Word>();
        }
    }

    // api/homeapi/files/zodynas
    [HttpGet("files/{name}")]
    public async Task<ActionResult> DownloadFile(string name)
    {
        var response = new ProcessFileResponse();
        if (_fileService.ProcessFile(response, name)) return File(response.Bytes, response.Type, response.Name);
        return NotFound();
    }
}