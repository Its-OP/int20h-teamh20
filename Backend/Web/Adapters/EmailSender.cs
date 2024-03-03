using System.Net.Mail;
using SendGrid;

namespace domain;

public class EmailMessage
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}