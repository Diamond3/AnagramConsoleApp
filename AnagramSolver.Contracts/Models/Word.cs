namespace AnagramSolver.Contracts.Models;

public class Word
{
    public int WordId { get; set; }
    public string FirstForm { get; set; } = null!;
    public string? Form { get; set; }
    public string? SecondForm { get; set; }
    public string? SortedForm { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var other = (Word)obj;
        return FirstForm == other.FirstForm
               && Form == other.Form
               && SecondForm == other.SecondForm
               && SortedForm == other.SortedForm;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstForm, Form, SecondForm, SortedForm);
    }
}