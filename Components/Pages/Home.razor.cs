using Microsoft.AspNetCore.Components;
using RoomBookingSystem.Models;
using RoomBookingSystem.Service;

namespace RoomBookingSystem.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] public BookingService BookingService { get; set; } = default!;

    public int? SelectedRoomId { get; set; }
    public IEnumerable<Booking> FilteredBookings => BookingService.GetBookingsByRoom(SelectedRoomId);

    protected override void OnInitialized()
    {
        // Any async init logic (optional)
    }

    public void DeleteBooking(int id)
    {
        BookingService.DeleteBooking(id);
        StateHasChanged();
    }
}
