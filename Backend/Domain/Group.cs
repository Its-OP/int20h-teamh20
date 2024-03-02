namespace domain;

public class Group : Entity<int>
{
    public Group() {}
    
    public Group(string code, IEnumerable<Subject> subjects)
    {
        Code = code;
        Subjects = subjects;
    }

    public string Code { get; set; }
    public virtual IEnumerable<Student> Students { get; set; } = new List<Student>();
    public virtual IEnumerable<Subject> Subjects { get; set; } = new List<Subject>();
}