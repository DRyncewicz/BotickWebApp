﻿using AutoMapper;
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
    internal class GetLocationsForCreateEventFormQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper) : IRequestHandler<GetLocationsForCreateEventFormQuery, List<LocationsForCreateEventFormVm>>
    {
        public async Task<List<LocationsForCreateEventFormVm>> Handle(GetLocationsForCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = sqlConnectionFactory.CreateConnection();
            var searchString = request.SearchString ?? string.Empty;
            var query = @"
                        SELECT * FROM Locations 
                        WHERE (@SearchString = '' OR City LIKE '%' + @SearchString + '%' 
                        OR Venue LIKE '%' + @SearchString + '%')";
            var locations = await connection.QueryAsync<Location>(query, new { SearchString = searchString });


            return mapper.Map<List<LocationsForCreateEventFormVm>>(locations);  
        }
    }
}

