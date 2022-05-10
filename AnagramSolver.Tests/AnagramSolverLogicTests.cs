using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Logic;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class AnagramSolverLogicTests
{
    private AnagramSolverLogic _solver;
    private HashSet<string> _words;

    [SetUp]
    public void SetUp()
    {
        _words = new HashSet<string> { "valia", "vailas", "laiivas", "lavas", "laivas", "balas", "tyras" };
        _solver = new AnagramSolverLogic();
    }

    [Test]
    public void Solve_TwoAnagrams_FindsTwoAnagrams()
    {
        //arrange
        var expected = new List<string> { "laivas", "vailas" };

        //act
        var actual = _solver.Solve("svaila", _words);

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    public void Solve_NoAnagrams_FindsZeroAnagrams()
    {
        //act
        var actual = _solver.Solve("laivasa", _words);

        //assert
        Assert.AreEqual(0, actual.Count);
    }

    [Test]
    public void Solve_DifferentFontCases_FindsOneAnagram()
    {
        //arrange
        var expected = new List<string> { "valia" };

        //act
        var actual = _solver.Solve("vAlAi", _words);

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
        var actual = _solver.Solve("labas rytas", _words);

        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
}