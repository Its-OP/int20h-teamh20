namespace domain;

public abstract class Entity<T>
{
    public Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    public virtual T Id { get; set; }
    public virtual DateTime CreatedAt { get; set; }
}