namespace _net_angular_demo.Dto;

public record NewPassengerDto(
    string Email,
    string FirstName,
    string LastName,
    bool Gender
);