using Microsoft.AspNetCore.Components;
using RoomBookingSystem.Models;
using RoomBookingSystem.Service;

namespace RoomBookingSystem.Components.Pages
{

public partial class CreateBooking : ComponentBase
{
    [Inject] public BookingService BookingService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    public Booking Booking { get; set; } = new()
    {
        StartTime = DateTime.Now,
        EndTime = DateTime.Now.AddHours(1)
    };

    public List<Room> Rooms { get; set; } = [];
    public string ErrorMessage { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        Rooms = BookingService.Rooms;
    }

    public void HandleBooking()
    {
        if (BookingService.AddBooking(Booking, out var error))
        {
            Navigation.NavigateTo("/");
        }
        else
        {
            ErrorMessage = error;
        }
    }
}
}