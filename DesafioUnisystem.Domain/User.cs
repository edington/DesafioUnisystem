namespace DesafioUnisystem.Domain;

public sealed class User : Entity
{
    public required string Name { get; init; }
    public required Email Email { get; init; }
    public required Password Password { get; init; }
}
