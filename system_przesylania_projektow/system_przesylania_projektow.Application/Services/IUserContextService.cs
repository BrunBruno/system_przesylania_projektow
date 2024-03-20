using System.Security.Claims;

namespace system_przesylania_projektow.Application.Services;

public interface IUserContextService {
    Guid? GetUserId();
    ClaimsPrincipal User { get; }
}
