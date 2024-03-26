﻿

namespace system_przesylania_projektow.Infrastructure.EF.Options;

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; }
}
