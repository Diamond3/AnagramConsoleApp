using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

public class HomeController : Controller
{
    private const int PageSize = 34;
    private readonly IAnagramSolverLogic _solver;
    private readonly IWordsService _wordService;

    public HomeController(IWordsService service, IAnagramSolverLogic anagramSolver)
    {
        _wordService = service;
        _solver = anagramSolver;
    }

    public IActionResult Index(string? word)
    {
        if (string.IsNullOrEmpty(word))
            return new EmptyResult();

        ViewData["Title"] = "Word";
        ViewData["Word"] = word;

        var wordsSet = _wordService.GetAllWords();
        var wordModel = new WordList
        {
            Word = word,
            Anagrams = _solver.Solve(word, wordsSet)
        };
        return View(wordModel);
    }

    public IActionResult Anagrams(int pageNumber = 1)
    {
        ViewData["Title"] = "Anagrams";
        var wordsList = _wordService.GetAllWords().ToList();
        return View(PaginatedList<string>.Create(wordsList, pageNumber, PageSize));
    }

    [HttpGet]
    public IActionResult UpdateDatabase()
    {
        ViewData["Title"] = "Database";
        return View();
    }

    [HttpPost]
    public IActionResult UpdateDatabase(string? word)
    {
        ViewData["Title"] = "Database";
        if (string.IsNullOrEmpty(word))
            ModelState.AddModelError("Error", "Empty word!");
        else if (_wordService.GetAllWords().Contains(word.ToLower()))
            ModelState.AddModelError("Error", "Word is already in database!");
        else
            try
            {
                _wordService.AddWord(word);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "Couldn't save the word!");
            }

        return View();
    }
}