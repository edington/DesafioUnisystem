using System.Security.Cryptography;

namespace DesafioUnisystem.Domain.Entities;

public sealed record Password
{
    private const int SaltSize = 16; 
    private const int KeySize = 32;
    private const int Iterations = 100_000;
    private const char Delimiter = ';';

    public string Hash { get; init; }

    protected Password() { }

    private Password(string password)
    {
        Hash = GenerateHash(password);
    }

    public static Result<Password> TryCreate(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result<Password>.Fail("Favor inserir uma senha.");

        if (password.Length < 5 || password.Length > 35)
            return Result<Password>.Fail("A senha tem que ter no mínimo 5 e máximo 35 caracteres.");

        return Result<Password>.Ok(new Password(password));
    }
    public bool Check(string password)
    {
        var parts = Hash.Split(Delimiter);
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);

        var keyToCheck = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256).GetBytes(KeySize);

        return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
    }

    private string GenerateHash(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256).GetBytes(KeySize);

        return $"{Convert.ToBase64String(salt)}{Delimiter}{Convert.ToBase64String(key)}";
    }
}
