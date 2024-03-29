﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using system_przesylania_projektow.Application.Services;

namespace system_przesylania_projektow.Infrastructure.Services;

public class UserContextService : IUserContextService {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public Guid? GetUserId() =>
        User is null ? null : (Guid?)Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
