namespace backend.ApiContracts;

public class MessageParams
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int TemplateId { get; set; }
    public bool IsTemplate { get; set; }
    public IEnumerable<int> ReceiverIds { get; set; }
}

public class MessageContract(domain.NotificationMessage message)
{
    public int Id { get; set; } = message.Id;
    public string Title { get; set; } = message.Title;
    public string Text { get; set; } = message.Text;
    public bool IsRead { get; set; } = message.IsRead;
    public string SentAt { get; set; } = message.CreatedAt.ToString("s");
}