namespace domain;

public class User : Entity<int>
{
    public User() {}
    public User(string firstName, string lastName, string patronymic, string phoneNumber, string email, string passwordHash, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}