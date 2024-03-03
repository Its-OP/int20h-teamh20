using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/students")]
public class StudentsController(IApplicationDbContext context) : ControllerBase
{
    private readonly IApplicationDbContext _context = context;

    [HttpGet]
    [Route("group/{groupId:int}")]
    public async Task<IActionResult> GetStudents([FromRoute] int groupId, CancellationToken token)
    {
        var group = await _context.Groups.Include(x => x.Students).SingleOrDefaultAsync(x => x.Id == groupId, token);

        if (group is null)
            return BadRequest(new ErrorContract($"Group with id '{groupId}' does not exist."));

        return Ok(group.Students.Select(x => new SimpleStudentContract(x)).ToList());
    }

    [HttpGet]
    [Route("{studentId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentContract))]
    public async Task<IActionResult> GetStudent([FromRoute] int studentId, CancellationToken token)
    {
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
        var student = await _context.Students.Include(x => x.Activities).SingleOrDefaultAsync(x => x.Id == studentId, token);
        if (student is null)
            return BadRequest(new ErrorContract($"Student {studentId} does not exist."));
        
        student.UpdateSocialMedia(media);

        return Ok(new StudentContract(student));
    }
}
