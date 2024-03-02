using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/activities")]
public class SystemController : ControllerBase
{
    [HttpGet]
    [Route("health")]
    public IActionResult HealthCheck()
    {
        return Ok(DateTime.Now);
    }
}