﻿using domain;
using domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace infrastructure;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;
    
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<User> Users { get; set; }

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
    }
}