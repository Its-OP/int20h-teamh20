using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Microsoft.AspNetCore.Components.Route("api/system")]
public class System : ControllerBase
{
    [HttpGet]
    [Route("health")]
    public IActionResult HealthCheck()
    {
        return Ok(DateTime.Now);
    }
}