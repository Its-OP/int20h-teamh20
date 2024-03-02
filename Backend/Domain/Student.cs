namespace domain;

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
using PIB = string;

public class Student(PIB allOf, float averageScore, int presences, int absences)
{
    public int Id { get; set; }
    public PIB AllOf { get; set; } = allOf;
    public float AverageScore { get; set; } = averageScore;
    public int Presences { get; set; } = presences;
    public int Absences { get; set; } = absences;
}