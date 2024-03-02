namespace domain;

public class Subject : Entity<int>
{
    public Subject() {}
    
    public Subject(string title)
    {
        Title = title;
    }

    public virtual string Title { get; set; }
    public virtual IEnumerable<Activity> Activities { get; set; }
    public virtual bool HasExam { get; set; }
}