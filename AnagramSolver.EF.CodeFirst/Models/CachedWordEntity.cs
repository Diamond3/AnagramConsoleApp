namespace AnagramSolver.EF.CodeFirst.Models;

public class CachedWordEntity
{
    public int Id { get; set; }
    public string? Word { get; set; }
    public string? Anagram { get; set; }
}