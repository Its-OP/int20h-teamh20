using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize]
[Route("api/groups")]
public class GroupsController : ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public GroupsController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateGroup([FromBody] GroupArguments apiGroup, CancellationToken token)
    {
        var code = apiGroup.Code.ToUpper();
        if (await _dbContext.Groups.AnyAsync(x => x.Code == code, token))
            return BadRequest(new ErrorContract("Group already exists"));

        if (apiGroup.SubjectIds.Count() > 15)
            return BadRequest(new ErrorContract("Too many subjects"));

        var matchingSubjects = await _dbContext.Subjects.Where(x => apiGroup.SubjectIds.Contains(x.Id)).ToListAsync(token);

        var group = new Group(code, matchingSubjects);
        await _dbContext.Groups.AddAsync(group, token);
        await _dbContext.SaveChangesAsync(token);

        return Ok(new NoContentContract());
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupContract>))]
    public async Task<IActionResult> GetGroups(CancellationToken token)
    {
        var query = _dbContext.Groups.AsQueryable();
        if (User.IsInRole(Roles.Student))
        {
            var studentId = Helpers.GetUserId(User);
            query = query.Where(x => x.Students.Any(s => s.Id == studentId));
        }
            
        var groups = await query.Select(x => new GroupContract(x)).ToListAsync(token);
        return Ok(groups);
    }
}