namespace domain;

public class Student : Entity<int>
{
    public virtual IEnumerable<Activity> Activities { get; set; } = new List<Activity>();
    public virtual Group Group { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public virtual SocialMedias SocialMedias { get; set; } = new();

    public void UpdateSocialMedia(SocialMedias medias)
    {
        SocialMedias.Facebook = medias.Facebook;
        SocialMedias.Telegram = medias.Telegram;
        SocialMedias.GitHub = medias.GitHub;
        SocialMedias.LinkedIn = medias.LinkedIn;
        SocialMedias.Twitter = medias.Twitter;
    }
}
