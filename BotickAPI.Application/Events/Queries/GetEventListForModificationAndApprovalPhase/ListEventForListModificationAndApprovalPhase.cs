using BotickAPI.Application.Events.Queries.GetEventListForApprovalPhase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase
{
    public class ListEventForListModificationAndApprovalPhase
    {
        public ICollection<EventForListModificationAndApprovalPhase> EventsForListModificationAndApprovalPhase { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
