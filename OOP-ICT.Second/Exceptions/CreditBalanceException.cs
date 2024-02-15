namespace OOP_ICT.Second.Exceptions;

public class CreditBalanceException : Exception
{
    public CreditBalanceException(string message)
        : base(message)
    {
    }
}