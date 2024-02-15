using System.Collections.Generic;
using System.Collections.ObjectModel;
using OOP_ICT.Enums;
using OOP_ICT.Exceptions;
using OOP_ICT.Models;
using Xunit;
using Xunit.Abstractions;

namespace OOP_ICT.FIrst.Tests;

public class TestDealerFunctions
{
    private readonly Dealer _dealer = new Dealer();

    [Fact]
    public void HasCardDeck()
    {
        Assert.NotNull(_dealer.GetAllCards());
    }

    [Fact]
    public void ShouldGetAllCards()
    {
        Assert.Equal(new List<Card>
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
            new(Denomination.Two, Suit.Diamonds)
        }.AsReadOnly(), _dealer.GetAllCards());
    }

    [Fact]
    public void CheckCardDeck()
    {
        var dealerCards = _dealer.GetAllCards();
        Assert.Equal(new CardDeck().Cards, dealerCards);
    }

    [Fact]
    public void CheckShuffle()
    {
        _dealer.Shuffle();
        var dealerCards = _dealer.GetAllCards();;
        var expectedCardDeck = (new List<Card>
        {
            new(Denomination.Eight, Suit.Clubs),
            new(Denomination.Ace, Suit.Spades),
            new(Denomination.Eight, Suit.Diamonds),
            new(Denomination.Ace, Suit.Hearts),
            new(Denomination.Seven, Suit.Spades),
            new(Denomination.Ace, Suit.Clubs),
            new(Denomination.Seven, Suit.Hearts),
            new(Denomination.Ace, Suit.Diamonds),
            new(Denomination.Seven, Suit.Clubs),
            new(Denomination.King, Suit.Spades),
            new(Denomination.Seven, Suit.Diamonds),
            new(Denomination.King, Suit.Hearts),
            new(Denomination.Six, Suit.Spades),
            new(Denomination.King, Suit.Clubs),
            new(Denomination.Six, Suit.Hearts),
            new(Denomination.King, Suit.Diamonds),
            new(Denomination.Six, Suit.Clubs),
            new(Denomination.Queen, Suit.Spades),
            new(Denomination.Six, Suit.Diamonds),
            new(Denomination.Queen, Suit.Hearts),
            new(Denomination.Five, Suit.Spades),
            new(Denomination.Queen, Suit.Clubs),
            new(Denomination.Five, Suit.Hearts),
            new(Denomination.Queen, Suit.Diamonds),
            new(Denomination.Five, Suit.Clubs),
            new(Denomination.Jack, Suit.Spades),
            new(Denomination.Five, Suit.Diamonds),
            new(Denomination.Jack, Suit.Hearts),
            new(Denomination.Four, Suit.Spades),
            new(Denomination.Jack, Suit.Clubs),
            new(Denomination.Four, Suit.Hearts),
            new(Denomination.Jack, Suit.Diamonds),
            new(Denomination.Four, Suit.Clubs),
            new(Denomination.Ten, Suit.Spades),
            new(Denomination.Four, Suit.Diamonds),
            new(Denomination.Ten, Suit.Hearts),
            new(Denomination.Three, Suit.Spades),
            new(Denomination.Ten, Suit.Clubs),
            new(Denomination.Three, Suit.Hearts),
            new(Denomination.Ten, Suit.Diamonds),
            new(Denomination.Three, Suit.Clubs),
            new(Denomination.Nine, Suit.Spades),
            new(Denomination.Three, Suit.Diamonds),
            new(Denomination.Nine, Suit.Hearts),
            new(Denomination.Two, Suit.Spades),
            new(Denomination.Nine, Suit.Clubs),
            new(Denomination.Two, Suit.Hearts),
            new(Denomination.Nine, Suit.Diamonds),
            new(Denomination.Two, Suit.Clubs),
            new(Denomination.Eight, Suit.Spades),
            new(Denomination.Two, Suit.Diamonds),
            new(Denomination.Eight, Suit.Hearts)
        }).AsReadOnly();
        Assert.Equal(expectedCardDeck, dealerCards);
    }

    [Fact]
    public void ShouldGetOneCard()
    {
        _dealer.Shuffle();
        Assert.Equal(new Card(Denomination.Eight, Suit.Clubs), _dealer.GetOneCard());
        Assert.Equal(new List<Card>
        {
            new(Denomination.Ace, Suit.Spades),
            new(Denomination.Eight, Suit.Diamonds),
            new(Denomination.Ace, Suit.Hearts),
            new(Denomination.Seven, Suit.Spades),
            new(Denomination.Ace, Suit.Clubs),
            new(Denomination.Seven, Suit.Hearts),
            new(Denomination.Ace, Suit.Diamonds),
            new(Denomination.Seven, Suit.Clubs),
            new(Denomination.King, Suit.Spades),
            new(Denomination.Seven, Suit.Diamonds),
            new(Denomination.King, Suit.Hearts),
            new(Denomination.Six, Suit.Spades),
            new(Denomination.King, Suit.Clubs),
            new(Denomination.Six, Suit.Hearts),
            new(Denomination.King, Suit.Diamonds),
            new(Denomination.Six, Suit.Clubs),
            new(Denomination.Queen, Suit.Spades),
            new(Denomination.Six, Suit.Diamonds),
            new(Denomination.Queen, Suit.Hearts),
            new(Denomination.Five, Suit.Spades),
            new(Denomination.Queen, Suit.Clubs),
            new(Denomination.Five, Suit.Hearts),
            new(Denomination.Queen, Suit.Diamonds),
            new(Denomination.Five, Suit.Clubs),
            new(Denomination.Jack, Suit.Spades),
            new(Denomination.Five, Suit.Diamonds),
            new(Denomination.Jack, Suit.Hearts),
            new(Denomination.Four, Suit.Spades),
            new(Denomination.Jack, Suit.Clubs),
            new(Denomination.Four, Suit.Hearts),
            new(Denomination.Jack, Suit.Diamonds),
            new(Denomination.Four, Suit.Clubs),
            new(Denomination.Ten, Suit.Spades),
            new(Denomination.Four, Suit.Diamonds),
            new(Denomination.Ten, Suit.Hearts),
            new(Denomination.Three, Suit.Spades),
            new(Denomination.Ten, Suit.Clubs),
            new(Denomination.Three, Suit.Hearts),
            new(Denomination.Ten, Suit.Diamonds),
            new(Denomination.Three, Suit.Clubs),
            new(Denomination.Nine, Suit.Spades),
            new(Denomination.Three, Suit.Diamonds),
            new(Denomination.Nine, Suit.Hearts),
            new(Denomination.Two, Suit.Spades),
            new(Denomination.Nine, Suit.Clubs),
            new(Denomination.Two, Suit.Hearts),
            new(Denomination.Nine, Suit.Diamonds),
            new(Denomination.Two, Suit.Clubs),
            new(Denomination.Eight, Suit.Spades),
            new(Denomination.Two, Suit.Diamonds),
            new(Denomination.Eight, Suit.Hearts)
        }.AsReadOnly(), _dealer.GetAllCards());
    }

    [Fact]
    public void ShouldGetSeveralCards()
    {
        _dealer.Shuffle();
        Assert.Equal(new List<Card>
        {
            new(Denomination.Eight, Suit.Clubs),
            new(Denomination.Ace, Suit.Spades)
        }, _dealer.GetSeveralCards(2));
        Assert.Equal(new List<Card>
        {
            new(Denomination.Eight, Suit.Diamonds),
            new(Denomination.Ace, Suit.Hearts),
            new(Denomination.Seven, Suit.Spades),
            new(Denomination.Ace, Suit.Clubs),
            new(Denomination.Seven, Suit.Hearts),
            new(Denomination.Ace, Suit.Diamonds),
            new(Denomination.Seven, Suit.Clubs),
            new(Denomination.King, Suit.Spades),
            new(Denomination.Seven, Suit.Diamonds),
            new(Denomination.King, Suit.Hearts),
            new(Denomination.Six, Suit.Spades),
            new(Denomination.King, Suit.Clubs),
            new(Denomination.Six, Suit.Hearts),
            new(Denomination.King, Suit.Diamonds),
            new(Denomination.Six, Suit.Clubs),
            new(Denomination.Queen, Suit.Spades),
            new(Denomination.Six, Suit.Diamonds),
            new(Denomination.Queen, Suit.Hearts),
            new(Denomination.Five, Suit.Spades),
            new(Denomination.Queen, Suit.Clubs),
            new(Denomination.Five, Suit.Hearts),
            new(Denomination.Queen, Suit.Diamonds),
            new(Denomination.Five, Suit.Clubs),
            new(Denomination.Jack, Suit.Spades),
            new(Denomination.Five, Suit.Diamonds),
            new(Denomination.Jack, Suit.Hearts),
            new(Denomination.Four, Suit.Spades),
            new(Denomination.Jack, Suit.Clubs),
            new(Denomination.Four, Suit.Hearts),
            new(Denomination.Jack, Suit.Diamonds),
            new(Denomination.Four, Suit.Clubs),
            new(Denomination.Ten, Suit.Spades),
            new(Denomination.Four, Suit.Diamonds),
            new(Denomination.Ten, Suit.Hearts),
            new(Denomination.Three, Suit.Spades),
            new(Denomination.Ten, Suit.Clubs),
            new(Denomination.Three, Suit.Hearts),
            new(Denomination.Ten, Suit.Diamonds),
            new(Denomination.Three, Suit.Clubs),
            new(Denomination.Nine, Suit.Spades),
            new(Denomination.Three, Suit.Diamonds),
            new(Denomination.Nine, Suit.Hearts),
            new(Denomination.Two, Suit.Spades),
            new(Denomination.Nine, Suit.Clubs),
            new(Denomination.Two, Suit.Hearts),
            new(Denomination.Nine, Suit.Diamonds),
            new(Denomination.Two, Suit.Clubs),
            new(Denomination.Eight, Suit.Spades),
            new(Denomination.Two, Suit.Diamonds),
            new(Denomination.Eight, Suit.Hearts)
        }.AsReadOnly(), _dealer.GetAllCards());
    }

    [Fact]
    public void ErrorIfMoreCards()
    {
        var exceptionForSeveralCards = Assert.Throws<CardNumberException>(() => _dealer.GetSeveralCards(54));
        Assert.Equal("Not enough cards in the deck", exceptionForSeveralCards.Message);
        _dealer.GetSeveralCards(52);
        var exceptionForOneCard = Assert.Throws<CardNumberException>(() => _dealer.GetOneCard());
        Assert.Equal("Not enough cards in the deck", exceptionForOneCard.Message);
    }
}