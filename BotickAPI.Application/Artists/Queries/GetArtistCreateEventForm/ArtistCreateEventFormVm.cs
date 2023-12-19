using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Domain.Entities;

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    public class ArtistCreateEventFormVm : IMapFrom<Artist>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ArtName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistCreateEventFormVm>();
        }
    }
}
