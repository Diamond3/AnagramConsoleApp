using System.Collections.Generic;

namespace AnagramSolver.Tests;
using NUnit.Framework;

public class AnagramSolverTests
{
    [Test]
    public void Solve_TwoAnagrams_FindsTwoAnagrams()
    {
        //arrange
        var solver = new BusinessLogic.Logic.AnagramSolver();
        var expected = new List<string>() { "laivas", "vailas" };
        var hashSet = new HashSet<string>() { "valia", "vailas", "laiivas", "lavas", "laivas" };
        
        //act
        var actual = solver.Solve("svaila", hashSet);
        
        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Solve_NoAnagrams_FindsZeroAnagrams()
    {
        //arrange
        var solver = new BusinessLogic.Logic.AnagramSolver();
        var hashSet = new HashSet<string>() { "valia", "laivas", "laiivas", "lavas", "vailas" };
        
        //act
        var actual = solver.Solve("laivasa", hashSet);
        
        //assert
        Assert.AreEqual(0, actual.Count);
    }
    
    [Test]
    public void Solve_DifferentFontCases_FindsOneAnagram()
    {
        //arrange
        var solver = new BusinessLogic.Logic.AnagramSolver();
        var expected = new List<string>(){"valia"};
        var hashSet = new HashSet<string>() { "valia", "laivas", "laiivas", "lavas", "vailas" };
        
        //act
        var actual = solver.Solve("vAlAi", hashSet);
        
        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Solve_ShortSentence_FindsOneAnagram() //fails
    {
        //arrange
        var solver = new BusinessLogic.Logic.AnagramSolver();
        var expected = new List<string>(){"balas tyras"};
        var hashSet = new HashSet<string>() { "balas", "tyras", "ryte", "labai" };
        
        //act
        var actual = solver.Solve("labas rytas", hashSet);
        
        //assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
}