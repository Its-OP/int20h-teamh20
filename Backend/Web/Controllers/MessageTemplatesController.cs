using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize(Roles = Roles.Professor)]
[Route("api/messages/templates")]
public class MessageTemplatesController(IApplicationDbContext context) : ControllerBase
{
    private readonly IApplicationDbContext _context = context;

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateTemplate([FromBody] MessageTemplateParams apiTemplate, CancellationToken token)
    {
        var templates = await GetTemplatesOfProfessor(token);

        if (templates.Any(x => x.Title == apiTemplate.Title))
            return BadRequest("Template with that title already exists.");

        var template = new MessageTemplate
        {
            Title = apiTemplate.Title,
            Text = apiTemplate.Text,
            Owner = await GetCurrentProfessor(token),
        };

        await _context.MessageTemplates.AddAsync(template, token);
        return await ResponseOnSaveAsync(token);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTemplate([FromRoute] int id, CancellationToken token)
    {
        return Ok(new MessageTemplateContract(await GetTemplateById(id, token)));
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetTemplates(CancellationToken token)
    {
        var templates = await GetTemplatesOfProfessor(token);
        return Ok(templates.Select(x => new MessageTemplateContract(x)).ToList());
    }

    private async Task<IActionResult> ResponseOnSaveAsync(CancellationToken token)
    {
        int count = await _context.SaveChangesAsync(token);
        return Ok(new { Result = count != 0 });
    }

    private async Task<Professor> GetCurrentProfessor(CancellationToken token)
    {
        var id = Helpers.GetUserId(User);
        var professor = await _context.Professors.SingleOrDefaultAsync(x => x.User.Id == id, token)
                    ?? throw new BadRequestException("Invalid user id.");
        return professor;
    }

    private async Task<IEnumerable<MessageTemplate>> GetTemplatesOfProfessor(CancellationToken token)
    {
        var professor = await GetCurrentProfessor(token);
        return professor.MessageTemplates;
    }

    private async Task<MessageTemplate> GetTemplateById(int id, CancellationToken token)
    {
        var messages = await GetTemplatesOfProfessor(token);
        return messages.FirstOrDefault(x => x.Id == id)
                ?? throw new BadRequestException("Invalid template id.");
    }
}