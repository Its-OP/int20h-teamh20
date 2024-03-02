using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class SubjectsController : ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public SubjectsController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateSubject([FromBody] SubjectContract apiSubject, CancellationToken token)
    {
        var subject = new Subject(apiSubject.Title, apiSubject.IsExam);
        if (await _dbContext.Subjects.AnyAsync(x => x.Title == subject.Title, token))
            return BadRequest("Subject already exists");

        await _dbContext.Subjects.AddAsync(subject, token);
        await _dbContext.SaveChangesAsync(token);

        return NoContent();
    }
}