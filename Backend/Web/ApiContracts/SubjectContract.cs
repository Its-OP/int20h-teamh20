namespace backend.ApiContracts;

public class SubjectContract
{
    public string Title { get; set; }
}

public class SubjectArguments : SubjectContract
{
    public bool IsExam { get; set; }
}