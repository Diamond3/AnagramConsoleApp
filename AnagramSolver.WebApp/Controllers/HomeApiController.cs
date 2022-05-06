using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AnagramSolver.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly IAnagramSolverLogic _solver;
        private readonly IWordsServiceOld _wordService;
        private readonly IFileService _fileService;
        
        private readonly WordRepository _wordRepoSql = new(); //use WordService instead of repo
        
        public HomeApiController(IWordsServiceOld service, IAnagramSolverLogic anagramSolver, IFileService fileService)
        {
            _wordService = service;
            _solver = anagramSolver;
            _fileService = fileService;
        }
        
        //---- using SQL database
        // api/homeapi/sql
        [HttpGet("sql")]
        public IEnumerable<WordModel> GetWords()
        {
            return _wordRepoSql.GetWords();
        }
        
        // api/homeapi/sql/addAll
        [HttpGet("sql/addAll")]
        public void AddAllWordsToDb() //move form .txt to database
        {
            var data = new DataAccessHashSet();
            var models = data.ReadFileToList("zodynas.txt");
            _wordRepoSql.AddAllWordModels(models);
        }
        //----
        
        // api/homeapi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _wordService.GetAllWords().Take(50);
        }
        
        // api/homeapi/word
        [HttpGet("{word}")]
        public IEnumerable<string> GetAnagrams(string word)
        {
            try
            {
                var wordsList = _wordService.GetAllWords();
                return _solver.Solve(word, wordsList);
            }
            catch(Exception)
            {
                return new List<string>();
            }
        }
        
        // api/homeapi/word
        [HttpPut("{word}")]
        public async Task<ActionResult> InsertWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                return BadRequest("Empty word!");
            return _wordService.AddWord(word) ? Ok() : BadRequest("Error!");
        }
        
        // api/homeapi/files/zodynas
        [HttpGet("files/{name}")]
        public async Task<ActionResult> DownloadFile(string name)
        {
            var response = new ProcessFileResponse();
            if (_fileService.ProcessFile(response, name))
            {
                return File(response.Bytes, response.Type, response.Name);
            }
            return NotFound();
        }
    }
}
 