using System.Net.Mail;

namespace domain;

public class EmailMessage
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
public class EmailSender
{
    private readonly SmtpClient _server;

    public EmailSender(string host, int port, string login, string password)
    {
        _server = new(host)
        {
            Port = port,
            EnableSsl = true,
            Credentials = new System.Net.NetworkCredential(login, password),
        };
    }

    public void SendEmail(EmailMessage msg)
    {
        MailMessage mail = new()
        {
            From = new MailAddress(msg.Sender),
            Subject = msg.Title,
            Body = msg.Body,
            BodyEncoding = System.Text.Encoding.UTF8,
            SubjectEncoding = System.Text.Encoding.UTF8
        };
        mail.To.Add(msg.Receiver);
        _server.Send(mail);
    }
}