using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm
{
    internal class GetLocationCreateEventFormQueryHandler(IBotickDbContext dbContext, IMapper mapper) : IRequestHandler<GetLocationCreateEventFormQuery, List<LocationCreateEventFormVm>>
    {
        public async Task<List<LocationCreateEventFormVm>> Handle(GetLocationCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            var locations = await dbContext.Locations.ToListAsync(cancellationToken);

            return mapper.Map<List<LocationCreateEventFormVm>>(locations);  
        }
    }
}

