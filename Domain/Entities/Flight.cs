using _net_angular_demo.Domain.Errors;

namespace _net_angular_demo.Domain.Entities;

public record Flight(
    Guid Id,
    string Airline,
    string Price,
    TimePlace Departure,
    TimePlace Arrival,
    int remainingSeats
){
    public IList<Booking> bookings = new List<Booking>();
    public int remainingSeats {get; set;} = remainingSeats;

    public object? MakeBooking(string passengerEmail, byte numberOfSeats){
        
        if(this.remainingSeats < numberOfSeats){
            return new OverbookError();
        }

        this.bookings.Add(
            new Booking(
                passengerEmail,
                numberOfSeats
            )
        );

        this.remainingSeats -= numberOfSeats;
        return null;
    }
}