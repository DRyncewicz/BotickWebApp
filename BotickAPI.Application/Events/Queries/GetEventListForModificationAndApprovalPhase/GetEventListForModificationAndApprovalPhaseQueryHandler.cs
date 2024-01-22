using AutoMapper;
using BotickAPI.Application.Common.Exceptions;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Application.Events.Queries.GetEventListForApprovalPhase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase
{
    public class GetEventListForModificationAndApprovalPhaseQueryHandler(IBotickDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<GetEventListForModificationAndApprovalPhaseQueryHandler> log) : IRequestHandler<GetEventListForModificationAndApprovalPhaseQuery, ListEventForListModificationAndApprovalPhase>
    {
        public async Task<ListEventForListModificationAndApprovalPhase> Handle(GetEventListForModificationAndApprovalPhaseQuery request, CancellationToken cancellationToken)
        {
            var userEmail = currentUserService.Email;
            if (userEmail == null)
            {
                throw new AuthorizationException("It is not possible to use this module without a confirmed email");
            }

            var eventList = await dbContext.Events
                .Include(p => p.Artists)
                .Include(p => p.LocationEvents).ThenInclude(le => le.Location)
                .Where(p => p.OrganizerEmail == userEmail)
                .ToListAsync();

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
