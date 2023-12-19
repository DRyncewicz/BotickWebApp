using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListForBoard
{
    public class GetEventListBoardQuery : IRequest<List<EventForListVm>>
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public string? EventsType { get; set; }
    }
}
