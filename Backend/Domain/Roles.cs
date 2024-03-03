namespace domain;

public static class Roles
{
    public const string Professor = nameof(Professor);
    public const string Student = nameof(Student);

    public const string AllRoles = $"{Professor},{Student}";
}