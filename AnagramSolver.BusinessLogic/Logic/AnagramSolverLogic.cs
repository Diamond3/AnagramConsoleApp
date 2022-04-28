using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Logic;

public class AnagramSolverLogic : IAnagramSolverLogic
{
    private readonly IWordRepository _repo;
    private HashSet<string> _words;

    public AnagramSolverLogic(IWordRepository repo)
    {
        _repo = repo;
    }

    public void LoadData(string filePath)
    {
        _words = _repo.GetWords(filePath);
    }
    public List<string> Solve(string input)
    {
        var anagrams = new List<string>();

        var inputArray = input.ToLower().ToArray();
        Array.Sort(inputArray);

        foreach (var word in _words)
        {
            var wordArray = word.ToLower().ToArray();
            Array.Sort(wordArray);

            if (input != word.ToLower() && inputArray.SequenceEqual(wordArray)) anagrams.Add(word);
        }

        return anagrams;
    }
}