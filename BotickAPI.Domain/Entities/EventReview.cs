using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Common;

namespace BotickAPI.Domain.Entities
{
    public class EventReview : AuditableEntity
    {
        public int EventId { get; set; }

        public string UserId { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public Event Event { get; set; }
    }
}
