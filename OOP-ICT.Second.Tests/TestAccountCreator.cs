using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestAccountCreator
{
    private readonly AccountRepository _repository = new AccountRepository();
    private readonly BankAccountFactory _debitCreator;
    private readonly BankAccountFactory _creditCreator;
    private readonly CasinoAccountFactory _casinoAccountFactory;

    public TestAccountCreator()
    {
        _debitCreator = new DebitAccountCreator("DefaultDebitAccountCreatorName", _repository);
        _casinoAccountFactory = new CasinoAccountAccountFactory("DefaultCasinoAccountCreatorName", _repository);
        _creditCreator = new CreditAccountCreator("DefaultCreditAccountCreatorName", _repository);
    }

    private readonly Player _player1 = new Player("1234567890", "Player1Name", "Player1Surname");
    private readonly Player _player2 = new Player("1111111111", "Player2Name", "Player2Surname");
    private readonly Player _player3 = new Player("7777777777", "Player3Name", "Player3Surname");

    [Fact]
    public void AccountCreatorsAreNotNull()
    {
        Assert.NotNull(_debitCreator);
        Assert.NotNull(_creditCreator);
        Assert.NotNull(_casinoAccountFactory);
    }

    [Fact]
    public void DebitAndCreditAccountsAreNotNull()
    {
        var accountPlayer1 = _debitCreator.Create(_player1, 500);
        var accountPlayer2 = _creditCreator.Create(_player2, 500);
        Assert.NotNull(accountPlayer1);
        Assert.NotNull(accountPlayer2);
    }

    [Theory]
    [InlineData(-500)]
    [InlineData(-1)]
    public void CheckDebitAccountBalance_ExceptionIfNegative(decimal balance)
    {
        var exception = Assert.Throws<NegativeBalanceException>(() => _debitCreator.Create(_player1, balance));
        Assert.Equal("Balance of debit bank account can't be negative", exception.Message);
    }

    [Theory]
    [InlineData(-100_000_000)]
    [InlineData(-100_001)]
    public void CheckCreditAccountBalance_ExceptionIfLessThanMaxCredit(decimal balance)
    {
        var exception = Assert.Throws<CreditBalanceException>(() => _creditCreator.Create(_player1, balance));
        Assert.Equal("Balance of credit bank account can't be less than -100_000", exception.Message);
    }

    [Theory]
    [InlineData(500)]
    [InlineData(743921)]
    public void CheckDebitAccountBalance_NotNegative(decimal balance)
    {
        var exception = Record.Exception(() => _debitCreator.Create(_player1, balance));
        Assert.Null(exception);
    }

    [Fact]
    public void RepositoryAddBankAccounts()
    {
        var accountPlayer1 = _debitCreator.Create(_player1, 500);
        Assert.Contains(accountPlayer1, _repository.Accounts);
        var accountPlayer2 = _creditCreator.Create(_player2, 1000);
        Assert.Contains(accountPlayer1, _repository.Accounts);
        Assert.Equal(2, _repository.Accounts.Count);
        var accountPlayer3 = _debitCreator.Create(_player3, 500);
        Assert.Contains(accountPlayer3, _repository.Accounts);
        Assert.Equal(3, _repository.Accounts.Count);
    }

    [Theory]
    [InlineData(-700)]
    [InlineData(-100_000)]
    public void CheckCreditAccountBalance_MoreThanMaxCredit(decimal balance)
    {
        var exception = Record.Exception(() => _creditCreator.Create(_player1, balance));
        Assert.Null(exception);
    }

    [Fact]
    public void CheckCreatingCasinoAccount_ExceptionIfNoBankAccount()
    {
        var exception = Assert.Throws<AccountException>(() => _casinoAccountFactory.Create(_player3));
        Assert.Equal("Player has no bank account. Casino bank account can't be created without bank account",
            exception.Message);
    }

    [Fact]
    public void CheckCreatingCasinoAccount_HasBankAccount()
    {
        var accountPlayer3 = _debitCreator.Create(_player3, 500);
        var exception = Record.Exception(() => _casinoAccountFactory.Create(_player3));
        Assert.Null(exception);
    }

    [Fact]
    public void CheckDebitAccountNumber_ExceptionIfMoreThanOne()
    {
        var accountPlayer1 = _debitCreator.Create(_player1, 500);
        var exception = Assert.Throws<AccountException>(() => _debitCreator.Create(_player1, 3000));
        Assert.Equal("Player can't have more than 1 debit account", exception.Message);
    }

    [Fact]
    public void CheckCreditAccountNumber_ExceptionIfMoreThanOne()
    {
        var accountPlayer1 = _creditCreator.Create(_player1, 500);
        var exception = Assert.Throws<AccountException>(() => _creditCreator.Create(_player1, 3000));
        Assert.Equal("Player can't have more than 1 credit account", exception.Message);
    }

    [Fact]
    public void CheckCasinoAccountNumber_ExceptionIfMoreThanOne()
    {
        var accountPlayer3 = _creditCreator.Create(_player3, 500);
        var casinoAccountPlayer3 = _casinoAccountFactory.Create(_player3);
        var exception = Assert.Throws<AccountException>(() => _casinoAccountFactory.Create(_player3));
        Assert.Equal("Player can't have more than 1 casino account", exception.Message);
    }
}