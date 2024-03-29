﻿using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Infrastructure.EF.Configuration;
using system_przesylania_projektow.Core.Entities;


namespace system_przesylania_projektow.Infrastructure.EF.Contexts;

public class AppDbContext : DbContext {

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<ProjectStudent> Students { get; set; }
    public DbSet<ProjectSolution> Solutions { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        var configuration = new DbContextConfiguration();

        builder.ApplyConfiguration<User>(configuration);
        builder.ApplyConfiguration<Role>(configuration);
        builder.ApplyConfiguration<Project>(configuration);
        builder.ApplyConfiguration<ProjectTask>(configuration);
        builder.ApplyConfiguration<ProjectStudent>(configuration);
        builder.ApplyConfiguration<ProjectSolution>(configuration);
    }
}
