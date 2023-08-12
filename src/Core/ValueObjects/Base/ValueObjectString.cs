namespace Core.ValueObjects.Base;

public class ValueObjectString : ValueObjectBase
{
    public string? Value { get; protected set; }

    internal object CreateMemento()
    {
        return new
        {
            Value
        };
    }

    internal void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Value = state.Valor;
    }
}