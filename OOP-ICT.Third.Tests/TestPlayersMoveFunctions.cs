using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Enums;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestPlayersMoveFunctions
{
    private readonly BlackjackDealer _dealer = new BlackjackDealer();
    private readonly BlackjackGame _game;
    private readonly BlackjackCasino _blackjackCasino = new BlackjackCasino();
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestPlayersMoveFunctions()
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
    public void CheckPlayersMoveAndTurn()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        Assert.Equal(0,_game.PlayersTurn);
        _game.PlayerMove(_player1, true);
        Assert.Equal(3, _player1.PlayersHand.Cards.Count);
        Assert.Equal(1,_game.PlayersTurn);
        _game.PlayerMove(_player2, false);
        Assert.Equal(2, _player2.PlayersHand.Cards.Count);
        Assert.Equal(0,_game.PlayersTurn);
    }
    [Fact]
    public void CheckDealersMove()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        _game.DealCards();
        Assert.Single(_dealer.DealersHand.Cards);
        _game.PlayerMove(_player1, true);
        _game.PlayerMove(_player2, false);
        Assert.Equal(2, _dealer.DealersHand.Cards.Count);
    }
    [Fact]
    public void CheckGameStatuses()
    {
        _game.AddPlayer(_player1, 1000);
        _game.AddPlayer(_player2, 500);
        Assert.Equal(_game.GameStatus, BlackjackStatus.Betting);
        _game.DealCards();
        Assert.Equal(_game.GameStatus, BlackjackStatus.PlayersTurn);
        _game.PlayerMove(_player1, true); ;
        _game.PlayerMove(_player2, false);
        Assert.Equal(_game.GameStatus, BlackjackStatus.DealerTurn);
    }
}