using AutoMapper;
using Botick.Shared.ViewModels.Artist.Queries.GetArtistForCreateEventForm;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botick.Shared.ViewModels.Location.Queries.GetLocationForCreateEventForm;

namespace BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm
{
    public class LocationsForCreateEventFormDto : IMapFrom<Location>
    {
        public List<LocationsForCreateEventFormVm> LocationsVm { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Location, LocationsForCreateEventFormVm>();
        }
    }
}
