namespace domain;

public class Group : Entity<int>
{
    public Group() {}
    
    public Group(int id, string code)
    {
        Id = id;
        Code = code;
    }

    public virtual string Code { get; set; }
    // TODO: reference Students
}