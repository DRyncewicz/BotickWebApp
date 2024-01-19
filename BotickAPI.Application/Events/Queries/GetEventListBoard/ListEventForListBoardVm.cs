using BotickAPI.Application.Events.Queries.GetEventListForBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListBoard
{
    public class ListEventForListBoardVm
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public ICollection<EventForListBoardVm> EventsForListBoard { get; set; }
    }
}
