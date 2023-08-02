using Core.SeedWork;

namespace Core.ValueObjects;

public abstract class ValueObjectBase : BaseValidate
{
    public string? Value { get; protected set; }
}