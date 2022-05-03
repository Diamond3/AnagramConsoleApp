using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Logic;

public class AnagramSolverLogic : IAnagramSolverLogic
{
    public AnagramSolverLogic()
    {
        
    }
    public List<string> Solve(string input, IEnumerable<string> words)
    {
        var anagrams = new List<string>();

        var inputArray = input.ToLower().ToArray();
        Array.Sort(inputArray);

        foreach (var word in words)
        {
            var wordArray = word.ToLower().ToArray();
            Array.Sort(wordArray);

            if (!string.Equals(input, word, StringComparison.CurrentCultureIgnoreCase)
                && inputArray.SequenceEqual(wordArray)) anagrams.Add(word);
        }

        return anagrams;
    }
}