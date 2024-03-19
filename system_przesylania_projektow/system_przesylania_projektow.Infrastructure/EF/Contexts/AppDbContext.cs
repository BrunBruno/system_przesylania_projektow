using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Infrastructure.EF.Configuration;

namespace system_przesylania_projektow.Infrastructure.EF.Contexts;

class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var configuration = new DbContextConfiguration();

        base.OnModelCreating(builder);
    }
}
