
namespace _net_angular_demo.ReadModels;

public record FlightRm(
    Guid Id,
    string Airline,
    string Price,
    TimePlaceRm Departure,
    TimePlaceRm Arrival,
    int remainingSeats
);