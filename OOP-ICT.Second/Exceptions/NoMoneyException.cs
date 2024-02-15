namespace OOP_ICT.Second.Exceptions;

public class NoMoneyException : Exception
{
    public NoMoneyException(string message)
        : base(message)
    {
    }
}