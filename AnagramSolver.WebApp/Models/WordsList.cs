using AnagramSolver.Contracts.Models;

namespace AnagramSolver.WebApp.Models;

public class WordList
{
    public List<WordModel> Anagrams = new();
    public string Word { get; set; }
}