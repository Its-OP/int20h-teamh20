namespace backend.ApiContracts;

public class GroupContract
{
    public string Code { get; set; }
}

public class GroupArguments : GroupContract
{
    public IEnumerable<int> SubjectIds { get; set; } = new List<int>();
}
