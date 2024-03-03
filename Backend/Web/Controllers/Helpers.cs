using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using domain;
using domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;


public class BadRequestException(string message) : Exception(message)
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
}


public static class Helpers
{
    public static int GetUserID(this ClaimsPrincipal principal)
    {
        return int.Parse(principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
    
    public static async Task<string> GetPasswordHash(string password, CancellationToken token)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        using var stream = new MemoryStream(passwordBytes);

        return Convert.ToHexString(await sha256.ComputeHashAsync(stream, token));
    }

    public static async Task<bool> UserShouldHaveAccessToStudentData(ClaimsPrincipal user, int studentId, IApplicationDbContext context)
    {
        if (user.IsInRole(Roles.Professor))
            return true;

        if (int.TryParse(user.FindFirst(nameof(User.Id))?.Value, out var claimedId)
            && (await context.Students.SingleOrDefaultAsync(x => x.User.Id == claimedId))?.Id  == studentId)
            return true;

        return false;
    }
    
    public static int? GetUserId(ClaimsPrincipal user)
    {
        if (int.TryParse(user.FindFirst(nameof(User.Id))?.Value, out var claimedId))
            return claimedId;

        return null;
    }
}