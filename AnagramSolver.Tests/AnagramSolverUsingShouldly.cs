using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Logic;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.Tests;

public class AnagramSolverUsingShouldly
{
    [Test]
    public void Solve_TwoAnagrams_FindsTwoAnagrams()
    {
        //arrange
        var words = new HashSet<string> { "valia", "vailas", "laiivas", "lavas", "laivas", "balas", "tyras" };
        var solver = new AnagramSolverLogic();

        var expectedAnagrams = new List<string> { "laivas", "vailas" };

        //act
        var result = solver.Solve("svaila", words);

        //assert
        result.ShouldBe(expectedAnagrams, true);
    }
}