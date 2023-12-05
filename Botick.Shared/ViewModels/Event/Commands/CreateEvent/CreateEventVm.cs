using Botick.Shared.ViewModels.Event.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botick.Shared.ViewModels.Event.Commands.CreateEvent
{
    public class CreateEventVm
    {
        public string OrganizerEmail { get; set; }

        public string Name { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public byte[] Image { get; set; }

        public List<LocationForNewEvent> Locations { get; set; } = new List<LocationForNewEvent>();

        public List<ArtistForNewEvent> Artists { get; set; } = new List<ArtistForNewEvent>();
    }
}
