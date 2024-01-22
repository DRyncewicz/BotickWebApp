using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Application.SharedDto;
using BotickAPI.Domain.Entities;

namespace BotickAPI.Application.Events.Queries.GetEventListForBoard
{
    public class EventForListBoardVm : IMapFrom<Event>
    {

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public string ImagePath { get; set; }

        public string EventType { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }

        public ICollection<LocationDto> Locations { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventForListBoardVm>()
                .ForMember(p => p.Locations, map => map.MapFrom(src => src.LocationEvents.Select(x => x.Location)));
        }
    }
}
