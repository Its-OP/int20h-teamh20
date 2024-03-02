namespace domain;

public class Subject : Entity<int>
{
    public Subject() { }

    public Subject(string title, bool hasExam)
    {
        Title = title;
        HasExam = hasExam;
    }

    public string Title { get; set; }
    public virtual IEnumerable<Activity> Activities { get; set; } = new List<Activity>();
    public bool HasExam { get; set; }
}