using System.Text.RegularExpressions;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/groups")]
public class GroupsController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public GroupsController(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups(CancellationToken token)
    {
        // var groups = await _context.Groups.ToListAsync(token);
        var groups = new List<domain.Group> {
            new(0, "КП-01"),
            new(1, "KП-02"),
            new(2, "KП-03"),
        };
        return Ok(groups.Select(x => new ApiContracts.GroupContract(x)));
    }
}