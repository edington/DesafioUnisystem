﻿namespace DesafioUnisystem.ApplicationService.Dtos;

public sealed record UserAddDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
