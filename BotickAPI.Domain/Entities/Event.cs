using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Common;

namespace BotickAPI.Domain.Entities
{
    public class Event :  AuditableEntity
    {
        public int OrganizerEmail { get; set; }

        public string Name { get; set; }

        public string EventType { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }

        public string Status { get; set; }

        public ICollection<EventReview> Reviews { get; set; }

        public ICollection<Ticket> Ticket { get; set; }

        public ICollection<Location> Locations { get; set; }

        public ICollection<Artist> Artists { get; set; }
    }
}
