namespace OOP_ICT.Fourth.Tests;

using OOP_ICT.Fourth.Classes;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using Xunit;

public class TestWinners

{
    private readonly PokerGame _game;
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly PokerCasino _pokerCasino = new PokerCasino();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestWinners()
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
    public void DealingPlayerLeavesTheGame()
    {
        _game.AddPlayer(_player1, true);
        _game.AddPlayer(_player2, false);
        _game.AddPlayer(_player3, false);
        _game.StartGame();
        _game.MakeFirstBlindBet(_player2, 50);
        _game.MakeFirstBlindBet(_player3, 100);
        _game.Flop();
        _game.Raise(_player1, 1000);
        _game.Raise(_player2, 1100);
        _game.Call(_player3);
        _game.Fold(_player1);
        Assert.Equal(1000, _game.Bank);
        Assert.Equal(_player2, _game.CurrentDealer.Player);
    }
    [Fact]
    public void WinnerIsAlone()
    {
        _game.AddPlayer(_player1, true);
        _game.AddPlayer(_player2, false);
        _game.AddPlayer(_player3, false);
        _game.StartGame();
        _game.MakeFirstBlindBet(_player2, 50);
        _game.MakeFirstBlindBet(_player3, 100);
        Assert.Equal(25000 - 100,_player3.CasinoAccount.Balance);

        _game.Flop();
        _game.Raise(_player1, 1000);
        _game.Raise(_player2, 1100);
        _game.Call(_player3);
        _game.Fold(_player1);
        _game.Fold(_player2);
        Assert.Equal(2150, _game.Bank);
        Assert.Equal(PokerStatus.WinningPayment, _game.GameStatus);
        _game.Payment();
        Assert.Equal(25000 - 100 + 2150 + 100,_player3.CasinoAccount.Balance);
    }
    [Fact]
    public void WinningByComparing()
    {
        _game.AddPlayer(_player1, true);
        _game.AddPlayer(_player2, false);
        _game.AddPlayer(_player3, false);
        _game.StartGame();
        _game.MakeFirstBlindBet(_player2, 50);
        _game.MakeFirstBlindBet(_player3, 100);
        _game.Flop();
        _game.Raise(_player1, 1000);
        _game.Raise(_player2, 1100);
        _game.Call(_player3);
        _game.Call(_player1);
        Assert.Equal(PokerStatus.Turn, _game.GameStatus);
        _game.Turn();
        _game.Raise(_player2, 100);
        _game.Call(_player3);
        _game.Call(_player1);
        Assert.Equal(PokerStatus.River, _game.GameStatus);
        _game.River();
        _game.Raise(_player2, 100);
        _game.Call(_player3);
        _game.Call(_player1);
        Assert.Equal(PokerStatus.Showdown, _game.GameStatus);
        _game.Showdown();
        Assert.Equal(PokerStatus.WinningPayment, _game.GameStatus);
        Assert.Equal(_player3, _game.Winner);
        _game.Payment();
        Assert.Equal(25000 + _game.BetCatalog[_player1] + _game.BetCatalog[_player2],_player3.CasinoAccount.Balance);
    }

}