using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotickAPI.Controllers
{
    [Route("api/hc")]
    [ApiController]
    [AllowAnonymous]
    public class HealthChecksController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> GetAsync()
        {
            return "Healthy";
        }
    }
}