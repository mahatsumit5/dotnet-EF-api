namespace DotnetApi.dtos;

public record class UserToAddDto(
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    string Gender,
    bool Active
);
