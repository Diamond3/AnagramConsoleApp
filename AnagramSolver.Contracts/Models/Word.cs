namespace AnagramSolver.Contracts.Models
{
    public partial class Word
    {
        public int WordId { get; set; }
        public string FirstForm { get; set; } = null!;
        public string? Form { get; set; }
        public string? SecondForm { get; set; }
        public string? SortedForm { get; set; }
    }
}
