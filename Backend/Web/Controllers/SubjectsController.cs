using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public SubjectsController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateSubject([FromBody] SubjectArguments apiSubject, CancellationToken token)
    {
        var subject = new Subject(apiSubject.Title, apiSubject.IsExam);
        if (await _dbContext.Subjects.AnyAsync(x => x.Title == apiSubject.Title, token))
            return BadRequest(new ErrorContract("Subject already exists"));

        await _dbContext.Subjects.AddAsync(subject, token);
        await _dbContext.SaveChangesAsync(token);

        return Ok(new NoContentContract());
    }
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubjectContract>))]
    public async Task<IActionResult> GetSubjects(CancellationToken token)
    {
        var subjects = await _dbContext.Subjects.ToListAsync(token);
        return Ok(subjects.Select(x => new SubjectContract { IsExam = x.HasExam, Title = x.Title, Id = x.Id }));
    }
}