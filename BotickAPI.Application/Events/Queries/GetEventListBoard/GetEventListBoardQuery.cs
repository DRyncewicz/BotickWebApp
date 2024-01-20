using BotickAPI.Application.Events.Queries.GetEventListBoard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListForBoard
{
    public class GetEventListBoardQuery : IRequest<ListEventForListBoardVm>
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
