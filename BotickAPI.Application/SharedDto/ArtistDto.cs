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
    public class ArtistDto : IMapFrom<Artist>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string ArtName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistDto>();
        }
    }
}
