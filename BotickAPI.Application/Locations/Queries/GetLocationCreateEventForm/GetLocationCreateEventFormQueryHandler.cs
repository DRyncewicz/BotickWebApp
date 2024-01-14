using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BotickAPI.Application.Locations.Queries.GetLocationsForCreateEventForm
{
    internal class GetLocationCreateEventFormQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper) : IRequestHandler<GetLocationCreateEventFormQuery, List<LocationCreateEventFormVm>>
    {
        public async Task<List<LocationCreateEventFormVm>> Handle(GetLocationCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = sqlConnectionFactory.CreateConnection();

            var query = "SELECT * FROM Locations";
            var locations = await connection.QueryAsync<Location>(query);


            return mapper.Map<List<LocationCreateEventFormVm>>(locations);  
        }
    }
}

