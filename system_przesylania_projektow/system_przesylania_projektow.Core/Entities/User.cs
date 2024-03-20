﻿using system_przesylania_projektow.Core.Enums;

namespace system_przesylania_projektow.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public int RoleId { get; set; } = (int)Roles.Student;
    public Role Role { get; set; }
}
