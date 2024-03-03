﻿using backend.ApiContracts;
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
    
    [HttpGet]
    [Route("student/{studentId:int}/scores")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentScoresContract>))]
    public async Task<IActionResult> GetStudentScores([FromRoute] int studentId, CancellationToken token)
    {
        if (!await _dbContext.Students.AnyAsync(x => x.Id == studentId, token))
            return BadRequest(new ErrorContract($"Student {studentId} not found"));

        var activities = await _dbContext.Activities
            .Where(x => x.Student.Id == studentId)
            .GroupBy(x => x.Subject.Title)
            .Select(x => new StudentScoresContract(x.Key, x.Sum(a => a.Score), x.Sum(a => a.MaxScore)))
            .ToListAsync(token);

        return Ok(activities);
    }

    [HttpGet]
    [Route("group/{groupId:int}/subject/{subjectId:int}/attendance")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentScoresContract>))]
    public async Task<IActionResult> GetGroupAttendance([FromRoute] int groupId, [FromRoute] int subjectId, CancellationToken token)
    {
        if (!await _dbContext.Groups.AnyAsync(x => x.Id == groupId, token))
            return BadRequest(new ErrorContract($"Group {groupId} not found"));
        
        if (!await _dbContext.Subjects.AnyAsync(x => x.Id == subjectId, token))
            return BadRequest(new ErrorContract($"Subject {subjectId} not found"));
    
        var query = _dbContext.Activities
            .Where(x => x.Subject.Id == subjectId)
            .Where(x => x.Student.Group.Id == groupId)
            .GroupBy(x => x.ConductedAt);
    
        var count = (await query.FirstAsync(token)).Count();
        var activities = await query
            .Select(x => new GroupAttendanceContract(x.Key.ToString("s"), x.Count(a => a.StudentWasPresent), count))
            .ToListAsync(token);
    
        return Ok(activities);
    }
    
    [HttpGet]
    [Route("group/{groupId:int}/subject/{subjectId:int}/scores")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentScoresContract>))]
    public async Task<IActionResult> GetGroupScores([FromRoute] int groupId, [FromRoute] int subjectId, CancellationToken token)
    {
        if (!await _dbContext.Groups.AnyAsync(x => x.Id == groupId, token))
            return BadRequest(new ErrorContract($"Group {groupId} not found"));
        
        if (!await _dbContext.Subjects.AnyAsync(x => x.Id == subjectId, token))
            return BadRequest(new ErrorContract($"Subject {subjectId} not found"));

        var query = _dbContext.Activities
            .Where(x => x.Subject.Id == subjectId)
            .Where(x => x.Student.Group.Id == groupId)
            .GroupBy(x => x.Student.Id);

        var maxScore = (await query.FirstAsync(token)).Sum(x => x.MaxScore);
        var activities =  await query
            .Select(x => new GroupScoresContract(x.Key, x.Sum(a => a.Score), maxScore))
            .ToListAsync(token);
        
        return Ok(activities);
    }
}