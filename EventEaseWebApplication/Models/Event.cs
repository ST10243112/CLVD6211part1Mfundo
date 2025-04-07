namespace EventEaseWebApplication.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public string EventName { get; set; }
        public DateOnly EventDate { get; set; }
        public string Description { get; set; }
        public List<Booking> Booking { get; set; } = new();

    }
}
