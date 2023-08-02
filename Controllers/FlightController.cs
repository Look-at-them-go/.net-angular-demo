using Microsoft.AspNetCore.Mvc;
using _net_angular_demo.ReadModels;
using _net_angular_demo.Dto;
using _net_angular_demo.Domain.Entities;
using _net_angular_demo.Domain.Errors;

namespace _net_angular_demo.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{

    private readonly ILogger<FlightController> _logger;

    public FlightController(ILogger<FlightController> logger)
    {
        _logger = logger;
    }

    static Random rand = new Random();

    static private Flight[] flights = new Flight[]{
            new (Guid.NewGuid(),
                "American Airlines",
                rand.Next(90,5000).ToString(),
                new TimePlace("Los Angeles", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Istanbul", DateTime.Now.AddHours(rand.Next(4,10))),
                2
            ),
            new (Guid.NewGuid(),
                "Deutsche BA",
                rand.Next(90,5000).ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Schiphol", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "British Airways",
                rand.Next(90,5000).ToString(),
                new TimePlace("London", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Rome", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Basiq Air",
                rand.Next(90,5000).ToString(),
                new TimePlace("Amsterdam", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Glasgow", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "BB Heliag",
                rand.Next(90,5000).ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Baku", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Adria Airways",
                rand.Next(90,5000).ToString(),
                new TimePlace("Ljubljana", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Warsaw", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
    };

    

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>),200)]
    public IEnumerable<FlightRm> Search(){

        FlightRm[] flightRmList = flights.Select(flight => new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(
                flight.Departure.Place.ToString(),
                flight.Departure.Time
            ),
            new TimePlaceRm(
                flight.Arrival.Place.ToString(),
                flight.Arrival.Time
            ),
            flight.remainingSeats
        )).ToArray();

        return flightRmList;
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(FlightRm),200)]
    [HttpGet("{id}")]
    public ActionResult<FlightRm> Find(Guid id){

        var flight = flights.SingleOrDefault(f => f.Id == id);

        if(flight == null){
            return NotFound();
        }

        var readModel = new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(
                flight.Departure.Place.ToString(),
                flight.Departure.Time
            ),
            new TimePlaceRm(
                flight.Arrival.Place.ToString(),
                flight.Arrival.Time
            ),
            flight.remainingSeats
        );

        return Ok(readModel);
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ProducesResponseType(200)]
    public IActionResult Book(BookDto dto){
        Console.WriteLine($"Booking a new flight {dto.FlightId}");

        var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);
        if(flight == null){
            return NotFound();
        }

        var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

        if(error is OverbookError){
            return Conflict(new {message = "The number of seats selected exceeds the number of available seats"});
        }

        return CreatedAtAction(nameof(Find), new {id = dto.FlightId});
    }
}
