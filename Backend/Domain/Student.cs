namespace domain;

public class Student : Entity<int>
{
    public virtual IEnumerable<Activity> Activities { get; set; } = new List<Activity>();
    public virtual IEnumerable<NotificationMessage> Messages { get; set; } = [];
    public virtual Group Group { get; set; }
    public virtual SocialMedias SocialMedias { get; set; } = new();
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public void UpdateSocialMedia(SocialMedias medias)
    {
        SocialMedias.Facebook = medias.Facebook;
        SocialMedias.Telegram = medias.Telegram;
        SocialMedias.GitHub = medias.GitHub;
        SocialMedias.LinkedIn = medias.LinkedIn;
        SocialMedias.Twitter = medias.Twitter;
    }
}
