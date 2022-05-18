using System.Diagnostics;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

public class HomeController : Controller
{
    private const int PageSize = 34;
    private readonly ICookieService _cookieService;
    private readonly IUserService _userService;
    private readonly IWordService<Word> _wordService;

    public HomeController(IWordService<Word> service, ICookieService cookieService, IUserService userService)
    {
        _wordService = service;
        _cookieService = cookieService;
        _userService = userService;
    }

    public async Task<IActionResult> Index(string? word)
    {
        ViewData["Title"] = "Word";

        var wordModel = new WordList
        {
            Word = "",
            Anagrams = new List<Word>()
        };

        var ip = "111.111.111.111";

        if (!_userService.AbleToDoAction(ip))
        {
            ModelState.AddModelError("Error", "0 actions left! Update/Add new word to get more actions");
            return View(wordModel);
        }

        if (string.IsNullOrEmpty(word)) return View(wordModel);

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

        wordModel.Word = word;
        wordModel.Anagrams = await _wordService.GetAnagramsAsync(word);

        _userService.DecreaseCount(ip);

        return View(wordModel);
    }

    public IActionResult Anagrams(int pageNumber = 1)
    {
        ViewData["Title"] = "Anagrams";
        var wordsList = _wordService.GetAllWords();
        return View("Anagrams", PaginatedList<Word>.Create(wordsList, pageNumber, PageSize));
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

    public IActionResult UpdateWord(int id, string word, int page)
    {
        var ip = "111.111.111.111";
        if (string.IsNullOrEmpty(word)) return Anagrams(page);

        _wordService.UpdateWord(id, word);
        _userService.IncreaseCount(ip);

        return Anagrams(page);
    }

    public IActionResult DeleteWord(int id, int page)
    {
        var ip = "111.111.111.111";

        if (!_userService.AbleToDoAction(ip))
        {
            ModelState.AddModelError("Error", "0 actions left! Update/Add new word to get more actions");
            return Anagrams(page);
        }

        _wordService.DeleteWord(id);
        _userService.DecreaseCount(ip);

        return Anagrams(page);
    }

    [HttpPost]
    public IActionResult AddWord(string? word)
    {
        ViewData["Title"] = "Database";
        var ip = "111.111.111.111";

        if (string.IsNullOrEmpty(word))
        {
            ModelState.AddModelError("Error", "Empty word!");
        }

        else if (_wordService.AddWord(word))
        {
            _userService.IncreaseCount(ip);
            ModelState.AddModelError("Error", "Added!");
        }
        else
        {
            ModelState.AddModelError("Error", "Can't save!");
        }

        return AddWord();
    }
}