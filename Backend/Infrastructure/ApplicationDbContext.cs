﻿using domain;
using domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<SimpleStudent> SimpleStudents { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Username).IsUnique();
            entity.HasIndex(x => new { x.Username, x.PasswordHash });
        });
        
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Groups");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Code).IsUnique();
        });
        
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activities");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex($"{nameof(Activity.ConductedAt)}", $"{nameof(Subject)}Id").IsUnique();
        });
        
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subjects");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Title).IsUnique();
        });
    }
}