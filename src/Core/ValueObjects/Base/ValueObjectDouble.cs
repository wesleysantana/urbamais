namespace Core.ValueObjects.Base;

public class ValueObjectDouble : ValueObjectBase
{
    public double? Value { get; protected set; }

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