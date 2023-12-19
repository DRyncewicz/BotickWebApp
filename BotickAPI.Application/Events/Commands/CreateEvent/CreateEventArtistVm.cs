using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botick.Shared.ViewModels.Event.Commands.CreateEvent
{
    public class CreateEventArtistVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int? Age { get; set; }

        public string ArtName { get; set; }

        public string? BirthCity { get; set; }

        public string Discipline { get; set; }

        public string Description { get; set; }
    }
}
