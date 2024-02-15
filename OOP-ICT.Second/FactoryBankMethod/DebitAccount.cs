using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryBankMethod;

public class DebitAccount : Account
{
    public DebitAccount(Player player, AccountRepository mainCasinoRepository, decimal balance) : base(player,
        mainCasinoRepository, balance)
    {
        if (balance < 0)
        {
            throw new NegativeBalanceException("Balance of debit bank account can't be negative");
        }

        Balance = balance;
        Player = player;
        ConnectedRepository = mainCasinoRepository;
        Player.BankAccount = this;
    }


    protected override bool HasMoneyAmount(decimal amount)
    {
        return Balance >= amount;
    }
}