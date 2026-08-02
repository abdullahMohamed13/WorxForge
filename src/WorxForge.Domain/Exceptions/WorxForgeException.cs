namespace WorxForge.Domain.Exceptions;

public class WorxForgeException : Exception
{
    public WorxForgeException()
    {
    }

    public WorxForgeException(string? message) : base(message)
    {
    }
}
