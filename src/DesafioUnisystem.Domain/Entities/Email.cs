using System.Text.RegularExpressions;

namespace DesafioUnisystem.Domain.Entities;

public sealed record Email
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Address { get; init; }

    private Email(string address)
    {
        Address = address.ToLowerInvariant();
    }

    public static Result<Email> TryCreate(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return Result<Email>.Fail("E-mail não pode ser vazio.");

        if (!EmailRegex.IsMatch(address))
            return Result<Email>.Fail("E-mail em formato inválido.");

        return Result<Email>.Ok(new Email(address));
    }
}
