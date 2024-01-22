using AutoMapper;
using BotickAPI.Application.Common.Exceptions;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Application.Events.Queries.GetEventListBoard;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BotickAPI.Application.Events.Queries.GetEventListForBoard
{
    public class GetEventListBoardQueryHandler(IBotickDbContext dbContext,
        IMapper mapper,
        ILogger<GetEventListBoardQueryHandler> log) : IRequestHandler<GetEventListBoardQuery, ListEventForListBoardVm>
    {
        public async Task<ListEventForListBoardVm> Handle(GetEventListBoardQuery request, CancellationToken cancellationToken)
        {
            var eventsForListBoard = await dbContext.Events
                .Include(p => p.Artists)
                .Include(p => p.LocationEvents).ThenInclude(le => le.Location)
                .Where(p => p.Status == "in progress")
                .ToListAsync();

            var eventsForListBoardVm = mapper.Map < List<EventForListBoardVm>>(eventsForListBoard);
            if (eventsForListBoardVm == null)
            {
                throw new NotFoundException("eventForListBoardVm is a empty list - null reference");
            }

            ListEventForListBoardVm listEventForListBoardVm = new ListEventForListBoardVm()
            {
                Count = eventsForListBoardVm.Count(),
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage,
                EventsForListBoard = eventsForListBoardVm
                    .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize).ToList(),
            };

            log.LogInformation($"Successfully return result for GetEventListBoardQueryHandler");
            return listEventForListBoardVm;
        }
    }
}
