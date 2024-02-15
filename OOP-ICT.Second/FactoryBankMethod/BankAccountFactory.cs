using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.FactoryBankMethod;

public abstract class BankAccountFactory
{
    public string Name { get; private set; }
    protected readonly AccountRepository MainBankRepository;

    protected BankAccountFactory(string n, AccountRepository repository)
    {
        Name = n;
        MainBankRepository = repository;
    }

    public abstract Account Create(Player player, decimal balance);
}