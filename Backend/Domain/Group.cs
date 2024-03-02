namespace domain;

public class Group
{
    public Group() {}
    
    public Group(int id, string code)
    {
        Id = id;
        Code = code;
    }

    public virtual int Id { get; set; }
    public virtual string Code { get; set; }
}