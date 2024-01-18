using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.SharedDto;

namespace BotickAPI.Application.Events.Queries.GetEventListForBoard
{
    public class EventForListVm
    {

        public string Name { get; set; }

        public List<LocationDto> Locations { get; set; } = new List<LocationDto>();

        public DateTime StartTime { get; set; }

        public string ImagePath { get; set; }

        public string EventType { get; set; }
    }
}
