namespace Kolokwium.Exceptions;

public class PlayerOverLoadedException : Exception
{
    public PlayerOverLoadedException(string message) : base(message)
    {
    }
}