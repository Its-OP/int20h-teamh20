using backend.ApiContracts;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public AnalyticsController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("student/{studentId:int}/attendance")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentAttendanceContract>))]
    public async Task<IActionResult> GetStudentAttendance([FromRoute] int studentId, CancellationToken token)
    {
        if (!await _dbContext.Students.AnyAsync(x => x.Id == studentId, token))
            return BadRequest(new ErrorContract($"Student {studentId} not found"));

        var activities = await _dbContext.Activities
            .Where(x => x.Student.Id == studentId)
            .GroupBy(x => x.Subject.Title)
            .Select(x => new StudentAttendanceContract(x.Key, x.Count(a => a.StudentWasPresent == true), x.Count()))
            .ToListAsync(token);

        return Ok(activities);
    }
}