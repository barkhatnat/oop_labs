using System.Diagnostics;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.Models;

public abstract class Account
{
    public decimal Balance { get; protected set; }
    public Player Player { get; protected set; }

    public AccountRepository ConnectedRepository { get; set; }

    protected Account(Player player, AccountRepository repository)
    {
        Balance = 0;
        Player = player;
        ConnectedRepository = repository;
    }

    protected Account(Player player, AccountRepository repository, decimal balance)
    {
        Balance = balance;
        Player = player;
        ConnectedRepository = repository;
    }

    public void TransferMoneyAdd(decimal amount)
    {
        AddMoney(amount);
        ConnectedRepository.FindAccountByPassport(Player.Passport).SubtractMoney(amount);
    }

    public void TransferMoneySubtract(decimal amount)
    {
        SubtractMoney(amount);
        ConnectedRepository.FindAccountByPassport(Player.Passport).AddMoney(amount);
    }

    protected abstract bool HasMoneyAmount(decimal amount);

    private void AddMoney(decimal amount)
    {
        Balance += amount;
    }

    private void SubtractMoney(decimal amount)
    {
        if (!HasMoneyAmount(amount))
        {
            throw new NoMoneyException("Not enough money for this operation");
        }

        Balance -= amount;
    }

    public void GetCasinoMoneyFromPlayer(decimal amount, Player player)
    {
        AddMoney(amount);
        player.CasinoAccount.SubtractMoney(amount);
    }

    public void GiveCasinoMoneyToPlayer(decimal amount, Player player)
    {
        SubtractMoney(amount);
        player.CasinoAccount.AddMoney(amount);
    }
}