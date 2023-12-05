using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Common;

namespace BotickAPI.Domain.Entities
{
    public class Location : AuditableEntity
    {
        public string City { get; set; }

        public string Venue { get; set; }

        public int Capacity { get; set; }
    }
}
