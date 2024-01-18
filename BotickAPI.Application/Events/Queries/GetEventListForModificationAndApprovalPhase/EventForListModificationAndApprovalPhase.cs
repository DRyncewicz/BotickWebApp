using AutoMapper;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Application.SharedDto;
using BotickAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListForApprovalPhase
{
    public class EventForListModificationAndApprovalPhase : IMapFrom<Event>
    {
        public string Name { get; set; }

        public string EventType { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Status { get; set; }

        public ICollection<LocationDto> Locations { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventForListModificationAndApprovalPhase>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
