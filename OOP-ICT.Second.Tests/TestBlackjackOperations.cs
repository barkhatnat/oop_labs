using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestBlackjackOperations
{
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;
    private readonly BlackjackCasino _blackjackCasino;


    public TestBlackjackOperations()
    {
        _blackjackCasino = new BlackjackCasino();
        _debitCreator = new DebitAccountCreator("DefaultDebitAccountCreatorName", _repository);
        _casinoAccountFactory = new CasinoAccountAccountFactory("DefaultCasinoAccountCreatorName", _repository);
        _creditCreator = new CreditAccountCreator("DefaultCreditAccountCreatorName", _repository);
    }

    private readonly Player _player = new Player("1234567890", "Player1Name", "Player1Surname");


    private bool _hasBlackjack;

    [Theory]
    [InlineData(10, 100, 110)]
    [InlineData(0, 8000, 8000)]
    public void AddMoneyIfWinDebit(decimal startDebitBalance, decimal winAmount, decimal endDebitBalance)
    {
        _hasBlackjack = true;
        var debitAccount = _debitCreator.Create(_player, startDebitBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        casinoAccount.TransferMoneyAdd(startDebitBalance);
        _blackjackCasino.CheckBlackjackAndEndGame(_player, _hasBlackjack, winAmount);
        Assert.Equal(startDebitBalance + winAmount, casinoAccount.Balance);
        Assert.Equal(endDebitBalance, casinoAccount.Balance);
    }

    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(8000, 7000, 1000)]
    public void SubtractMoneyIfLossDebit(decimal startDebitBalance, decimal lossAmount, decimal endDebitBalance)
    {
        _hasBlackjack = false;
        var debitAccount = _debitCreator.Create(_player, startDebitBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        casinoAccount.TransferMoneyAdd(startDebitBalance);
        _blackjackCasino.CheckBlackjackAndEndGame(_player, _hasBlackjack, lossAmount);
        Assert.Equal(startDebitBalance - lossAmount, casinoAccount.Balance);
        Assert.Equal(endDebitBalance, casinoAccount.Balance);
    }

    [Theory]
    [InlineData(10, 100, 110)]
    [InlineData(0, 8000, 8000)]
    public void AddMoneyIfWinCredit(decimal startCreditBalance, decimal winAmount, decimal endCreditBalance)
    {
        _hasBlackjack = true;
        var creditAccount = _creditCreator.Create(_player, startCreditBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        casinoAccount.TransferMoneyAdd(startCreditBalance);
        _blackjackCasino.CheckBlackjackAndEndGame(_player, _hasBlackjack, winAmount);
        Assert.Equal(startCreditBalance + winAmount, casinoAccount.Balance);
        Assert.Equal(endCreditBalance, casinoAccount.Balance);
    }

    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(8000, 7000, 1000)]
    public void SubtractMoneyIfLossCredit(decimal startCreditBalance, decimal lossAmount, decimal endCreditBalance)
    {
        _hasBlackjack = false;
        var creditAccount = _creditCreator.Create(_player, startCreditBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        casinoAccount.TransferMoneyAdd(startCreditBalance);
        _blackjackCasino.CheckBlackjackAndEndGame(_player, _hasBlackjack, lossAmount);
        Assert.Equal(startCreditBalance - lossAmount, casinoAccount.Balance);
        Assert.Equal(endCreditBalance, casinoAccount.Balance);
    }
}