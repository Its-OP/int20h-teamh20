using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/groups")]
public class GroupsController: ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public GroupsController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateGroup([FromBody] GroupContract apiGroup, CancellationToken token)
    {
        if (await _dbContext.Groups.AnyAsync(x => string.Equals(x.Code, apiGroup.Code, StringComparison.InvariantCultureIgnoreCase), token))
            return BadRequest("Group already exists");

        if (apiGroup.Subjects.Count() > 15)
            return BadRequest("Too many subjects");

        var subjectTitles = apiGroup.Subjects.Select(x => x.Title).Distinct(StringComparer.InvariantCultureIgnoreCase);
        var matchingSubjects = await _dbContext.Subjects.Where(x => subjectTitles.Contains(x.Title, StringComparer.InvariantCultureIgnoreCase)).ToListAsync(token);
        
        var group = new Group(apiGroup.Code, matchingSubjects);
        await _dbContext.Groups.AddAsync(group, token);
        await _dbContext.SaveChangesAsync(token);

        return NoContent();
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupContract>))]
    public async Task<IActionResult> GetGroups(CancellationToken token)
    {
        var groups = await _dbContext.Groups.Select(x => new GroupContract { Code = x.Code }).ToListAsync(token);
        return Ok(groups);
    }
}