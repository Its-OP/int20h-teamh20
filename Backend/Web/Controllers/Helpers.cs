using System.Net;
using System.Security.Claims;

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
}