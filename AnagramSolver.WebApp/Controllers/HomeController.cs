using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using AnagramSolver.BusinessLogic;

namespace AnagramSolver.WebApp.Controllers;

public class HomeController : Controller
{
    private const int PageSize = 34;
    private readonly IAnagramSolverLogic _solver;
    private readonly IWordsService _wordService;
    private readonly ICookieService _cookieService;

    public HomeController(IWordsService service, IAnagramSolverLogic anagramSolver, ICookieService cookieService)
    {
        _wordService = service;
        _solver = anagramSolver;
        _cookieService = cookieService;
    }

    public IActionResult Index(string? word)
    {
        if (string.IsNullOrEmpty(word))
            return new EmptyResult();
        
        CookieOptions option = new CookieOptions();
        var count = _cookieService.GetCount(Request.Cookies[CookieService.CountKey]);
        var cookiesList = new List<string>();
        for (var i = 0; i < count; i++)
        {
            var value = Request.Cookies[CookieService.ValueKey + i];
            if(!string.IsNullOrEmpty(value))
                cookiesList.Add(value);
        }

        if (_cookieService.CanAddValue(word, cookiesList))//can
        {
            Response.Cookies.Append(CookieService.ValueKey + count, word, option);
            Response.Cookies.Append(CookieService.CountKey, (count + 1).ToString(), option);
        }

        ViewData["Title"] = "Word";

        var wordsList = _wordService.GetAllWords();
        var wordModel = new WordList
        {
            Word = word,
            Anagrams = _solver.Solve(word, wordsList)
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
    public IActionResult AddWord()
    {
        ViewData["Title"] = "Database";
        return View();
    }

    [HttpPost]
    public IActionResult AddWord(string? word)
    {
        ViewData["Title"] = "Database";
        if (string.IsNullOrEmpty(word))
            ModelState.AddModelError("Error", "Empty word!");
        else if (!_wordService.AddWord(word))
            ModelState.AddModelError("Error", "Couldn't save the word!");

        return View();
    }
}