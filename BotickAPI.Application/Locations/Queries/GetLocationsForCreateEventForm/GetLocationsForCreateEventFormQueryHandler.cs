using AutoMapper;

using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm
{
    public class GetLocationsForCreateEventFormQueryHandler(IBaseQueryRepository<Location> baseQueryRepository, IMapper mapper) : IRequestHandler<GetLocationsForCreateEventFormQuery, List<LocationsForCreateEventFormVm>>
    {
        public async Task<List<LocationsForCreateEventFormVm>> Handle(GetLocationsForCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            var searchString = request.SearchString ?? string.Empty;

            var artists = await baseQueryRepository.GetAllAsync();
            artists = artists.Where(p =>
                (p.City != null && p.City.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (p.Venue != null && p.Venue.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0));

            return mapper.Map<List<LocationsForCreateEventFormVm>>(artists);  
        }
    }
}

