using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Core.Enums;

namespace system_przesylania_projektow.Infrastructure.EF.Configuration;

public class DbContextConfiguration : IEntityTypeConfiguration<User>,
                                      IEntityTypeConfiguration<Role>,
                                      IEntityTypeConfiguration<Project>,
                                      IEntityTypeConfiguration<ProjectStudent>,
                                      IEntityTypeConfiguration<ProjectTask>,
                                      IEntityTypeConfiguration<ProjectSolution> 
{  

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
        builder
            .HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
    }

    public void Configure(EntityTypeBuilder<ProjectStudent> builder) {
        builder
          .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.ProjectId);
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }

    public void Configure(EntityTypeBuilder<ProjectTask> builder) {
        builder
           .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId);

    }

    public void Configure(EntityTypeBuilder<ProjectSolution> builder) {
        builder
           .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.Solutions)
            .HasForeignKey(x => x.StudentId);
        builder
            .HasOne(x => x.Task)
            .WithMany(x => x.Solutions)
            .HasForeignKey(x => x.TaskId);

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
