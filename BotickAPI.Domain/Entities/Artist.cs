using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Common;

namespace BotickAPI.Domain.Entities
{
    public class Artist : AuditableEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int? Age { get; set; }

        public string ArtName { get; set; }

        public string? BirthCity { get; set; }

        public string Discipline { get; set;}

        public string Description { get; set; }

        public int Likes { get; set; } = 0;

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<Event> Events {get; set; }
    }
}
