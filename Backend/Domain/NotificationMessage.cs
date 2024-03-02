namespace domain;

public class NotificationMessage : Entity<int>
{
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
    public bool IsRead { get; set; } = false;
    public virtual Student Receiver { get; set; }
}