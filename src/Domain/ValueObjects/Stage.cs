using Domain.Primitives;

namespace Domain.ValueObjects;

public class Stage : ValueObject
{
    public static Stage Planned => new(0, "Planned");
    public static Stage InProgress => new(1, "In Progress");
    public static Stage Completed => new(2, "Completed");

    public int Id { get; }
    public string Name { get; }

    private Stage(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Stage FromId(int id)
    {
        return id switch
        {
            0 => Planned,
            1 => InProgress,
            2 => Completed,
            _ => throw new ArgumentException($"Unknown stage ID: {id}.", nameof(id))
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public override string ToString() => Name;
}
