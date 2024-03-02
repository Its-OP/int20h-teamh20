namespace domain;

public class Activity : Entity<int>
{
    public virtual Subject Subject { get; set; }
    public virtual DateTime ConductedAt { get; set; }
    public virtual int? Score { get; set; }
    public virtual bool StudentWasPresent { get; set; }
    // TODO: reference Student
}

public enum ActivityType
{
    Exam,
    Lab,
    Lecture,
    Practise
}
