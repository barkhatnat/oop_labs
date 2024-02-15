using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.FactoryBankMethod;
using OOP_ICT.Second.FactoryCasinoMethod;

namespace OOP_ICT.Second.Models;

public class BlackjackCasino
{
    private const decimal MaxMoneyAmount = 999_999_999_999_999_999;
    private readonly Player _adminPlayer;

    public BlackjackCasino()
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

    private void Win(Player player, decimal amount)
    {
        if (!player.HasCasinoAccount())
        {
            throw new AccountException("Player has no casino account");
        }

        player.CasinoAccount.GetCasinoMoneyFromPlayer(amount, _adminPlayer);
    }

    private void Loss(Player player, decimal amount)
    {
        if (!player.HasCasinoAccount())
        {
            throw new AccountException("Player has no casino account");
        }

        player.CasinoAccount.GiveCasinoMoneyToPlayer(amount, _adminPlayer);
    }

    public void CheckBlackjackAndEndGame(Player player, bool hasBlackjack, decimal amount)
    {
        if (hasBlackjack)
        {
            Win(player, amount);
        }
        else
        {
            Loss(player, amount);
        }
    }
}