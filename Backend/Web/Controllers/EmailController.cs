
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace backend.Controllers;

public class EmailMessage
{
    public int ReceiverId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

[Route("api/email")]
public class EmailController(IApplicationDbContext context, IConfiguration configuration) : ControllerBase
{
    private readonly IApplicationDbContext _dbContext = context;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> SendEmail([FromBody] EmailMessage apiEmail)
    {
        var client = new SendGridClient(_configuration["EmailSettings:ApiKey"]);
        var from = new EmailAddress(_configuration["EmailSettings:SenderEmail"],
                                    _configuration["EmailSettings:SenderName"]);
        var subject = apiEmail.Title;

        var user = _dbContext.Users.SingleOrDefault(x => x.Id == apiEmail.ReceiverId);

        if (user is null)
            return BadRequest("Invalid receiver id.");

        var to = new EmailAddress(user.Email);
        var plainTextContent = apiEmail.Body;
        var htmlContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        return Ok(await client.SendEmailAsync(msg));
    }
}