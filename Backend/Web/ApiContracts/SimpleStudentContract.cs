using System.Diagnostics;
using domain;

namespace backend.ApiContracts;

/*
PIB:
    properties:
        lastname:
            type: string
        firstname:
            type: string
        patronymic:
            type: string
*/

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

/*
StudentSimple:
    properties:
        allOf: PIB
        averageScore:
            type: number
        presences:
            type: integer
        absences:
            type: string
*/

public class SimpleStudentContract
{
    public SimpleStudentContract(Student student)
    {
        AllOf = new PIBContract
        {
            Lastname = student.LastName,
            Firstname = student.FirstName,
            Patronymic = student.Patronymic,
        };

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

    public PIBContract AllOf { get; set; }
    public decimal AverageScore { get; set; } = 0;
    public int Presences { get; set; } = 0;
    public int Absences { get; set; } = 0;
}