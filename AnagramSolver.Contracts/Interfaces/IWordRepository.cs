namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    List<string> GetAnagrams(string myWords);
}