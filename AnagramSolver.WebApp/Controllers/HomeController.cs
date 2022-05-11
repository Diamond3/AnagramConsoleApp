using System.Diagnostics;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

public class HomeController : Controller
{
    private const int PageSize = 34;
    private readonly ICookieService _cookieService;
    private readonly IWordService<Word> _wordService;

    public HomeController(IWordService<Word> service, ICookieService cookieService)
    {
        _wordService = service;
        _cookieService = cookieService;
    }

    public IActionResult Index(string? word)
    {
        if (string.IsNullOrEmpty(word))
            return new EmptyResult();

        var option = new CookieOptions();
        var count = _cookieService.GetCount(Request.Cookies[CookieService.CountKey]);
        var cookiesList = new List<string>();
        for (var i = 0; i < count; i++)
        {
            var value = Request.Cookies[CookieService.ValueKey + i];
            if (!string.IsNullOrEmpty(value))
                cookiesList.Add(value);
        }

        if (_cookieService.CanAddValue(word, cookiesList))
        {
            Response.Cookies.Append(CookieService.ValueKey + count, word, option);
            Response.Cookies.Append(CookieService.CountKey, (count + 1).ToString(), option);
        }

        ViewData["Title"] = "Word";

        var wordModel = new WordList
        {
            Word = word,
            Anagrams = _wordService.GetAnagrams(word)
        };
        return View(wordModel);
    }

    public IActionResult Anagrams(int pageNumber = 1)
    {
        ViewData["Title"] = "Anagrams";
        var wordsList = _wordService.GetAllWords();
        return View(PaginatedList<Word>.Create(wordsList, pageNumber, PageSize));
    }

    public IActionResult SearchWords(string? wordPart)
    {
        ViewData["Title"] = "Search";
        if (string.IsNullOrEmpty(wordPart)) return View(new List<Word>());
        var models = _wordService.GetWordsByWordPart(wordPart);
        return View(models);
    }

    public IActionResult SearchWordsInfo(string? word)
    {
        ViewData["Title"] = "SearchInfo";

        var userInfo = new UserInfo
        {
            UserIp = "111.111.111.111"
        };
        if (string.IsNullOrEmpty(word)) return View("UserInfo", userInfo);

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var models = _wordService.GetAnagrams(word);
        stopWatch.Stop();

        userInfo.SearchTime = stopWatch.ElapsedMilliseconds;
        userInfo.WordList = new WordList
        {
            Word = word,
            Anagrams = models
        };
        return View("UserInfo", userInfo);
    }

    [HttpGet]
    public IActionResult AddWord()
    {
        ViewData["Title"] = "Database";
        return View();
    }

    /*[HttpPost]
    public IActionResult InsertWord(string? word)
    {
        ViewData["Title"] = "Database";
        if (string.IsNullOrEmpty(word))
            ModelState.AddModelError("Error", "Empty word!");
        else if (!_wordService.AddWord(word))
            ModelState.AddModelError("Error", "Couldn't save the word!");

        return View();
    }*/
}