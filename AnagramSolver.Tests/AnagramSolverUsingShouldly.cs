using System.Collections.Generic;
using System.Linq;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.Contracts.Interfaces;
using Moq;
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

        var repo = new Mock<IWordRepositoryOld>();
        repo.Setup(x => x.GetWords()).Returns(words.ToList());
        var solver = new AnagramSolverLogic();

        var expectedAnagrams = new List<string> { "laivas", "vailas" };

        //act
        var result = solver.Solve("svaila", words);

        //assert
        result.ShouldBe(expectedAnagrams, true);
    }
}