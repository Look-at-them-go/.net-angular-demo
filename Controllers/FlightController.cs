using Microsoft.AspNetCore.Mvc;
using _net_angular_demo.ReadModels;
using _net_angular_demo.Dto;

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

    static private FlightRm[] flights = new FlightRm[]{
            new (Guid.NewGuid(),
                "American Airlines",
                rand.Next(90,5000).ToString(),
                new TimePlaceRm("Los Angeles", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlaceRm("Istanbul", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Deutsche BA",
                rand.Next(90,5000).ToString(),
                new TimePlaceRm("Munchen", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlaceRm("Schiphol", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "British Airways",
                rand.Next(90,5000).ToString(),
                new TimePlaceRm("London", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlaceRm("Rome", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Basiq Air",
                rand.Next(90,5000).ToString(),
                new TimePlaceRm("Amsterdam", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlaceRm("Glasgow", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "BB Heliag",
                rand.Next(90,5000).ToString(),
                new TimePlaceRm("Zurich", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlaceRm("Baku", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Adria Airways",
                rand.Next(90,5000).ToString(),
                new TimePlaceRm("Ljubljana", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlaceRm("Warsaw", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
    };

    static private IList<BookDto> bookings = new List<BookDto>();

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>),200)]
    public IEnumerable<FlightRm> Search(){
        return flights;
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

        return Ok(flight);
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ProducesResponseType(200)]
    public IActionResult Book(BookDto dto){
        Console.WriteLine($"Booking a new flight {dto.FlightId}");

        var flightFound = flights.Any(f => f.Id == dto.FlightId);
        if(flightFound == false){
            return NotFound();
        }

        bookings.Add(dto);
        return CreatedAtAction(nameof(Find), new {id = dto.FlightId});
    }
}
