using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    public class GetArtistCreateEventFormQueryHandler(IBotickDbContext dbContext, IMapper mapper) : IRequestHandler<GetArtistCreateEventFormQuery, List<ArtistCreateEventFormVm>>
    {
        public async Task<List<ArtistCreateEventFormVm>> Handle(GetArtistCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            var artists = await dbContext.Artists.ToListAsync(cancellationToken);

            return mapper.Map<List<ArtistCreateEventFormVm>>(artists);
        }
    }
}
