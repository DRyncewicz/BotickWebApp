using BotickAPI.Application.Events.Commands.CreateEvent;
using BotickAPI.Application.Events.Queries.GetEventListForBoard;
using BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase;
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
        [Authorize(Roles = "Organiser")]
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

        [Authorize(Roles = "Organiser")]
        [HttpGet("/user-events")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetForModificationAndApprovalPhase(int pageSize, int currentPage)
        {
            var result = await Mediator.Send(new GetEventListForModificationAndApprovalPhaseQuery()
            {
                PageSize = pageSize,
                CurrentPage = currentPage
            });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllForBoardMainView(int pageSize, int currentPage)
        {
            var result = await Mediator.Send(new GetEventListBoardQuery()
            {
                PageSize = pageSize,
                CurrentPage = currentPage
            });

            return Ok(result);
        }
    }
}