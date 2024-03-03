namespace domain;

public class Professor : Entity<int>
{
    public Professor() {}
    
    public Professor(User user)
    {
        User = user;
    }

    public virtual User User { get; set; }
    public int UserId { get; set; }
    public virtual IEnumerable<MessageTemplate> MessageTemplates { get; set; } = new List<MessageTemplate>();
}