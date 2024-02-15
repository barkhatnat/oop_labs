using System.Collections.ObjectModel;
using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestBlackjackStartGameFunctions
{
    private readonly BlackjackDealer _dealer = new BlackjackDealer();
    private readonly BlackjackGame _game;
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BlackjackCasino _blackjackCasino = new BlackjackCasino();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestBlackjackStartGameFunctions()
    {
        _game = new BlackjackGame(_dealer, _blackjackCasino);
        _debitCreator = new DebitAccountCreator("DefaultDebitAccountCreatorName", _repository);
        _casinoAccountFactory = new CasinoAccountAccountFactory("DefaultCasinoAccountCreatorName", _repository);
        _creditCreator = new CreditAccountCreator("DefaultCreditAccountCreatorName", _repository);
        var accountPlayer1 = _debitCreator.Create(_player1, 1000);
        var accountPlayer2 = _creditCreator.Create(_player2, 500);
        var casinoAccount1 = _casinoAccountFactory.Create(_player1);
        var casinoAccount2 = _casinoAccountFactory.Create(_player2);
        casinoAccount1.TransferMoneyAdd(1000);
        casinoAccount2.TransferMoneyAdd(500);
    }

    private readonly Player _player1 = new Player("1234567890", "Player1Name", "Player1Surname");
    private readonly Player _player2 = new Player("1111111111", "Player2Name", "Player2Surname");

    [Fact]
    public void AddPlayersAndCheckBets()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 300);
        Assert.Equal(new List<Player>() { _player1, _player2 }, _game.Players);
        Assert.Equal(new List<Player>() { _player1, _player2 }, _game.BetCatalog.Keys);
        Assert.Equal(1000, _game.BetCatalog[_player1]);
        Assert.Equal(300, _game.BetCatalog[_player2]);
        Assert.Equal(0, _player1.CasinoAccount.Balance);
        Assert.Equal(200, _player2.CasinoAccount.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1001)]
    [InlineData(-344)]
    public void CheckBet_NotMoreThanBalanceAndNotNegative(int bet)
    {
        var exception = Assert.Throws<NoMoneyException>(() => _game.AddPlayer(_player1, bet));
        Assert.Equal("Not enough money in the player's casino account to place a bet", exception.Message);
    }

    [Fact]
    public void CheckNumberOfCards()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        Assert.Equal(2, _player1.PlayersHand.Cards.Count);
        Assert.Equal(2, _player2.PlayersHand.Cards.Count);
        Assert.Single(_dealer.DealersHand.Cards);
    }
    

    [Fact]
    public void CheckAddCard()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        _player1.AskForCards(_dealer, 1);
        _player2.AskForCards(_dealer, 2);
        Assert.Equal(3, _player1.PlayersHand.Cards.Count);
        Assert.Equal(4, _player2.PlayersHand.Cards.Count);
    }

    [Fact]
    public void CheckCountScoreWhereAceIs11()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        Assert.Equal(8 + 11, _game.Players[0].PlayersHand.BlackjackCounter());
        Assert.Equal(8 + 11, _game.Players[1].PlayersHand.BlackjackCounter());
    }

    [Theory]
    [InlineData(Denomination.Ace, Denomination.Jack, 21)]
    [InlineData(Denomination.Ace, Denomination.Eight, 19)]
    [InlineData(Denomination.Ace, Denomination.Ace, 12)]
    public void CheckBlackjackCounter(Denomination denomination1, Denomination denomination2, int result)
    {
        var cardDeck = new BlackjackHand();
        var cardList = new List<Card>();
        cardList.Add(new Card(denomination1, Suit.Clubs));
        cardList.Add(new Card(denomination2, Suit.Diamonds));
        cardDeck.Cards = new ReadOnlyCollection<Card>(cardList);
        Assert.Equal(result, cardDeck.BlackjackCounter());
    }
}