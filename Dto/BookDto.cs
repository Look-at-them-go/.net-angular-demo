using System.ComponentModel.DataAnnotations;

namespace _net_angular_demo.Dto;

public record BookDto(
    [Required]
    Guid FlightId,

    [Required] [EmailAddress] [StringLength(100, MinimumLength = 3)]
    string PassengerEmail,
    
    [Required] [Range(1,254)]
    byte NumberOfSeats
);