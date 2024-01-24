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

        public int LocationId { get; set; }

        public List<int> ArtistsId { get; set; }
    }
}
