namespace OOP_ICT.Third.Exceptions;

public class GameStatusException : Exception
{
    public GameStatusException(string message)
        : base(message)
    {
    }
}