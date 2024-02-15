using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestMoneyOperations
{
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestMoneyOperations()
    {
        _debitCreator = new DebitAccountCreator("DefaultDebitAccountCreatorName", _repository);
        _casinoAccountFactory = new CasinoAccountAccountFactory("DefaultCasinoAccountCreatorName", _repository);
        _creditCreator = new CreditAccountCreator("DefaultCreditAccountCreatorName", _repository);
    }

    private readonly Player _player = new Player("1234567890", "Player1Name", "Player1Surname");

    [Theory]
    [InlineData(400, 30, 370)]
    [InlineData(500, 100, 400)]
    [InlineData(7, 7, 0)]
    public void FromDebitToCasinoAndBack(decimal debitStartBalance, decimal amount, decimal debitEndBalance)
    {
        var debitAccount = _debitCreator.Create(_player, debitStartBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        casinoAccount.TransferMoneyAdd(amount);
        Assert.Equal(debitStartBalance - amount, debitAccount.Balance);
        Assert.Equal(debitEndBalance, debitAccount.Balance);
        Assert.Equal(amount, casinoAccount.Balance);
        casinoAccount.TransferMoneySubtract(amount);
        Assert.Equal(debitEndBalance + amount, debitAccount.Balance);
        Assert.Equal(debitStartBalance, debitAccount.Balance);
        Assert.Equal(0, casinoAccount.Balance);
    }

    [Theory]
    [InlineData(-400, 30, -430)]
    [InlineData(500, 600, -100)]
    [InlineData(7, 7, 0)]
    public void FromCreditToCasinoAndBack(decimal creditStartBalance, decimal amount, decimal creditEndBalance)
    {
        var creditAccount = _creditCreator.Create(_player, creditStartBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        casinoAccount.TransferMoneyAdd(amount);
        Assert.Equal(creditStartBalance - amount, creditAccount.Balance);
        Assert.Equal(creditEndBalance, creditAccount.Balance);
        Assert.Equal(amount, casinoAccount.Balance);
        casinoAccount.TransferMoneySubtract(amount);
        Assert.Equal(creditEndBalance + amount, creditAccount.Balance);
        Assert.Equal(creditStartBalance, creditAccount.Balance);
        Assert.Equal(0, casinoAccount.Balance);
    }
    [Theory]
    [InlineData(30, 31)]
    [InlineData(100, 500)]
    public void ShouldHavaEnoughMoney_ExceptionIfNotEnough(decimal debitStartBalance, decimal amount)
    {
        var debitAccount = _debitCreator.Create(_player, debitStartBalance);
        var casinoAccount = _casinoAccountFactory.Create(_player);
        var exception = Assert.Throws<NoMoneyException>(() => casinoAccount.TransferMoneyAdd(amount));
        Assert.Equal("Not enough money for this operation",
        exception.Message);
    }
}