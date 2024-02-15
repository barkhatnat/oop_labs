using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryCasinoMethod;

public class CasinoAccountAccountFactory : CasinoAccountFactory
{
    public CasinoAccountAccountFactory(string n, AccountRepository bankAccountRepository) : base(n, bankAccountRepository)
    {
    }

    public override Account Create(Player player)
    {
        if (!BankAccountRepository.HasAccount(player.Passport))
            throw new AccountException(
                "Player has no bank account. Casino bank account can't be created without bank account");
        if (MainCasinoRepository.HasAccount(player.Passport))
        {
            throw new AccountException("Player can't have more than 1 casino account");
        }

        var newBankAccount = new CasinoAccount(player, BankAccountRepository);
        MainCasinoRepository.AddNewAccount(newBankAccount);
        return newBankAccount;

    }
}