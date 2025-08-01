using System.ComponentModel.DataAnnotations;

namespace RoomBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
