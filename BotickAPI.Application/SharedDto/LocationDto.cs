using AutoMapper;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.SharedDto
{
    public class LocationDto : IMapFrom<Location>
    {
        public string City { get; set; }

        public string Venue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Location, LocationDto>(); 
        }
    }
}
