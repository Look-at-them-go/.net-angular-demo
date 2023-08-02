using Microsoft.AspNetCore.Mvc;
using _net_angular_demo.Dto;
using _net_angular_demo.ReadModels;
using _net_angular_demo.Domain.Entities;
using _net_angular_demo.Data;

namespace net_angular_demo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        private readonly Entities entities;

        public PassengerController(Entities e){
            entities = e;
        }



        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto){
            entities.passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
            ));
            Console.WriteLine(entities.passengers.Count);
            return CreatedAtAction(nameof(Find), new {email= dto.Email});
        }

        
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<PassengerRm> Find(string email){
            
            var passenger = entities.passengers.FirstOrDefault(p => p.Email == email);
            
            if (passenger == null){
                return NotFound();
            }
            var passengerModel = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
            );
            return Ok(passengerModel);
        }
    }
}
