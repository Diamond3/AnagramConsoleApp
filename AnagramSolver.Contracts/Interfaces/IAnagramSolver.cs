namespace AnagramSolver.Contracts.Interfaces;

public interface IAnagramSolver
{
    List<string> Solve(string input, HashSet<string> dataset);
}