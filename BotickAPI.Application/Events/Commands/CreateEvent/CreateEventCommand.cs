using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        public List<int> LocationsId { get; set; }

        public List<int> ArtistsId { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<CreateEventCommand, Event>()
                     .ForMember(dest => dest.ImagePath, opt => opt.Ignore()) 
                     .ForMember(dest => dest.OrganizerEmail, opt => opt.Ignore()) 
                     .ForMember(dest => dest.Status, opt => opt.Ignore())                                           
                     .ForMember(dest => dest.LocationEvents, opt => opt.MapFrom(src =>
                         src.LocationsId.Select(id => new LocationEvent { LocationId = id }).ToList()))

                     .ForMember(dest => dest.Artists, opt => opt.MapFrom(src =>
                         src.ArtistsId.Select(id => new Artist { Id = id }).ToList()));
        }
    }
}