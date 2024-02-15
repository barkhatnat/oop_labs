using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Exceptions;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestExceptions
{
    private readonly BlackjackDealer _dealer = new BlackjackDealer();
    private readonly BlackjackGame _game;
    private readonly BlackjackCasino _blackjackCasino = new BlackjackCasino();
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestExceptions()
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
    public void CheckExceptionsDealCards()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        var exception = Assert.Throws<GameStatusException>(() => _game.DealCards());
        Assert.Equal("Cannot deal cards at this game state", exception.Message);
    }

    [Fact]
    public void CheckExceptionsBetPayment()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        var exception = Assert.Throws<GameStatusException>(() => _game.BetPayment());
        Assert.Equal("Cannot give payment at this game state", exception.Message);
    }

    [Fact]
    public void CheckExceptionsTurn()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        var exception = Assert.Throws<GameStatusException>(() => _game.PlayerMove(_player1, true));
        Assert.Equal("Now is not player's turn", exception.Message);
    }

    [Fact]
    public void CheckExceptionsWrongTurn()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        _game.PlayerMove(_player1, true);
        var exception = Assert.Throws<GameStatusException>(() => _game.PlayerMove(_player1, true));
        Assert.Equal("Now is turn of player Player2Name, not turn of player Player1Name", exception.Message);
    }
}