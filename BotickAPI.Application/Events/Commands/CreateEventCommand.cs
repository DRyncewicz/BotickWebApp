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

namespace BotickAPI.Application.Events.Commands
{
    public class CreateEventCommand : IRequest<int>, IMapFrom<Event>
    {
        public CreateEventVm CreateEventVm { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<CreateEventVm, Event>()
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations))
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists));

            profile.CreateMap<LocationForNewEvent, Location>();
            profile.CreateMap<ArtistForNewEvent, Artist>();
        }
    }
}