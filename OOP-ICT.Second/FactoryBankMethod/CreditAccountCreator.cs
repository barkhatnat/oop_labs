using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryBankMethod;

public class CreditAccountCreator : BankAccountFactory
{
    public CreditAccountCreator(string n, AccountRepository repository) : base(n, repository)
    {
    }

    public override Account Create(Player player, decimal balance)
    {
        if (MainBankRepository.HasAccount(player.Passport))
        {
            throw new AccountException("Player can't have more than 1 credit account");
        }

        var newAccount = new CreditAccount(player, MainBankRepository, balance);
        MainBankRepository.AddNewAccount(newAccount);
        return newAccount;
    }
}