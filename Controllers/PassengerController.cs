using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _net_angular_demo.Dto;
using _net_angular_demo.ReadModels;

namespace net_angular_demo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        static private IList<NewPassengerDto> passengers = new List<NewPassengerDto>();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto){
            passengers.Add(dto);
            Console.WriteLine(passengers.Count);
            return CreatedAtAction(nameof(Find), new {email= dto.Email});
        }

        
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<PassengerRm> Find(string email){
            
            var passenger = passengers.FirstOrDefault(p => p.Email == email);
            
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
