using domain;

namespace backend.ApiContracts;

public class ActivityContract
{
    public string SubjectTitle { get; set; }
    public ActivityType Type { get; set; }
    public bool IsAbsent { get; set; }
    public int? Score { get; set; }
    
    /// <summary>
    /// DateTime in ISO8601
    /// </summary>
    public string ConductedAt { get; set; }
    public int StudentId { get; set; }
}
