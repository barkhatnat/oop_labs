namespace OOP_ICT.Fourth.Tests;

using OOP_ICT.Fourth.Classes;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using Xunit;

public class TestBlindsBetting
{
    private readonly PokerGame _game;
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly PokerCasino _pokerCasino = new PokerCasino();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestBlindsBetting()
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
    public void BlindsBets()
    {
        _game.AddPlayer(_player1, true);
        _game.AddPlayer(_player2, false);
        _game.AddPlayer(_player3, false);
        _game.StartGame();
        Assert.Equal(PokerStatus.BlindsBetting, _game.GameStatus);
        _game.MakeFirstBlindBet(_player2, 50);
        Assert.Equal(50, _game.BetCatalog[_player2]);
        Assert.Equal(PokerStatus.BlindsBetting, _game.GameStatus);
        _game.MakeFirstBlindBet(_player3, 100);
        Assert.Equal(100, _game.BetCatalog[_player3]);
        Assert.Equal(PokerStatus.Dealing, _game.GameStatus);
    }

    [Fact]
    public void BlindsBetExceptions()
    {
        _game.AddPlayer(_player1, true);
        _game.AddPlayer(_player2, false);
        _game.AddPlayer(_player3, false);
        _game.StartGame();
        var exception1 = Assert.Throws<BetException>(() => _game.MakeFirstBlindBet(_player2, 1000));
        Assert.Equal("Small blind's first bet must equals the half of the minimal bet amount", exception1.Message);
        _game.MakeFirstBlindBet(_player2, 50);
        var exception2 = Assert.Throws<BetException>(() => _game.MakeFirstBlindBet(_player3, 1000));
        Assert.Equal("Big blind's first bet must equals the minimal bet amount", exception2.Message);
        var exception3 = Assert.Throws<BetException>(() => _game.MakeFirstBlindBet(_player1, 1000));
        Assert.Equal("This player is not small or big blind", exception3.Message);
    }
}