
using System.Linq.Expressions;
using domain;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/email")]
public class EmailController : ControllerBase
{
    private readonly EmailSender _emailSender;

    public EmailController(IConfiguration configuration)
    {
        var server = configuration["SmtpCredentials:Server"];
        var port = configuration["SmtpCredentials:Port"];
        var login = configuration["SmtpCredentials:Login"];
        var password = configuration["SmtpCredentials:Password"];

        if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new Exception("Invalid SMTP Server configuratio.");

        _emailSender = new(server, int.Parse(port), login, password);
    }

    [HttpPost]
    [Route("")]
    public IActionResult SendEmail([FromBody] EmailMessage apiEmail)
    {
        // try
        // {
            _emailSender.SendEmail(apiEmail);
            return Ok(new { Result = true });
        // }
        // catch (Exception ex)
        // {
        //     return BadRequest(ex.Message);
        // }
    }
}