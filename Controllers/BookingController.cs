using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _net_angular_demo.Domain.Entities;
using _net_angular_demo.Data;
using _net_angular_demo.ReadModels;

namespace _net_angular_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly Entities entities;

        public BookingController(Entities e){
            entities = e;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>),200)]
        public ActionResult<IEnumerable<BookingRm>> List(string email){

            IEnumerable<BookingRm> bookings = entities.flights.ToArray()
                                                .SelectMany(f => f.bookings
                                                    .Where(b => b.PassengerEmail == email)
                                                        .Select(b => new BookingRm(
                                                            f.Id,
                                                            f.Airline,
                                                            f.Price.ToString(),
                                                            new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                                                            new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                                                            b.NumberOfSeats,
                                                            email
                                                        )));
            return Ok(bookings);                                               
        }

    }
}
