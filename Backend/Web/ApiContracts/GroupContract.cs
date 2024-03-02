namespace backend.ApiContracts;

public class GroupContract
{
    public GroupContract() { }
    public GroupContract(domain.Group g)
    {
        Id = g.Id;
        Code = g.Code;
    }

    public int Id { get; set; }
    public string Code { get; set; }
}

public class GroupArguments : GroupContract
{
    public IEnumerable<int> SubjectIds { get; set; } = new List<int>();
}
