namespace domain;

public class Student : Entity<int>
{
    public virtual IEnumerable<Activity> Activities { get; set; } = new List<Activity>();
    public virtual Group Group { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
}