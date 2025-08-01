using RoomBookingSystem.Models;

namespace RoomBookingSystem.Service;

public class BookingService
{
    public List<Room> Rooms { get; } = new();
    public List<Booking> Bookings { get; } = new();

    private int bookingId = 1;

    public BookingService()
    {
        // Prepopulate 3 rooms
        Rooms.AddRange(new[]
        {
            new Room { Id = 1, Name = "Room A", Capacity = 10 },
            new Room { Id = 2, Name = "Room B", Capacity = 8 },
            new Room { Id = 3, Name = "Room C", Capacity = 5 }
        });
    }

    public bool AddBooking(Booking newBooking, out string error)
    {
        error = "";

        if (string.IsNullOrWhiteSpace(newBooking.UserName))
        {
            error = "User name is required.";
            return false;
        }

        if (newBooking.EndTime <= newBooking.StartTime)
        {
            error = "End time must be after start time.";
            return false;
        }

        // ❗ Corrected: check for overlapping in same room (RoomId)
        bool overlaps = Bookings.Any(b =>
            b.RoomId == newBooking.RoomId &&
            newBooking.StartTime < b.EndTime &&
            newBooking.EndTime > b.StartTime &&
            b.Id != newBooking.Id // Exclude current booking in edit
        );

        if (overlaps)
        {
            error = "This booking overlaps with an existing one.";
            return false;
        }

        if (newBooking.Id == 0)
        {
            newBooking.Id = bookingId++;
            Bookings.Add(newBooking);
        }
        else
        {
            var existing = Bookings.FirstOrDefault(b => b.Id == newBooking.Id);
            if (existing != null)
            {
                existing.UserName = newBooking.UserName;
                existing.RoomId = newBooking.RoomId;
                existing.StartTime = newBooking.StartTime;
                existing.EndTime = newBooking.EndTime;
            }
            else
            {
                error = "Booking not found for editing.";
                return false;
            }
        }

        return true;
    }

    public void DeleteBooking(int id)
    {
        var booking = Bookings.FirstOrDefault(b => b.Id == id);
        if (booking != null)
        {
            Bookings.Remove(booking);
        }
    }

    public IEnumerable<Booking> GetBookingsByRoom(int? roomId = null) =>
        roomId.HasValue ? Bookings.Where(b => b.RoomId == roomId.Value) : Bookings;

    public Booking? GetBookingById(int id) => Bookings.FirstOrDefault(b => b.Id == id);
}
