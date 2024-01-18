using BotickAPI.Application.Events.Queries.GetEventListForApprovalPhase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase
{
    public class GetEventListForModificationAndApprovalPhaseQuery : IRequest<ListEventForListModificationAndApprovalPhase>
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
