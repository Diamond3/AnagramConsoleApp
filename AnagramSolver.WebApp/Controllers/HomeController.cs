using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordsService _service;
        private readonly IAnagramSolverLogic _solver;

        public HomeController(IWordsService service, IAnagramSolverLogic anagramSolver)
        {
            _service = service;
            _solver = anagramSolver;
        }
        
        public IActionResult Index(string? word)
        {
            if (string.IsNullOrEmpty(word))
                return new EmptyResult();

            ViewData["Title"] = "Word";
            ViewData["Word"] = word;

            var wordsList = _service.GetAllWords();
            var wordModel = new WordList()
            {
                Word = word,
                Anagrams = _solver.Solve(word, wordsList)
            };
            return View(wordModel);
        }

        public IActionResult Anagrams(int pageNumber = 1)
        {
            ViewData["Title"] = "Anagrams";
            var wordsList = _service.GetAllWords().ToList();

            var pageSize = 35;
            return View(PaginatedList<string>.Create(wordsList, pageNumber, pageSize));
        }
    }
}