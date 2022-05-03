namespace AnagramSolver.Contracts.Interfaces;

public interface IAnagramSolverLogic
{
    List<string> Solve(string input, IEnumerable<string> words);
}