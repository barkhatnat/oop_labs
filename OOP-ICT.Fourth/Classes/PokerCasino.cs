using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;
using OOP_ICT.Second.Interfaces;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.Classes;

public class PokerCasino
{
    private const decimal MaxMoneyAmount = 999_999_999_999_999_999;
    private readonly Player _adminPlayer;

    public PokerCasino()
    {
        var defaultCasinoRepository = new AccountRepository();
        var defaultBankCreator = new DebitAccountCreator("Default Bank Name", defaultCasinoRepository);
        var defaultCasinoCreator = new CasinoAccountAccountFactory("Default Casino Name", defaultCasinoRepository);
        var admin = new Player("0000000000", "Admin", "Admin");
        admin.BankAccount = defaultBankCreator.Create(admin, MaxMoneyAmount);
        admin.CasinoAccount = defaultCasinoCreator.Create(admin);
        admin.CasinoAccount.TransferMoneyAdd(MaxMoneyAmount);
        _adminPlayer = admin;
    }

    public void Win(IPlayer player, decimal amount)
    {
        if (!player.HasCasinoAccount())
        {
            throw new AccountException("Player has no casino account");
        }

        player.CasinoAccount.GetCasinoMoneyFromPlayer(amount, _adminPlayer);
    }

    public void Loss(IPlayer player, decimal amount)
    {
        if (!player.HasCasinoAccount())
        {
            throw new AccountException("Player has no casino account");
        }

        player.CasinoAccount.GiveCasinoMoneyToPlayer(amount, _adminPlayer);
    }

    public bool HasMoneyAmount(IPlayer player, decimal amount)
    {
        if (!player.HasCasinoAccount())
        {
            throw new AccountException("Player has no casino account");
        }

        return player.CasinoAccount.Balance >= amount;
    }
}