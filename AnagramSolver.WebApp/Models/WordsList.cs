namespace AnagramSolver.WebApp.Models;
public class WordList
{
    public string Word { get; set; }
    public List<string> Anagrams = new ();
}