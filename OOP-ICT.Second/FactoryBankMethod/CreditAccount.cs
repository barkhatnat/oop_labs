using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryBankMethod;

public class CreditAccount : Account
{
    private const decimal MaxCredit = -100_000;

    public CreditAccount(Player player, AccountRepository mainCasinoRepository, decimal balance) : base(player,
        mainCasinoRepository, balance)
    {
        if (balance < MaxCredit)
        {
            throw new CreditBalanceException("Balance of credit bank account can't be less than -100_000");
        }

        Balance = balance;
        Player = player;
        ConnectedRepository = mainCasinoRepository;
        Player.BankAccount = this;
    }

    protected override bool HasMoneyAmount(decimal amount)
    {
        return Balance >= MaxCredit + amount;
    }
}