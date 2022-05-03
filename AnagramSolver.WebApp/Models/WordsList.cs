using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Models;
public class WordList
{
    public string Word { get; set; }
    public List<string> Anagrams { get; set; }
}