using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.Tests;

public class AnagramSolverUsingShouldly
{
    [Test]
    public void Solve_TwoAnagrams_FindsTwoAnagrams()
    {
        //arrange
        var solver = new BusinessLogic.Logic.AnagramSolver();
        var expectedAnagrams = new List<string>() { "laivas", "vailas" };
        var hashSet = new HashSet<string>() { "valia", "vailas", "laiivas", "lavas", "laivas" };
        
        //act
        var result = solver.Solve("svaila", hashSet);
        
        //assert
        result.ShouldBe(expectedAnagrams, ignoreOrder: true);
    }
}