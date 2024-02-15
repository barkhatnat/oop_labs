using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryCasinoMethod;

public class CasinoAccount : Account
{
    public CasinoAccount(Player player, AccountRepository mainBankRepository) : base(player, mainBankRepository)
    {
        Balance = 0;
        Player = player;
        ConnectedRepository = mainBankRepository;
        Player.CasinoAccount = this;
    }

    protected override bool HasMoneyAmount(decimal amount)
    {
        return Balance >= amount;
    }
}