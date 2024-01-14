
using BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm;
using BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotickAPI.Server.Controllers
{
    [Route("api/locations")]
    [EnableCors("MyAllowSpecificOrigins")]
    public class LocationController : BaseController
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<LocationCreateEventFormVm>>> GetAll()
        {
            var vm = await Mediator.Send(new GetLocationCreateEventFormQuery());
            return vm;
        }
    }
}
