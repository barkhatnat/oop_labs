using OOP_ICT.Enums;
using OOP_ICT.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    private readonly Card _cardDefault = new Card(Denomination.Ace, Suit.Spades);

    [Fact]
    public void CardIsNotNull()
    {
        Assert.NotNull(_cardDefault);
    }

    [Theory]
    [InlineData(Denomination.Two, Suit.Clubs)]
    [InlineData(Denomination.Ace, Suit.Hearts)]
    [InlineData(Denomination.Queen, Suit.Spades)]
    public void ShouldMakeCards(Denomination denomination, Suit
        suit)
    {
        var card1 = new Card(denomination, suit);
        Assert.NotNull(card1);
        Assert.Equal(denomination, card1.Denomination);
        Assert.Equal(suit, card1.Suit);
    }
}
