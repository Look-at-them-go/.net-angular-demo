namespace _net_angular_demo.ReadModels;


public record BookingRm(
    Guid FlightId,
    string Airline,
    string Price,
    TimeplaceRm Arrival,
    TimeplaceRm Departure,
    int NumberOfBookedSeats,
    string PassengerEmail
);