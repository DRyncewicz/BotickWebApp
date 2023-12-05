using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botick.Shared.ViewModels.Event.Commands.CreateEvent
{
    public class LocationForCreateNewEventVm
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }

        public int Capacity { get; set; }
    }
}
