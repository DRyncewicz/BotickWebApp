using AutoMapper;
using Botick.Shared.ViewModels.Artist.Queries.GetArtistForCreateEventForm;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    public class GetArtistsForCreateEventFormQueryHandler(IBaseQueryRepository<Artist> baseQueryRepository, IMapper mapper) : IRequestHandler<GetArtistsForCreateEventFormQuery, ArtistsForCreateEventFormDto>
    {
        public async Task<ArtistsForCreateEventFormDto> Handle(GetArtistsForCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            var searchString = request.SearchString ?? string.Empty;

            var artists = await baseQueryRepository.GetAllAsync();
            artists = artists.Where(p =>
                (p.ArtName != null && p.ArtName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.Name != null && p.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.Surname != null && p.Surname.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));

            return new ArtistsForCreateEventFormDto()
            {
                ArtistsVm = mapper.Map<List<ArtistsForCreateEventFormVm>>(artists)
            };
        }
    }
}
