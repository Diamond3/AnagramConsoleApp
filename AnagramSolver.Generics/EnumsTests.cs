using System;
using NUnit.Framework;

namespace AnagramSolver.Generics;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MapIntToGender_ShouldReturnEnum_WhenGivenExistingInt()
    {
        //Arrange
        var num = 2;
        var gender = Enums.Gender.Female;

        //Act
        var result = Enums.MapIntToGender(num);

        //Assert
        Assert.AreEqual(gender, result);
    }

    [Test]
    public void MapStringToGender_ShouldReturnEnum_WhenGivenExistingString()
    {
        //Arrange
        var str = "Male";
        var gender = Enums.Gender.Male;

        //Act
        var result = Enums.MapStringToGender(str);

        //Assert
        Assert.AreEqual(gender, result);
    }

    [Test]
    public void MapStringToWeekday_ShouldReturnEnum_WhenGivenExistingString()
    {
        //Arrange
        var str = "Monday";
        var gender = Enums.Weekday.Monday;

        //Act
        var result = Enums.MapStringToWeekday(str);

        //Assert
        Assert.AreEqual(gender, result);
    }
    [Test]
    public void GenericEnumConverter_ShouldReturnWeekdayEnum_WhenGivenExistingString()
    {
        //Arrange
        var str = "Monday";
        var gender = Enums.Weekday.Monday;

        //Act
        var result = Enums.MapValueToEnum<Enums.Weekday, string>(str);

        //Assert
        Assert.AreEqual(gender, result);
    }
    [Test]
    public void GenericEnumConverter_ShouldReturnGenderEnum_WhenGivenExistingString()
    {
        //Arrange
        var str = "Female";
        var gender = Enums.Gender.Female;

        //Act
        var result = Enums.MapValueToEnum<Enums.Gender, string>(str);

        //Assert
        Assert.AreEqual(gender, result);
    }
    [Test]
    public void GenericEnumConverter_ShouldReturnWeekdayEnum_WhenGivenExistingInt()
    {
        //Arrange
        var num = 2;
        var gender = Enums.Weekday.Wednesday;

        //Act
        var result = Enums.MapValueToEnum<Enums.Weekday, int>(num);

        //Assert
        Assert.AreEqual(gender, result);
    }
}