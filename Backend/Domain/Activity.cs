using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace domain;

public class Activity : Entity<int>
{
    public virtual Subject Subject { get; set; }
    public virtual DateTime ConductedAt { get; set; }
    public int? Score { get; set; }
    public bool StudentWasPresent { get; set; }
    public virtual Student Student { get; set; }
    public virtual ActivityType ActivityType { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum ActivityType
{
    Exam,
    Lab,
    Lecture,
    Practise
}
