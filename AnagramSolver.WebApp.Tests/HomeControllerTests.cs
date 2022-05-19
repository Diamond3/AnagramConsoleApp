using System.Collections.Generic;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.WebApp.Tests;

public class HomeControllerTests: ControllerBase
{
    private HomeController _homeController;
    [SetUp]
    public void SetUp()
    {
        var wordServiceMock = new Mock<IWordService<Word>>();
        var cookieServiceMock = new Mock<ICookieService>();
        var userServiceMock = new Mock<IUserService>();
        
        var wordsList = new List<Word>()
        {
            new Word() { FirstForm = "zzodis" },
            new Word() { FirstForm = "kitasZodis" }
        };

        _homeController = new HomeController(wordServiceMock.Object, cookieServiceMock.Object, userServiceMock.Object);
        
        wordServiceMock.Setup(x => x.GetAllWords())
            .ReturnsAsync(wordsList);
        wordServiceMock.Setup(x => x.GetAnagrams(It.IsAny<string>()))
            .ReturnsAsync(wordsList);
        
        cookieServiceMock.Setup(x => x.GetCount(It.IsAny<string>()))
            .Returns(0);
        cookieServiceMock.Setup(x => x.CanAddValue(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Returns(false);
        
        userServiceMock.Setup(x => x.AbleToDoAction(It.IsAny<string>()))
            .Returns(true);
    }

    [Explicit, Test]
    public async Task Index_ShouldReturnAnagramsList()
    {
        //Arrange
        var expectedAnagramsList = new List<Word>()
        {
            new Word() { FirstForm = "kitasZodis" },
            new Word() { FirstForm = "zzodis" }
        };
        var expectedWord = "zzodis";


        ViewResult viewResult;
        WordList wordList;

        List<Word> actualAnagramsList = new List<Word>();
        string actualWord = "";
        
        //Act
        viewResult = await _homeController.Index(expectedWord) as ViewResult;
        wordList = (WordList)viewResult.Model;

        actualAnagramsList = wordList.Anagrams;
        actualWord = wordList.Word;

        //Assert
        actualAnagramsList.ShouldBe(expectedAnagramsList, ignoreOrder: true);
        actualWord.ShouldBe(expectedWord);
    }
    
    [Test]
    public async Task SearchAnagramsWithInfo_ReturnsModelWithAllAnagrams()
    {
        //Arrange
        var expectedAnagramsList = new List<Word>()
        {
            new Word() { FirstForm = "kitasZodis" },
            new Word() { FirstForm = "zzodis" }
        };
        var expectedWord = "zzodis";

        //Act
        var viewResult = await _homeController.SearchWordsInfo(expectedWord) as ViewResult;
        var userInfo = (UserInfo)viewResult.Model;

        var actualAnagramsList = userInfo.WordList.Anagrams;
        var actualWord = userInfo.WordList.Word;
        
        //Assert
        actualAnagramsList.ShouldBe(expectedAnagramsList, ignoreOrder: true);
        actualWord.ShouldBe(expectedWord);
    }
}