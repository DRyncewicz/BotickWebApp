using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm
{
    public class GetLocationsForCreateEventFormQuery : IRequest<List<LocationsForCreateEventFormVm>>
    {
        public string SearchString { get; set; }
    }
}
