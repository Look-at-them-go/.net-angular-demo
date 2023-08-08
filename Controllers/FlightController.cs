using Microsoft.AspNetCore.Mvc;
using _net_angular_demo.ReadModels;
using _net_angular_demo.Dto;
using _net_angular_demo.Domain.Errors;
using _net_angular_demo.Data;
using Microsoft.EntityFrameworkCore;

namespace _net_angular_demo.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{

    private readonly ILogger<FlightController> _logger;

    private readonly Entities entities;

    public FlightController(ILogger<FlightController> logger, Entities e)
    {
        _logger = logger;
        entities = e;
    }

    

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>),200)]
    public IEnumerable<FlightRm> Search(){

        FlightRm[] flightRmList = entities.flights.Select(flight => new FlightRm(
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

        var flight = entities.flights.SingleOrDefault(f => f.Id == id);

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

        var flight = entities.flights.SingleOrDefault(f => f.Id == dto.FlightId);
        if(flight == null){
            return NotFound();
        }

        var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

        if(error is OverbookError){
            return Conflict(new {message = "The number of seats selected exceeds the number of available seats"});
        }
        
        try{
            entities.SaveChanges();
        } catch (DbUpdateConcurrencyException e) {
            return Conflict(new {message = "Error occured while booking"});
        }
        

        return CreatedAtAction(nameof(Find), new {id = dto.FlightId});
    }
}
