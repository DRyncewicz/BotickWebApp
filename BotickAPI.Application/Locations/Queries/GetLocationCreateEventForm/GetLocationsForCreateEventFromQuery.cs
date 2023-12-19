using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm
{
    public class GetLocationCreateEventFormQuery : IRequest<List<LocationCreateEventFormVm>>
    {
        public string SearchString { get; set; }
    }
}
