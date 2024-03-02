namespace backend.ApiContracts;

public class ActivityArguments
{
    public bool IsAbsent { get; set; }
    public int? Score { get; set; }
    public int StudentId { get; set; }
}

public class ActivityContract : ActivityArguments
{
    /// <summary>
    /// DateTime in ISO8601
    /// </summary>
    public string ConductedAt { get; set; }
    public int TypeId { get; set; }
}
