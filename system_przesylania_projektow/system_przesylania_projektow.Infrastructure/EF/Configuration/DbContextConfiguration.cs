using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Core.Enums;

namespace system_przesylania_projektow.Infrastructure.EF.Configuration;

public class DbContextConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<Role>, IEntityTypeConfiguration<Project> {

    public void Configure(EntityTypeBuilder<User> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }

    public void Configure(EntityTypeBuilder<Role> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .HasData(GetRoles());
    }

    public void Configure(EntityTypeBuilder<Project> builder) {
        builder
            .HasKey(x => x.Id);
    }

    private IEnumerable<Role> GetRoles() {
        var roles = new List<Role> { 
            new() {
                Id = (int)Roles.Student,
                Name = "Student"
            },
            new() {
                Id = (int)Roles.Teacher,
                Name = "Teacher"
            }
        };

        return roles;
    }
}
