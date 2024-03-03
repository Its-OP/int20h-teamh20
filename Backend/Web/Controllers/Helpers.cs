using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
}