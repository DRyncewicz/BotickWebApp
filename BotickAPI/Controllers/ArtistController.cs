﻿
using BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotickAPI.Server.Controllers
{
    [Route("api/artists")]
    [EnableCors("MyAllowSpecificOrigins")]
    public class ArtistController : BaseController
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ArtistCreateEventFormVm>>> GetAll()
        {
            var vm = await Mediator.Send(new GetArtistCreateEventFormQuery());
            return vm;
        }
    }
}
