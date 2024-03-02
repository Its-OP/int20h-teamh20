namespace backend.ApiContracts;


public class GroupContract
{
    public string Code { get; set; }
}

public class GroupArguments : GroupContract
{
    public IEnumerable<SubjectContract> Subjects { get; set; } = new List<SubjectContract>();
}
