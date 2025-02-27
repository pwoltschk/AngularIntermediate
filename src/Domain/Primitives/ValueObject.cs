﻿namespace Domain.Primitives;

// Source: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return EqualOperator(one, two);
    }

    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return NotEqualOperator(one, two);
    }

    private static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        return !(left is null ^ right is null) && !(!ReferenceEquals(left, right) && !left!.Equals(right!));
    }

    private static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }
}
