using AutoMapper;
using BotickAPI.Application.Common.Exceptions;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Application.Events.Queries.GetEventListForApprovalPhase;
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

namespace BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase
{
    public class GetEventListForModificationAndApprovalPhaseQueryHandler(IDbQueryService dbQueryService,
        IMapper mapper, ICurrentUserService currentUserService,
        ILogger<GetEventListForModificationAndApprovalPhaseQueryHandler> log,
        MappingMultiEntityQueryHelper mappingMultiEntityQueryHelper) : IRequestHandler<GetEventListForModificationAndApprovalPhaseQuery, ListEventForListModificationAndApprovalPhase>
    {
        public async Task<ListEventForListModificationAndApprovalPhase> Handle(GetEventListForModificationAndApprovalPhaseQuery request, CancellationToken cancellationToken)
        {
            var userEmail = currentUserService.Email;
            if (userEmail == null)
            {
                throw new AuthorizationException("It is not possible to use this module without a confirmed email");
            }

            var query = @"
                SELECT e.*, l.*, a.*
                FROM Events e
                LEFT JOIN LocationEvent le ON e.Id = le.EventId
                LEFT JOIN Locations l ON le.LocationId = l.Id
                LEFT JOIN ArtistEvent ae ON e.Id = ae.EventsId
                LEFT JOIN Artists a ON ae.ArtistsId = a.Id
                WHERE e.OrganizerEmail = @SearchValue";

            var eventList = await mappingMultiEntityQueryHelper.MapEventListToReceiveLocationAndArtistAsync(dbQueryService, query, userEmail);

            if (eventList.Any())
            {
                var userEventsVm = mapper.Map<List<EventForListModificationAndApprovalPhase>>(eventList);

                ListEventForListModificationAndApprovalPhase listUserEventsVm = new ListEventForListModificationAndApprovalPhase()
                {
                    EventsForListModificationAndApprovalPhase = userEventsVm
                        .Skip((request.CurrentPage - 1) * request.PageSize)
                        .Take(request.PageSize).ToList(),
                    Count = userEventsVm.Count,
                    CurrentPage = request.CurrentPage,
                    PageSize = request.PageSize
                };

                log.LogInformation($"User with email:{userEmail} create request for EventForListModificationAndApprovalPhase succesfully");
                return listUserEventsVm;
            }
            else
            {
                throw new NotFoundException("User has not participated in the organization of any event so far");
            }
        }
    }
}
