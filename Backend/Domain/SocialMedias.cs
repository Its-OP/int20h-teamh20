using System.Text.Json.Serialization;

namespace domain;

public class SocialMedias
{
    [JsonIgnore]
    public Guid Id = Guid.NewGuid();
    [JsonIgnore]
    public int StudentId { get; set; }
    public string Telegram { get; set; }
    public string GitHub { get; set; }
    public string LinkedIn { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
}