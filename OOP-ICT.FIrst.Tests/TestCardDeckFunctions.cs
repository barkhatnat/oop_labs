using System.Collections.Generic;
using System.Collections.ObjectModel;
using OOP_ICT.Enums;
using OOP_ICT.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.FIrst.Tests;

public class TestCardDeckFunctions
{
    private readonly CardDeck _cardDeck = new CardDeck();

    [Fact]
    public void CheckNotNull()
    {
        Assert.NotNull(_cardDeck);
    }
    [Fact]
    public void CheckLength()
    {
        Assert.Equal(52, _cardDeck.Cards.Count);
    }

    [Fact]
    public void CheckSort()
    {
        var expectedCardDeck = (new List<Card>
        {
            new(Denomination.Ace, Suit.Spades),
            new(Denomination.Ace, Suit.Hearts),
            new(Denomination.Ace, Suit.Clubs),
            new(Denomination.Ace, Suit.Diamonds),
            
            new(Denomination.King, Suit.Spades),
            new(Denomination.King, Suit.Hearts),
            new(Denomination.King, Suit.Clubs),
            new(Denomination.King, Suit.Diamonds),
            
            new(Denomination.Queen, Suit.Spades),
            new(Denomination.Queen, Suit.Hearts),
            new(Denomination.Queen, Suit.Clubs),
            new(Denomination.Queen, Suit.Diamonds),
            
            new(Denomination.Jack, Suit.Spades),
            new(Denomination.Jack, Suit.Hearts),
            new(Denomination.Jack, Suit.Clubs),
            new(Denomination.Jack, Suit.Diamonds),
            
            new(Denomination.Ten, Suit.Spades),
            new(Denomination.Ten, Suit.Hearts),
            new(Denomination.Ten, Suit.Clubs),
            new(Denomination.Ten, Suit.Diamonds), 
            
            new(Denomination.Nine, Suit.Spades),
            new(Denomination.Nine, Suit.Hearts),
            new(Denomination.Nine, Suit.Clubs),
            new(Denomination.Nine, Suit.Diamonds),
            
            new(Denomination.Eight, Suit.Spades),
            new(Denomination.Eight, Suit.Hearts),
            new(Denomination.Eight, Suit.Clubs),
            new(Denomination.Eight, Suit.Diamonds),
            
            new(Denomination.Seven, Suit.Spades),
            new(Denomination.Seven, Suit.Hearts),
            new(Denomination.Seven, Suit.Clubs),
            new(Denomination.Seven, Suit.Diamonds),
            
            new(Denomination.Six, Suit.Spades),
            new(Denomination.Six, Suit.Hearts),
            new(Denomination.Six, Suit.Clubs),
            new(Denomination.Six, Suit.Diamonds),
            
            new(Denomination.Five, Suit.Spades),
            new(Denomination.Five, Suit.Hearts),
            new(Denomination.Five, Suit.Clubs),
            new(Denomination.Five, Suit.Diamonds),
            
            new(Denomination.Four, Suit.Spades),
            new(Denomination.Four, Suit.Hearts),
            new(Denomination.Four, Suit.Clubs),
            new(Denomination.Four, Suit.Diamonds),
            
            new(Denomination.Three, Suit.Spades),
            new(Denomination.Three, Suit.Hearts),
            new(Denomination.Three, Suit.Clubs),
            new(Denomination.Three, Suit.Diamonds),
            
            new(Denomination.Two, Suit.Spades),
            new(Denomination.Two, Suit.Hearts),
            new(Denomination.Two, Suit.Clubs),
            new(Denomination.Two, Suit.Diamonds),
        }).AsReadOnly();
        Assert.Equal(expectedCardDeck, _cardDeck.Cards);
    }
}