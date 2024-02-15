namespace OOP_ICT.Second.Exceptions;

public class NegativeBalanceException : Exception
{
    public NegativeBalanceException(string message)
        : base(message)
    {
    }
}