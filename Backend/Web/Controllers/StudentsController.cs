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
    public IActionResult GetStudents([FromQuery] string groupName, CancellationToken token)
    {
        var groups = _context.Groups;

        if (!groups.Any(x => x.Code == groupName))
        {
            return BadRequest("Invalid group code.");
        }

        return Ok(groups.Single(x => x.Code == groupName)
                    .Students.Select(x => new SimpleStudentContract(x)).ToList());
    }

}
