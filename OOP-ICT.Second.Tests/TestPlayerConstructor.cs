using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.Second.Tests;

public class TestPlayerConstructor
{
    private Player _player = new Player("1234567890", "DefaultName", "DefaultSurname");

    [Fact]
    public void PlayerIsNotNull()
    {
        Assert.NotNull(_player);
    }

    [Theory]
    [InlineData("12345678900000")]
    [InlineData("passport")]
    [InlineData("12abcd1234")]
    public void CheckPlayerPassport_ExceptionIfLettersAndHasNo10Symbols(string passport)
    {
        var exception =
            Assert.Throws<IncorrectPassportException>(() => new Player(passport, "DefaultName", "DefaultSurname"));
        Assert.Equal("Passport number can't be negative or contain letters", exception.Message);
    }

    [Theory]
    [InlineData("1234567890")]
    [InlineData("6565748392")]
    public void CheckPlayerPassport_HasNoLettersAndHas10Symbols(string passport)
    {
        var exception = Record.Exception(() => new Player(passport, "DefaultName", "DefaultSurname"));
        Assert.Null(exception);
    }
}