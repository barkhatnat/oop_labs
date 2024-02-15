namespace OOP_ICT.Second.Models;

public class AccountRepository
{
    private readonly List<Account> _accounts = new List<Account>();

    public List<Account> Accounts => _accounts;

    public void AddNewAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account FindAccountByPassport(string passport)
    {
        if (HasAccount(passport))
        {
            return _accounts.First(account => account.Player.CheckPassport(passport));
        }
        else
        {
            return null;
        }
    }

    public bool HasAccount(string passport)
    {
        return _accounts.Any(account => account.Player.CheckPassport(passport));
    }
}