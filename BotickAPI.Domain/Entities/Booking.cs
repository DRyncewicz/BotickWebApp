using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Common;

namespace BotickAPI.Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public string UserId { get; set; }

        public int EventId { get; set; }

        public double TotalPrice { get; set; }

        public DateTime BookingTime { get; set; }

        public string Status { get; set; }
    }
}
