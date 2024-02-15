using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Enums;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestWinsAndLosses
{
    private readonly BlackjackDealer _dealer = new BlackjackDealer();
    private readonly BlackjackGame _game;
    private readonly BlackjackCasino _blackjackCasino = new BlackjackCasino();
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestWinsAndLosses()
    {
        _game = new BlackjackGame(_dealer,_blackjackCasino);
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
    public void CheckDraw()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        _game.PlayerMove(_player1, true);
        _game.PlayerMove(_player2, false);
        _game.PlayerMove(_player1, true);
        _game.PlayerMove(_player2, false);
        _game.PlayerMove(_player1, true);
        _game.PlayerMove(_player2, false);
        Assert.Equal(21, _game.Players[0].PlayersHand.BlackjackCounter());
        Assert.Equal(Result.Draw, _game.ResultCatalog[_player1]);
        Assert.Equal(Result.Draw, _game.DealerResult[_player1]);
        Assert.Equal(Result.Loss, _game.ResultCatalog[_player2]);
        Assert.Equal(Result.Win, _game.DealerResult[_player2]);
        _game.BetPayment();
        Assert.Equal(1000, _game.PaymentCatalog[_player1]);
        Assert.Equal(1000, _player1.CasinoAccount.Balance);
        Assert.Equal(0, _game.PaymentCatalog[_player2]);
    }

    [Fact]
    public void CheckMoreThanBlackjack()
    {
        _game.AddPlayer(_player1, 900);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        _game.PlayerMove(_player1, true);
        _game.PlayerMove(_player2, true);
        Assert.Equal(26, _game.Players[1].PlayersHand.BlackjackCounter());
        Assert.Equal(Result.Loss, _game.ResultCatalog[_player2]);
        _game.PlayerMove(_player1, true);
        Assert.Equal(27, _game.Players[0].PlayersHand.BlackjackCounter());
        Assert.Equal(Result.Loss, _game.ResultCatalog[_player1]);
        _game.BetPayment();
        Assert.Equal(0, _game.PaymentCatalog[_player1]);
        Assert.Equal(100, _player1.CasinoAccount.Balance);
        Assert.Equal(0, _game.PaymentCatalog[_player2]);
    }
}