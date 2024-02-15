namespace OOP_ICT.Second.Exceptions;

public class IncorrectPassportException : Exception
{
    public IncorrectPassportException(string message)
        : base(message)
    {
    }
}