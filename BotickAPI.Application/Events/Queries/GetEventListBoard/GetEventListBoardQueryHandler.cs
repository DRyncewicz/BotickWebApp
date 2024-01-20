using AutoMapper;
using BotickAPI.Application.Common.Exceptions;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Application.Events.Queries.GetEventListBoard;
using BotickAPI.Application.Helpers;
using BotickAPI.Domain.Entities;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListForBoard
{
    public class GetEventListBoardQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper, ILogger<GetEventListBoardQueryHandler> log) : IRequestHandler<GetEventListBoardQuery, ListEventForListBoardVm>
    {
        public async Task<ListEventForListBoardVm> Handle(GetEventListBoardQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = sqlConnectionFactory.CreateConnection();

            var query = @"
                SELECT e.*, l.*, a.*
                FROM Events e
                LEFT JOIN LocationEvent le ON e.Id = le.EventId
                LEFT JOIN Locations l ON le.LocationId = l.Id
                LEFT JOIN ArtistEvent ae ON e.Id = ae.EventsId
                LEFT JOIN Artists a ON ae.ArtistsId = a.Id
                WHERE e.Status = @SearchValue";

            var eventList = await MapDapperQueryForEventListToReceiveLocationAndArtistEntity.MapAllAsync(connection, query, "in progress");

            await connection.DisposeAsync();

            var eventForListBoardVms = mapper.Map<List<EventForListBoardVm>>(eventList);

            if (eventForListBoardVms == null)
            {
                throw new NotFoundException("eventForListBoardVm is a empty list - null reference");
            }

            ListEventForListBoardVm listEventForListBoardVm = new ListEventForListBoardVm()
            {
                Count = eventForListBoardVms.Count(),
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                EventsForListBoard = eventForListBoardVms
                    .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize).ToList(),
            };

            log.LogInformation($"Successfully return result for GetEventListBoardQueryHandler");
            return listEventForListBoardVm;
        }
    }
}
