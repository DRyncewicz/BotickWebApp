namespace Botick.Models.Commands.Events.CreateEvent
{
    public class CreateEventVm
    {
        public string Name { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public byte[] Image { get; set; }

        public List<CreateEventLocationVm> Locations { get; set; } = new List<CreateEventLocationVm>();

        public List<CreateEventArtistVm> Artists { get; set; } = new List<CreateEventArtistVm>();

    }
}
