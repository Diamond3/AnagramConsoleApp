namespace AnagramSolver.EF.DatabaseFirst.Models;

public class CachedWord
{
    public int WordId { get; set; }
    public string? Word { get; set; }
    public string? Anagram { get; set; }
}