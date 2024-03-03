using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[Authorize(Roles = Roles.Professor)]
[ApiController]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public UsersController(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("student")]
    public async Task<IActionResult> CreateStudent(
        [FromBody] StudentArguments apiStudent,
        CancellationToken token
    )
    {
        var group = await _context.Groups.SingleOrDefaultAsync(x => x.Id == apiStudent.GroupId, token);

        if (group is null)
            return BadRequest(new ErrorContract($"Group with id '{apiStudent.GroupId}' does not exist."));

        if (await _context.Users.AnyAsync(x => x.Email == apiStudent.Email, token))
            return BadRequest(new ErrorContract($"User with email '{apiStudent.Email}' already exists."));

        if (await _context.Users.AnyAsync(x => x.PhoneNumber == apiStudent.PhoneNumber, token))
            return BadRequest(new ErrorContract($"User with number '{apiStudent.PhoneNumber} already exists."));

        var user = new User(apiStudent.Firstname,
            apiStudent.Lastname,
            apiStudent.Patronymic,
            apiStudent.PhoneNumber,
            apiStudent.Email,
            await Helpers.GetPasswordHash(apiStudent.Password, token),
            Roles.Student);
        
        var student = new Student
        {
            Group = group,
            User = user
        };

        await _context.Users.AddAsync(user, token);
        await _context.Students.AddAsync(student, token);
        await _context.SaveChangesAsync(token);

        return Ok(new NoContentContract());
    }
    
    [HttpPost]
    [Route("professor")]
    public async Task<IActionResult> CreateProfessor(
        [FromBody] UserArguments arguments,
        CancellationToken token
    )
    {
        if (await _context.Users.AnyAsync(x => x.Email == arguments.Email, token))
            return BadRequest(new ErrorContract($"User with email '{arguments.Email}' already exists."));

        if (await _context.Users.AnyAsync(x => x.PhoneNumber == arguments.PhoneNumber, token))
            return BadRequest(new ErrorContract($"User with number '{arguments.PhoneNumber} already exists."));

        var user = new User(arguments.Firstname,
            arguments.Lastname,
            arguments.Patronymic,
            arguments.PhoneNumber,
            arguments.Email,
            await Helpers.GetPasswordHash(arguments.Password, token),
            Roles.Professor);

        await _context.Users.AddAsync(user, token);
        await _context.Professors.AddAsync(new Professor(user), token);
        await _context.SaveChangesAsync(token);

        return Ok(new NoContentContract());
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("signIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInArguments signInArguments, CancellationToken token)
    {
        var passwordHash = await GetPasswordHash(signInArguments.Password, token);
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == signInArguments.Email
                                                                  && x.PasswordHash == passwordHash, token);
        
        if (user is null)
            return Unauthorized();
    
        return SignInInternal(user);
    }

    private IActionResult SignInInternal(User user)
    {
        var secureKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        if (string.IsNullOrEmpty(secureKey) || string.IsNullOrEmpty(issuer))
            return BadRequest(new ErrorContract(""));
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new (nameof(user.Id), user.Id.ToString()),
            new (ClaimTypes.Role, user.Role),
        };
    
        var jwtToken = new JwtSecurityToken(issuer,
            issuer,
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
    
        var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return Ok(new { Token = jwtString });
    }

    private async Task<string> GetPasswordHash(string password, CancellationToken token)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        using var stream = new MemoryStream(passwordBytes);

        return Convert.ToHexString(await sha256.ComputeHashAsync(stream, token));
    }
}