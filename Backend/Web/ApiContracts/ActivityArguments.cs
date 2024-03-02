using domain;

namespace backend.ApiContracts;

public class ActivityArguments
{
    public bool IsAbsent { get; set; }
    public int? Score { get; set; }
    public int StudentId { get; set; }
}

public class ActivityContract : ActivityArguments
{
    public ActivityContract(Activity activity)
    {
        IsAbsent = !activity.StudentWasPresent;
        Score = activity.Score;
        ConductedAt = activity.ConductedAt.ToString("s");
        StudentId = activity.Student.Id;
        TypeId = activity.ActivityType.Id;
    }
    
    /// <summary>
    /// DateTime in ISO8601
    /// </summary>
    public string ConductedAt { get; set; }
    public int TypeId { get; set; }
}
