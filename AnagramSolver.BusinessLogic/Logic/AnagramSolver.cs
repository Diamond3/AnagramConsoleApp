using System.Diagnostics;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Logic;

public class AnagramSolver: IAnagramSolver
{
    public List<string> Solve(string input, HashSet<string> dataSet)
    {
        var words = new List<string>();
        
        // var st = new Stopwatch();
        // st.Start();
        
        var inputArray = input.ToLower().ToArray();
        Array.Sort(inputArray);

        foreach (var word in dataSet)
        {
            var wordArray = word.ToArray();
            Array.Sort(wordArray);
            
            if (input != word && inputArray.SequenceEqual(wordArray))
            {
                words.Add(word);
            }
        }
        // st.Stop();
        // Console.WriteLine($"\nTook {st.ElapsedMilliseconds}ms to find");
        return words;
    }
}