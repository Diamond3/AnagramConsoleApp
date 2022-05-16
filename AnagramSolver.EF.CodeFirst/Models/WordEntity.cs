namespace AnagramSolver.EF.CodeFirst.Models;

public class WordEntity
{
    public int Id { get; set; }
    public string FirstForm { get; set; }
    public string? Form { get; set; }
    public string? SecondForm { get; set; }
    public string? SortedForm { get; set; }
}