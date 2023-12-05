using Botick.Shared.ViewModels.Artist.Queries.GetArtistForCreateEventForm;
using Botick.Shared.ViewModels.Location.Queries.GetLocationForCreateEventForm;
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
        [HttpGet("{searchString}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<LocationsForCreateEventFormVm>>> GetBySearchString(string searchString)
        {
            var vm = await Mediator.Send(new GetLocationsForCreateEventFormQuery() { SearchString = searchString });
            return vm.LocationsVm;
        }
    }
}
