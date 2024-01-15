using BotickAPI.Application.Events.Commands.CreateEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BotickAPI.Server.Controllers
{
    [Route("api/events")]
    [EnableCors("MyAllowSpecificOrigins")]
    [Authorize]
    public class EventController : BaseController
    {
        //[Authorize(Roles = "Organiser")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateEventCommand command)
        {
            var result = await Mediator.Send(command);
            var locationUri = ""; //Url.Action("GetDetails", new { id = result });

            return Created(locationUri, result);
        }
    }
}