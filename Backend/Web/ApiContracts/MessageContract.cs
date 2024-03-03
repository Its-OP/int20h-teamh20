using domain;

namespace backend.ApiContracts;

public class MessageTemplateParams
{
    public string Title { get; set; }
    public string Text { get; set; }
}

public class MessageParams : MessageTemplateParams
{
    public IEnumerable<int> ReceiverIds { get; set; }
}

public class MessageBaseContract(MessageBase message)
{
    public int Id { get; set; } = message.Id;
    public string Title { get; set; } = message.Title;
    public string Text { get; set; } = message.Text;
    public int AuthorId { get; set; } = message.Owner.Id;
}

public class MessageTemplateContract(MessageTemplate message) : MessageBaseContract(message)
{ }

public class MessageContract(NotificationMessage message) : MessageBaseContract(message)
{
    /// <summary>
    /// DateTime in ISO8601
    /// </summary>
    public string SentAt { get; set; } = message.CreatedAt.ToString("s");
    public bool IsRead { get; set; } = message.IsRead;
}
