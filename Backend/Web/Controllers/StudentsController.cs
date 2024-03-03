using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize]
[Route("api/students")]
public class StudentsController(IApplicationDbContext context) : ControllerBase
{
    private readonly IApplicationDbContext _context = context;

    [HttpGet]
    [Route("group/{groupId:int}")]
    public async Task<IActionResult> GetStudents([FromRoute] int groupId, CancellationToken token)
    {
        var query = _context.Groups.Include(x => x.Students).AsQueryable();
        if (User.IsInRole(Roles.Student))
        {
            var studentId = Helpers.GetUserId(User);
            query = query.Where(x => x.Students.Any(s => s.Id == studentId));
        }
        
        var group = await query.SingleOrDefaultAsync(x => x.Id == groupId, token);

        if (group is null)
            return BadRequest(new ErrorContract($"Group with id '{groupId}' does not exist or the user does not have permissions to access it."));

        return Ok(group.Students.Select(x => new SimpleStudentContract(x)).ToList());
    }

    [HttpGet]
    [Route("{studentId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentContract))]
    public async Task<IActionResult> GetStudent([FromRoute] int studentId, CancellationToken token)
    {
        if (!Helpers.UserShouldHaveAccessToStudentData(User, studentId))
            return Unauthorized(new ErrorContract());
        
        var student = await _context.Students.Include(x => x.Activities).SingleOrDefaultAsync(x => x.Id == studentId, token);
        if (student is null)
            return BadRequest(new ErrorContract($"Student {studentId} does not exist."));

        return Ok(new StudentContract(student));
    }
    
    [HttpPut]
    [Route("{studentId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentContract))]
    public async Task<IActionResult> UpdateSocialMedia([FromRoute] int studentId, [FromBody] SocialMedias media, CancellationToken token)
    {
        if (!Helpers.UserShouldHaveAccessToStudentData(User, studentId))
            return Unauthorized(new ErrorContract());
        
        var student = await _context.Students.Include(x => x.Activities).SingleOrDefaultAsync(x => x.Id == studentId, token);
        if (student is null)
            return BadRequest(new ErrorContract($"Student {studentId} does not exist."));
        
        student.UpdateSocialMedia(media);

        return Ok(new StudentContract(student));
    }
}
