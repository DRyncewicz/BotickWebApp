using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Botick.Shared.ViewModels.Event.Commands.CreateEvent;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Domain.Entities;
using MediatR;

namespace BotickAPI.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<int>, IMapFrom<Event>
    {
        public string Name { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public byte[] Image { get; set; }

        public List<LocationForCreateNewEventVm> Locations { get; set; } = new List<LocationForCreateNewEventVm>();

        public List<ArtistForCreateNewEventVm> Artists { get; set; } = new List<ArtistForCreateNewEventVm>();

        public void Mapping(Profile profile)
        {

            profile.CreateMap<CreateEventCommand, Event>()
                .ForMember(dest => dest.LocationEvents, opt => opt.MapFrom(src =>
                    src.Locations.Select(loc => new LocationEvent { LocationId = loc.Id })))
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists));

            profile.CreateMap<LocationForCreateNewEventVm, Location>();
            profile.CreateMap<ArtistForCreateNewEventVm, Artist>();
        }
    }
}