using BotickAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Domain.Entities
{
    public class BookingDetail : AuditableEntity
    {
        public int BookingId { get; set; }

        public int TicketId { get; set; }

        public int Quantity { get; set; }

        public Booking Booking { get; set; }

        public Ticket Ticket { get; set; }
    }
}
