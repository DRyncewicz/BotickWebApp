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

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    internal class GetArtistsForCreateEventFormQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper) : IRequestHandler<GetArtistsForCreateEventFormQuery, List<ArtistsForCreateEventFormVm>>
    {
        public async Task<List<ArtistsForCreateEventFormVm>> Handle(GetArtistsForCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = sqlConnectionFactory.CreateConnection();
            var searchString = request.SearchString ?? string.Empty;
            var query = @"
                SELECT * FROM Artists 
                WHERE (ArtName IS NOT NULL AND ArtName LIKE '%' + @SearchString + '%' ) 
                OR (Name IS NOT NULL AND Name LIKE '%' + @SearchString + '%' ) 
                OR (Surname IS NOT NULL AND Surname LIKE '%' + @SearchString + '%' )";

            var artists = await connection.QueryAsync<Artist>(query, new { SearchString = searchString });

            return mapper.Map<List<ArtistsForCreateEventFormVm>>(artists);
        }
    }
}
