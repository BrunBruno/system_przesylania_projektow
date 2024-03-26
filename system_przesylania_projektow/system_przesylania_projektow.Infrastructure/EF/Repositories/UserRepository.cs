using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Infrastructure.EF.Contexts;

namespace system_przesylania_projektow.Infrastructure.EF.Repositories;

public class UserRepository : IUserRepository {

    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByEmail(string email)
        => await _dbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetUserById(Guid id)
        => await _dbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddUser(User user) {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}
