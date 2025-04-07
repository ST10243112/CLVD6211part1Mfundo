using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseWebApplication.Models
{
    public class Booking
    {
     
        public int BookingId { get; set; }
        public int VenueId { get; set; }
        public int EventId { get; set; }
        public DateOnly BookingDate { get; set; }
      
    }
}
