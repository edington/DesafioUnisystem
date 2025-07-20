namespace DesafioUnisystem.Domain;

public class Entity
{
    public required Guid Id { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

}
