namespace system_przesylania_projektow.Shared.Exeptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
