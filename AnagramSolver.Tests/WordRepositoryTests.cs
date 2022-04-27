using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.Contracts.Interfaces;
using NUnit.Framework;
using Moq;
namespace AnagramSolver.Tests;

public class WordRepositoryTests
{
    [Test]
    public void GetAnagrams_Test()
    {
        //arrange
        var data = new Mock<IDataAccess<HashSet<string>>>();
        var solver = new Mock<IAnagramSolver>();
        var repo = new WordRepository(solver.Object, data.Object);
        var hashSet = new HashSet<string>() { "balas", "tyras" };
        
        var expected = new List<string>() { "tyras" };
        
        data.Setup(x => x.ReadFile()).Returns(hashSet);
        solver.Setup(x => x.Solve("rytas", It.IsAny<HashSet<string>>()))
                .Returns(new List<string>() { "tyras" });
        
        //act
        var actual = repo.GetAnagrams("rytas");
        
        //assert
        data.Verify(v => v.ReadFile(), Times.Once());
        solver.Verify(v => v.Solve("rytas", It.IsAny<HashSet<string>>()), Times.Once());
        CollectionAssert.AreEquivalent(expected, actual);
    }
}