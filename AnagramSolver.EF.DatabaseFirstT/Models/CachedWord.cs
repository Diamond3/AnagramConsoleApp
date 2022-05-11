using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class CachedWord
    {
        public int WordId { get; set; }
        public string? Word { get; set; }
        public string? Anagram { get; set; }
    }
}
