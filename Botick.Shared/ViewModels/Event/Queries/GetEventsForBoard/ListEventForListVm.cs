using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botick.Shared.ViewModels.Event.Queries.GetEventsForBoard
{
    public class ListEventForListVm
    {
        public List<EventForListVm> Events { get; set; } = new List<EventForListVm>();

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public string? EventsType { get; set; }
    }
}
