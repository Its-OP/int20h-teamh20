using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/activities")]
public class ActivitiesController : ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public ActivitiesController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateActivity([FromBody] ActivityContract apiActivity, CancellationToken token)
    {
        var student = await _dbContext.Students.SingleOrDefaultAsync(x => x.Id == apiActivity.StudentId, token);
        if (student is null)
            return BadRequest($"Student {apiActivity.StudentId} does not exist");
        
        var subject = await _dbContext.Subjects.SingleOrDefaultAsync(x => string.Equals(x.Title, apiActivity.SubjectTitle, StringComparison.InvariantCultureIgnoreCase), token);
        if (subject is null)
            return BadRequest($"Subject {apiActivity.SubjectTitle} does not exist");
        
        if (!DateTime.TryParse(apiActivity.ConductedAt, out var date))
            return BadRequest("Date cannot be parsed");

        date = date.RoundToMinutes();
        
        if (await _dbContext.Activities.AnyAsync(x => x.ConductedAt == date && string.Equals(x.Subject.Title, apiActivity.SubjectTitle, StringComparison.InvariantCultureIgnoreCase), token))
            return BadRequest("Activity already exists");
        
        var activity = new Activity { Subject = subject, Score = apiActivity.Score, Student = student, ActivityType = apiActivity.Type, ConductedAt = date, StudentWasPresent = !apiActivity.IsAbsent };

        await _dbContext.Activities.AddAsync(activity, token);
        await _dbContext.SaveChangesAsync(token);

        return NoContent();
    }

    [HttpGet]
    [Route("{studentId:int}/{subjectTitle}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityContract>))]
    public async Task<IActionResult> GetActivities([FromQuery] int studentId, [FromQuery] string subjectTitle, CancellationToken token)
    {
        var activities = await _dbContext.Activities.Where(x => x.Student.Id == studentId && string.Equals(x.Subject.Title, subjectTitle)).ToListAsync(token);
        var apiActivities = activities.Select(x => new ActivityContract
        {
            ConductedAt = x.ConductedAt.ToString("s"),
            IsAbsent = !x.StudentWasPresent,
            Score = x.Score,
            StudentId = studentId,
            SubjectTitle = subjectTitle,
            Type = x.ActivityType
        });

        return Ok(apiActivities);
    }
}