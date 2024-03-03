using domain;
using domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<NotificationMessage> Messages { get; set; }
    public DbSet<MessageTemplate> MessageTemplates { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }


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

            entity.HasIndex(x => x.Email).IsUnique();
            entity.HasIndex(x => x.PhoneNumber).IsUnique();
            entity.HasIndex(x => new { x.Email, x.PasswordHash });
        });
        
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Groups");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Code).IsUnique();
            entity.HasMany(x => x.Subjects).WithOne().IsRequired(false);
        });
        
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activities");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
        });
        
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subjects");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Title).IsUnique();
        });
        
        modelBuilder.Entity<SocialMedias>(entity =>
        {
            entity.HasKey(x => x.Id);
        });

        modelBuilder.Entity<NotificationMessage>(entity =>
        {
            entity.ToTable("Messages");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MessageTemplate>(entity => 
        {
            entity.ToTable("MessageTemplates");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Title).IsUnique();
        });
        
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasOne(x => x.User).WithOne().HasForeignKey<Student>(x => x.UserId);

            entity.HasOne(x => x.SocialMedias).WithOne().HasForeignKey<SocialMedias>(x => x.StudentId);
        });
        
        modelBuilder.Entity<Professor>(entity =>
        {
            entity.ToTable("Professors");
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasOne(x => x.User).WithOne().HasForeignKey<Professor>(x => x.UserId);
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.ToTable("ActivityTypes");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.Title).IsUnique();
        });
    }
}