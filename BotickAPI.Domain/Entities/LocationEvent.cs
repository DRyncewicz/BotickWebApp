using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Domain.Entities
{
    public class LocationEvent
    {
        public int EventId { get; set; }

        public Event Event { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
