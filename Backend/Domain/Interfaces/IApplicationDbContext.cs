using Microsoft.EntityFrameworkCore;

namespace domain.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Group> Groups { get; }
    public DbSet<SimpleStudent> SimpleStudents { get; }

    public Task<int> SaveChangesAsync(CancellationToken token);
}