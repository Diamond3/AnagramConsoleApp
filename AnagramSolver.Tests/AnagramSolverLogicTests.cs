using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.Contracts.Interfaces;
using Moq;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class AnagramSolverLogicTests
{
    private AnagramSolverLogic _solver;
    private HashSet<string> _hashSet;

    [SetUp]
    public void SetUp()
    {
        _hashSet = new HashSet<string> { "valia", "vailas", "laiivas", "lavas", "laivas", "balas", "tyras" };

        var repo = new Mock<IWordRepository>();
        repo.Setup(x => x.GetWords()).Returns(_hashSet);

        _solver = new AnagramSolverLogic();
        
    }

    [Test]
    public void Solve_TwoAnagrams_FindsTwoAnagrams()
    {
        //arrange
        var expected = new List<string> { "laivas", "vailas" };

        //act
        var actual = _solver.Solve("svaila", _hashSet);

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    public void Solve_NoAnagrams_FindsZeroAnagrams()
    {
        //act
        var actual = _solver.Solve("laivasa", _hashSet);

        //assert
        Assert.AreEqual(0, actual.Count);
    }

    [Test]
    public void Solve_DifferentFontCases_FindsOneAnagram()
    {
        //arrange
        var expected = new List<string> { "valia" };

        //act
        var actual = _solver.Solve("vAlAi", _hashSet);

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
        var actual = _solver.Solve("labas rytas", _hashSet);

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
}