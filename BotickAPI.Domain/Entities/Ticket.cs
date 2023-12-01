using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Common;

namespace BotickAPI.Domain.Entities
{
    public class Ticket : AuditableEntity
    {
        public int EventId { get; set; }

        public double Price { get; set; }

        public string TicketType { get; set; }

        public Event Event { get; set; }

        public ICollection<BookingDetail> Bookings { get; set; }
    }
}
