using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace domain;

public class SocialMedias
{
    [JsonIgnore]
    public Guid Id = Guid.NewGuid();
    [JsonIgnore]
    public int StudentId { get; set; }
    
    [MaxLength(256)]
    public string Telegram { get; set; } = string.Empty;
    [MaxLength(256)]
    public string GitHub { get; set; } = string.Empty;
    [MaxLength(256)]
    public string LinkedIn { get; set; } = string.Empty;
    [MaxLength(256)]
    public string Facebook { get; set; } = string.Empty;
    [MaxLength(256)]
    public string Twitter { get; set; } = string.Empty;
}