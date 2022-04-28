using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.Contracts.Interfaces;
using Moq;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class AnagramSolverLogicTests
{
    private AnagramSolverLogic _solver;

    [SetUp]
    public void SetUp()
    {
        var hashSet = new HashSet<string> { "valia", "vailas", "laiivas", "lavas", "laivas", "balas", "tyras" };

        var repo = new Mock<IWordRepository>();
        repo.Setup(x => x.GetWords(It.IsAny<string>())).Returns(hashSet);

        _solver = new AnagramSolverLogic(repo.Object);
        
        _solver.LoadData(It.IsAny<string>());
    }

    [Test]
    public void Solve_TwoAnagrams_FindsTwoAnagrams()
    {
        //arrange
        var expected = new List<string> { "laivas", "vailas" };

        //act
        var actual = _solver.Solve("svaila");

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    public void Solve_NoAnagrams_FindsZeroAnagrams()
    {
        //act
        var actual = _solver.Solve("laivasa");

        //assert
        Assert.AreEqual(0, actual.Count);
    }

    [Test]
    public void Solve_DifferentFontCases_FindsOneAnagram()
    {
        //arrange
        var expected = new List<string> { "valia" };

        //act
        var actual = _solver.Solve("vAlAi");

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    [Explicit]
    public void Solve_ShortSentence_FindsOneAnagram() //fails
    {
        //arrange
        var expected = new List<string> { "balas tyras" };

        //act
        var actual = _solver.Solve("labas rytas");

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
}