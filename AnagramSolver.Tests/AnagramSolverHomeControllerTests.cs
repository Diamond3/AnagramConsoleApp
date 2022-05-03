using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class AnagramSolverHomeControllerTests
{
    private HomeController _controller;
    [SetUp]
    public void SetUp()
    {
        var repo = new Mock<IWordRepository>();
        var serv = new Mock<IWordsService>();
        var solver = new AnagramSolverLogic();
        var hashSet = new HashSet<string> { "valia", "vailas", "laiivas", "lavas", "laivas", "balas", "tyras" };

        _controller = new HomeController(serv.Object, solver);
        serv.Setup(x => x.GetAllWords()).Returns(hashSet);
        
    }
    [Test]
    public void TestIndex_NullString_EmptyView()
    {
        //act
        var actual = _controller.Index(null) as ViewResult;
        
        //assert
        Assert.AreEqual(null, actual);
    }
    
    [Test]
    public void TestIndex_OneWord_EmptyView()
    {
        //arrange
        var expected = new List<string> { "laivas", "vailas" };

        //act
        var result = _controller.Index("svaila") as ViewResult;
        var actual = (WordList)result.Model;
        
        //assert
        CollectionAssert.AreEquivalent(expected, actual.Anagrams);
    }
}