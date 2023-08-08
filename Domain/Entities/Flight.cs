using _net_angular_demo.Domain.Errors;

namespace _net_angular_demo.Domain.Entities;

public class Flight{

    
    public Guid Id {get; set;}
    public string Airline {get; set;}
    public string Price {get; set;}
    public TimePlace Departure {get; set;}
    public TimePlace Arrival {get; set;}
    public int remainingSeats {get; set;}

    public IList<Booking> bookings = new List<Booking>();

    public Flight(){}
    public Flight(
        Guid id,
        string airline,
        string price,
        TimePlace departure,
        TimePlace arrival,
        int remaining_Seats
    ){
        Id = id;
        Airline = airline;
        Price = price;
        Departure = departure;
        Arrival = arrival;
        remainingSeats = remaining_Seats;
    }


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

    public object? CancelBooking(string passengerEmail, byte numberOfSeats){

        var booking = bookings.FirstOrDefault(b => numberOfSeats == b.NumberOfSeats && passengerEmail.ToLower() == b.PassengerEmail.ToLower());
    
        if(booking == null){
            return new NotFoundError();
        }

        bookings.Remove(booking);
        remainingSeats += booking.NumberOfSeats;

        return null;
    }
}