using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[Route("api/students")]
public class StudentsController(IApplicationDbContext context) : ControllerBase
{
    private readonly IApplicationDbContext _context = context;

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetStudents([FromQuery] int groupId, CancellationToken token)
    {
        var group = await _context.Groups.SingleOrDefaultAsync(x => x.Id == groupId, token);

        if (group is null)
            return BadRequest($"Group with id '{groupId}' does not exist.");

        return Ok(group.Students.Select(x => new SimpleStudentContract(x)).ToList());
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateStudent(
        [FromBody] StudentArguments apiStudent,
        CancellationToken token
    )
    {
        var group = await _context.Groups.SingleOrDefaultAsync(x => x.Id == apiStudent.GroupId, token);

        if (group is null)
            return BadRequest($"Group with id '{apiStudent.GroupId}' does not exist.");

        if (await _context.Students.AnyAsync(x => x.Email == apiStudent.Email, token))
            return BadRequest($"Student with email '{apiStudent.Email}' already exists.");

        if (await _context.Students.AnyAsync(x => x.MobileNumber == apiStudent.PhoneNumber, token))
            return BadRequest($"Student with number '{apiStudent.PhoneNumber} already exists.");

        var student = new Student
        {
            Group = group,
            FirstName = apiStudent.Firstname,
            LastName = apiStudent.Lastname,
            Patronymic = apiStudent.Patronymic,
            MobileNumber = apiStudent.PhoneNumber,
            Email = apiStudent.Email,
        };

        await _context.Students.AddAsync(student, token);
        await _context.SaveChangesAsync(token);

        return NoContent();
    }
}
