using OOP_ICT.Enums;
using OOP_ICT.Fourth.Classes;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class TestDealingPlayerFunctions
{
    private readonly Dealer _dealer = new Dealer();
    private readonly DealingPlayer _dealingPlayer;
    private readonly Player _player = new Player("1234567890", "Name", "Surname");
    private readonly Player _newPlayer = new Player("0987654321", "NewName", "NewSurname");

    public TestDealingPlayerFunctions()
    {
        _dealingPlayer = new DealingPlayer(_player);
    }

    [Fact]
    public void CheckShuffle()
    {
        _dealingPlayer.Shuffle();
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
        Assert.Equal(expectedCardDeck, _dealingPlayer.GetAllCards());
    }

    [Fact]
    public void CheckDealCardsToHimself()
    {
        _dealingPlayer.Shuffle();
        _dealingPlayer.AskForCards(_dealingPlayer, 2);
        var expectedDealerCardDeck = (new List<Card>
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
        }).AsReadOnly();
        var expectedPlayerCardDeck = (new List<Card>
        {
            new(Denomination.Eight, Suit.Clubs),
            new(Denomination.Ace, Suit.Spades)
        }).AsReadOnly();
        Assert.Equal(expectedDealerCardDeck, _dealingPlayer.GetAllCards());
        Assert.Equal(expectedPlayerCardDeck, _dealingPlayer.Player.PlayersHand.Cards);
    }

    [Fact]
    public void CheckChangePlayer()
    {
        _dealingPlayer.Shuffle();
        _dealingPlayer.AskForCards(_dealingPlayer, 2);
        _dealingPlayer.ChangeDealingPlayer(_newPlayer);
        var expectedDealerCardDeck = (new List<Card>
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
        }).AsReadOnly();
        Assert.Equal(expectedDealerCardDeck, _dealingPlayer.GetAllCards());
        Assert.Empty(_dealingPlayer.Player.PlayersHand.Cards);
    }
}