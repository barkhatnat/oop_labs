using OOP_ICT.Fourth.Classes;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class TestStartPokerGame
{
    private readonly PokerGame _game;
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly PokerCasino _pokerCasino = new PokerCasino();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestStartPokerGame()
    {
        _game = new PokerGame(_pokerCasino);
        _debitCreator = new DebitAccountCreator("DefaultDebitAccountCreatorName", _repository);
        _casinoAccountFactory = new CasinoAccountAccountFactory("DefaultCasinoAccountCreatorName", _repository);
        _creditCreator = new CreditAccountCreator("DefaultCreditAccountCreatorName", _repository);
        var accountPlayer1 = _debitCreator.Create(_player1, 10000);
        var accountPlayer2 = _creditCreator.Create(_player2, 5000);
        var accountPlayer3 = _creditCreator.Create(_player3, 25000);

        var casinoAccount1 = _casinoAccountFactory.Create(_player1);
        var casinoAccount2 = _casinoAccountFactory.Create(_player2);
        var casinoAccount3 = _casinoAccountFactory.Create(_player3);

        casinoAccount1.TransferMoneyAdd(10000);
        casinoAccount2.TransferMoneyAdd(5000);
        casinoAccount3.TransferMoneyAdd(25000);
    }

    private readonly Player _player1 = new Player("1234567890", "Player1Name", "Player1Surname");
    private readonly Player _player2 = new Player("1111111111", "Player2Name", "Player2Surname");
    private readonly Player _player3 = new Player("9999999999", "Player3Name", "Player3Surname");

    [Fact]
    public void AddPlayersAndStartGame()
    {
        Assert.Equal(PokerStatus.JoiningPlayers, _game.GameStatus);
        Assert.Empty(_game.Players);
        _game.AddPlayer(_player1, true);
        Assert.Single(_game.Players);
        Assert.Contains(_player1, _game.Players.ToList());
        _game.AddPlayer(_player2, false);
        Assert.Equal(2, _game.Players.Count);
        Assert.Contains(_player2, _game.Players.ToList());
        _game.AddPlayer(_player3, false);
        Assert.Equal(3, _game.Players.Count);
        Assert.Contains(_player3, _game.Players.ToList());
    }

    [Fact]
    public void ShouldBeNoMoreThanOneDealingPlayer()
    {
        _game.AddPlayer(_player1, true);
        var exception = Assert.Throws<DealingPlayerException>(() => _game.AddPlayer(_player2, true));
        Assert.Equal(_player1.Name + " is dealer of this game", exception.Message);
    }

    [Fact]
    public void ShouldBeAtLeastOneDealingPlayer()
    {
        _game.AddPlayer(_player1, false);
        _game.AddPlayer(_player2, false);
        _game.AddPlayer(_player3, false);
        var exception = Assert.Throws<DealingPlayerException>(() => _game.StartGame());
        Assert.Equal("Game can't be start without dealer", exception.Message);
    }
}