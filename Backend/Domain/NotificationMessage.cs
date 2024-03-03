namespace domain;

public class MessageBase : Entity<int>
{
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
    public virtual User Owner { get; set; }
}

public class MessageTemplate : MessageBase
{}

public class NotificationMessage : MessageBase
{
    public bool IsRead { get; set; } = false;
    public virtual Student Receiver { get; set; }
}