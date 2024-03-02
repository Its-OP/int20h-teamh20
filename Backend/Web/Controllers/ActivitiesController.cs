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
    [Route("types")]
    public async Task<IActionResult> CreateActivityType([FromBody] ActivityTypeContract apiActivity, CancellationToken token)
    {
        if (await _dbContext.ActivityTypes.AnyAsync(x => x.Title == apiActivity.Title, token))
            return BadRequest($"ActivityType {apiActivity.Title} already exists");

        var type = new ActivityType { Title = apiActivity.Title };
        await _dbContext.ActivityTypes.AddAsync(type, token);
        await _dbContext.SaveChangesAsync(token);

        return NoContent();
    }
    
    [HttpGet]
    [Route("types")]
    public async Task<IActionResult> GetActivityTypes(CancellationToken token)
    {
        var types = await _dbContext.ActivityTypes.Select(x => new ActivityTypeContract { Id = x.Id, Title = x.Title }).ToListAsync(token);
        return Ok(types);
    }
    
    [HttpPost]
    [Route("subject/{subjectId:int}/at/{conductedAtISO8601}/type/{activityTypeId:int}")]
    public async Task<IActionResult> CreateActivities([FromRoute] int subjectId,
        [FromRoute] string conductedAtISO8601,
        [FromRoute] int activityTypeId,
        [FromBody] List<ActivityArguments> apiActivities,
        CancellationToken token)
    {
        var subject = await _dbContext.Subjects.SingleOrDefaultAsync(x => x.Id == subjectId, token);
        if (subject is null)
            return BadRequest($"Subject {subjectId} does not exist");
        
        var activityType = await _dbContext.ActivityTypes.SingleOrDefaultAsync(x => x.Id == activityTypeId, token);
        if (activityType is null)
            return BadRequest($"Activity type {activityType} does not exist");
        
        if (!DateTime.TryParse(conductedAtISO8601, out var date))
            return BadRequest("Date cannot be parsed");
        
        if (apiActivities.Count > 50)
            return BadRequest("Too many activities");

        var studentIds = apiActivities.Select(x => x.StudentId).Distinct().ToList();
        var students = await _dbContext.Students.Where(x => studentIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id, x => x, token);
        
        var activities = new List<Activity>();
        foreach (var apiActivity in apiActivities)
        {
            if (!students.ContainsKey(apiActivity.StudentId))
                continue;

            var activity = new Activity
            {
                ActivityType = activityType,
                ConductedAt = date.RoundToMinutes(),
                Score = apiActivity.Score,
                Student = students[apiActivity.StudentId],
                Subject = subject,
                StudentWasPresent = !apiActivity.IsAbsent
            };
            
            activities.Add(activity);
        }

        await _dbContext.Activities.AddRangeAsync(activities, token);
        await _dbContext.SaveChangesAsync(token);

        return NoContent();
    }

    [HttpGet]
    [Route("student/{studentId:int}/subject/{subjectId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityContract>))]
    public async Task<IActionResult> GetActivities([FromRoute] int studentId, [FromRoute] int subjectId, CancellationToken token)
    {
        var activities = await _dbContext.Activities.Where(x => x.Student.Id == studentId && x.Subject.Id == subjectId).ToListAsync(token);
        var apiActivities = activities.Select(x => new ActivityContract
        {
            ConductedAt = x.ConductedAt.ToString("s"),
            IsAbsent = !x.StudentWasPresent,
            Score = x.Score,
            StudentId = studentId,
            TypeId = x.ActivityType.Id
        });

        return Ok(apiActivities);
    }
}