namespace DesafioUnisystem.ApplicationService.Dtos;

public sealed record UsersGetDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}
