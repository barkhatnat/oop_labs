namespace OOP_ICT.Third.Exceptions;

public class InvalidCardNumberException : Exception
{
    public InvalidCardNumberException(string message)
        : base(message)
    {
    }
}