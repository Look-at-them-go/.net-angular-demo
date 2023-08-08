namespace _net_angular_demo.ReadModels;


public record BookingRm(
    Guid FlightId,
    string Airline,
    string Price,
    TimePlaceRm Arrival,
    TimePlaceRm Departure,
    int NumberOfBookedSeats,
    string PassengerEmail
);