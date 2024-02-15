using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryCasinoMethod;

public abstract class CasinoAccountFactory
{
    public string Name { get; set; }
    protected readonly AccountRepository MainCasinoRepository = new AccountRepository();
    protected readonly AccountRepository BankAccountRepository;

    protected CasinoAccountFactory(string n, AccountRepository bankAccountRepository)
    {
        Name = n;
        BankAccountRepository = bankAccountRepository;
    }

    public abstract Account Create(Player player);
}