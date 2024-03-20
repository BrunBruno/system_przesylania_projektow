

using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Repositories;

public interface IUserRepository {

    Task<User?> GetUserByEmail(string email);
    Task AddUser(User user);
}
