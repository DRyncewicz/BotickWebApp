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
        public int OrganizerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        public ICollection<EventReview> Reviews { get; set; }

        public ICollection<Ticket> Ticket { get; set; }
    }
}
