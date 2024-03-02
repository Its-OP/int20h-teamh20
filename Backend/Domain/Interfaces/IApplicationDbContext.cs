using Microsoft.EntityFrameworkCore;

namespace domain.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Group> Groups { get; }
    public DbSet<Student> Students { get; }
    public DbSet<Subject> Subjects { get; }
    public DbSet<Activity> Activities { get; }
    DbSet<ActivityType> ActivityTypes { get; }
    public Task<int> SaveChangesAsync(CancellationToken token);
}