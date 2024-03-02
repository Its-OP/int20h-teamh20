namespace backend.ApiContracts;

public class SubjectContract : SubjectArguments
{
    public int Id { get; set; }
}

public class SubjectArguments
{
    public string Title { get; set; }
    public bool IsExam { get; set; }
}