using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Shouldly;


namespace AnagramSolver.BusinessLogic.Tests.ServicesTests;

[TestFixture]
public class WordServiceTest
{
    private Mock<IWordRepository> _wordRepositoryMock;
    private WordService _wordService;
    
    [SetUp]
    public void Setup()
    {
        _wordRepositoryMock = new Mock<IWordRepository>();
        _wordService = new WordService(_wordRepositoryMock.Object);

        var cachedWordsList = new List<Word>()
        {
            new Word() { FirstForm = "naujasZodis" },
            new Word() { FirstForm = "kitasZodis" }
        };
        var wordsList = new List<Word>()
        {
            new Word() { FirstForm = "zzodis" },
            new Word() { FirstForm = "kitasZodis" }
        };
        
        _wordRepositoryMock
            .Setup(x => x.GetAnagramsFromCachedWord(It.IsAny<string>()))
            .ReturnsAsync(cachedWordsList);
        _wordRepositoryMock
            .Setup(x => x.GetAllWordsBySortedForm(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(wordsList);
        _wordRepositoryMock
            .Setup(x => x.GetWords())
            .ReturnsAsync(wordsList);
    }

    [Test]
    public async Task GetAllWords_ShouldGetAllWords()
    {
        //Arrange
        var expectedList = new List<Word>()
        {
            new Word() { FirstForm = "kitasZodis" },
            new Word() { FirstForm = "zzodis" }
        };
        
        //Act
        var wordList = await _wordService.GetAllWords();
        
        //Assert
        wordList.ShouldBe(expectedList, ignoreOrder: true);
    }
    
    [Test]
    public async Task GetAnagrams_ShouldGetAllAnagrams_WithoutCallingGetAllWordsBySortedForm()
    {
        //Arrange
        var word = "zodis";
        var chars = word.ToLower().ToLower().ToCharArray();
        Array.Sort(chars);
        var sortedWord = new string(chars);
        
        var expectedList = new List<Word>()
        {
            new Word() { FirstForm = "naujasZodis" },
            new Word() { FirstForm = "kitasZodis" }
        };
        
        //Act
        var anagramsList = await _wordService.GetAnagrams(word);
        
        //Assert
        _wordRepositoryMock.Verify(w => w.GetAnagramsFromCachedWord(word), Times.Once());
        _wordRepositoryMock.Verify(w => w.GetAllWordsBySortedForm(sortedWord, word), Times.Never());
        anagramsList.ShouldBe(expectedList, ignoreOrder: true);
    }
    
    [Test]
    public async Task GetAnagrams_ShouldGetAllAnagrams()
    {
        //Arrange
        var word = "zodis";
        var chars = word.ToLower().ToLower().ToCharArray();
        Array.Sort(chars);
        var sortedWord = new string(chars);
        
        var expectedList = new List<Word>()
        {
            new Word() { FirstForm = "kitasZodis" },
            new Word() { FirstForm = "zzodis" }
        };
        
        _wordRepositoryMock
            .Setup(x => x.GetAnagramsFromCachedWord(It.IsAny<string>()))
            .ReturnsAsync(new List<Word>());
        
        //Act
        var anagramsList = await _wordService.GetAnagrams(word);
        
        //Assert
        _wordRepositoryMock.Verify(w => w.GetAnagramsFromCachedWord(word), Times.Once());
        _wordRepositoryMock.Verify(w => w.GetAllWordsBySortedForm(sortedWord, word), Times.Once());
        anagramsList.ShouldBe(expectedList, ignoreOrder: true);
    }
    
    [Test]
    public async Task GetAnagrams_ShouldReturnEmpty_WhenGivenNullWord()
    {
        //Arrange

        var emptyList = new List<Word>();
        
        _wordRepositoryMock
            .Setup(x => x.GetAnagramsFromCachedWord(It.IsAny<string>()))
            .ReturnsAsync(new List<Word>());
        
        //Act
        var anagramsList = await _wordService.GetAnagrams(null);
        
        //Assert
        _wordRepositoryMock.Verify(w => w.GetAnagramsFromCachedWord(It.IsAny<string?>()), Times.Never());
        _wordRepositoryMock.Verify(w => w.GetAllWordsBySortedForm(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never());
        anagramsList.ShouldBe(emptyList);
    }
}