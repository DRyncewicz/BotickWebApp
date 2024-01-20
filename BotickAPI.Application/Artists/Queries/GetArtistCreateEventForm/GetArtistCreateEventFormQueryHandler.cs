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
    public class GetArtistCreateEventFormQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper) : IRequestHandler<GetArtistCreateEventFormQuery, List<ArtistCreateEventFormVm>>
    {
        public async Task<List<ArtistCreateEventFormVm>> Handle(GetArtistCreateEventFormQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = sqlConnectionFactory.CreateConnection();

            var query = @"SELECT * FROM Artists ";
            var artists = await connection.QueryAsync<Artist>(query);
            connection.Dispose();

            return mapper.Map<List<ArtistCreateEventFormVm>>(artists);
        }
    }
}
