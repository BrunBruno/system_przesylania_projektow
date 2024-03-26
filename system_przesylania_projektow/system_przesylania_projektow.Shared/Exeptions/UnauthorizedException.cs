namespace system_przesylania_projektow.Shared.Exeptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}
