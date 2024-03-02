namespace domain;

public class Group : Entity<int>
{
    public Group() {}
    
    public Group(int id, string code)
    {
        Id = id;
        Code = code;
    }

    public string Code { get; set; }
    public virtual IEnumerable<Student> Students { get; set; } = new List<Student>();
}