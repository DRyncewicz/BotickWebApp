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

        public int LocationId { get; set; }

        public List<int> ArtistsId { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<CreateEventCommand, Event>()
                     .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                     .ForMember(dest => dest.OrganizerEmail, opt => opt.Ignore())
                     .ForMember(dest => dest.Status, opt => opt.Ignore())
                     .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                     .ForMember(dest => dest.Ticket, opt => opt.Ignore())
                     .ForMember(dest => dest.Artists, opt => opt.Ignore())
                     .ForMember(dest => dest.Id, opt => opt.Ignore())
                     .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                     .ForMember(dest => dest.Location, opt => opt.Ignore())
                     .ForMember(dest => dest.Created, opt => opt.Ignore())
                     .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                     .ForMember(dest => dest.Modified, opt => opt.Ignore())
                     .ForMember(dest => dest.StatusId, opt => opt.Ignore())
                     .ForMember(dest => dest.InactivatedBy, opt => opt.Ignore())
                     .ForMember(dest => dest.Inactivated, opt => opt.Ignore());
        }
    }
}