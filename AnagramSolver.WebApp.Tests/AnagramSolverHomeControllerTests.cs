using System.Collections.Generic;
using System.Linq;
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
        var wordServiceMock = new Mock<IWordsServiceOld>();
        var cookieServiceMock = new Mock<ICookieService>();
        var solver = new AnagramSolverLogic();
        var words = new HashSet<string> { "valia", "vailas", "laiivas", "lavas", "laivas", "balas", "tyras" };

        _controller = new HomeController(wordServiceMock.Object, solver, cookieServiceMock.Object);
        wordServiceMock.Setup(x => x.GetAllWords()).Returns(words.ToList());
    }

    [Test]
    public void TestIndex_NullString_EmptyView()
    {
        //act
        var result = _controller.Index(null) as ViewResult;

        //assert
        Assert.IsNull(result);
    }

    [Test]
    public void TestIndex_OneWord_ReturnsModelWithTwoValues()
    {
        //arrange
        var expectedWords = new List<string> { "laivas", "vailas" };

        //act
        var result = _controller.Index("svaila") as ViewResult;
        var actualWords = (WordList)result.Model;

        //assert
        CollectionAssert.AreEquivalent(expectedWords, actualWords.Anagrams);
    }
}