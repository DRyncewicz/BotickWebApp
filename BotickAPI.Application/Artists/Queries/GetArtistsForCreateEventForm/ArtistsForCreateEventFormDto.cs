using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Botick.Shared.ViewModels.Artist.Queries.GetArtistForCreateEventForm;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Domain.Entities;

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    public class ArtistsForCreateEventFormDto : IMapFrom<Artist>
    {
        public List<ArtistsForCreateEventFormVm> ArtistsVm { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistsForCreateEventFormVm>();
        }
    }
}
