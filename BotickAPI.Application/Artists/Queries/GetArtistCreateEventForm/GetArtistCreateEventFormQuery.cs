using MediatR;

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    public class GetArtistCreateEventFormQuery : IRequest<List<ArtistCreateEventFormVm>>
    {

    }
}
