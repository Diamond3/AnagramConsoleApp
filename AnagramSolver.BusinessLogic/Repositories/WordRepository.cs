using System.Diagnostics;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepository: IWordRepository
{
    private readonly IAnagramSolver _solver;
    private readonly IDataAccess<HashSet<string>> _data;
    private readonly HashSet<string> _dataSet;

    public WordRepository(IAnagramSolver solver, IDataAccess<HashSet<string>> data)
    {
        _solver = solver;
        _data = data;
        _dataSet = _data.ReadFile();

    }
    
    public List<string> GetAnagrams(string myWords)
    {
        return _solver.Solve(myWords, _dataSet);
    }
}