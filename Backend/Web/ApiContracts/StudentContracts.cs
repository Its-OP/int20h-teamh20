using domain;

namespace backend.ApiContracts;
public class PIBContract
{
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Patronymic { get; set; }
}

public class StudentArguments : PIBContract
{
    public int GroupId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class SimpleStudentContract : PIBContract
{
    public SimpleStudentContract(Student student)
    {
        Id = student.Id;
        Lastname = student.LastName;
        Firstname = student.FirstName;
        Patronymic = student.Patronymic;

        var nActivities = student.Activities.Count();

        foreach (var activity in student.Activities)
        {
            if (activity.StudentWasPresent)
            {
                ++Presences;

                if (activity.Score is not null)
                    AverageScore += (decimal)activity.Score / nActivities;
            }
            else
            {
                ++Absences;
            }
        }
    }

    public int Id { get; set; }
    public decimal AverageScore { get; set; }
    public int Presences { get; set; }
    public int Absences { get; set; }
}

public class StudentContract : StudentArguments
{
    public StudentContract(Student student)
    {
        Firstname = student.FirstName;
        Lastname = student.LastName;
        Patronymic = student.Patronymic;
        Email = student.Email;
        PhoneNumber = student.PhoneNumber;
        GroupId = student.Group.Id;
        GroupCode = student.Group.Code;
        Activities = student.Activities.Select(x => new ActivityContract(x)).ToList();
        SocialMedias = student.SocialMedias;
    }
    
    public string GroupCode { get; set; }
    public List<ActivityContract> Activities { get; set; }
    public SocialMedias SocialMedias { get; set; }
}
