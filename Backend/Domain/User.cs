namespace domain;

public class User : Entity<int>
{
    public User() { }
    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public virtual IEnumerable<MessageTemplate> MessageTemplates { get; set; }
}