using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Services;

public interface IJwtService
{
    string GetJwtToken(User user);
}
