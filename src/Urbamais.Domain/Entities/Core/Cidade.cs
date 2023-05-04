using System.Reflection;
using Urbamais.Domain.ValueObjects;

namespace Urbamais.Domain.Entities.Core;

public sealed class Cidade : BaseEntity, IAggregateRoot
{
    public NomeVO Nome { get; private set; }
    public Uf Uf { get; private set; }

    public Cidade(NomeVO nome, Uf siglaUf)
    {
        Nome = nome;
        Uf = siglaUf;

        Validar();
    }

    private void Validar()
    {
        ValidationResult.Errors.AddRange(Nome.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Uf.ValidationResult.Errors);

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(NomeVO? nome = null, Uf? siglaUf = null)
    {
        if (nome is not null) Nome = nome;
        if (siglaUf is not null) Uf = siglaUf;

        Validar();
    }

    #region Sobrescrita Object

    public override string ToString() => $"Cidade - Id: {Id}, Nome: {Nome}, Estado: {Uf}";

    public override bool Equals(object? obj)
    {
        return obj is Cidade cidade &&
            Id == cidade.Id &&
            EqualityComparer<NomeVO>.Default.Equals(Nome, cidade.Nome) &&
            EqualityComparer<Uf>.Default.Equals(Uf, cidade.Uf);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nome, Uf);
    }

    public static bool operator ==(Cidade left, Cidade right) => left.Equals(right);

    public static bool operator !=(Cidade left, Cidade right) => !left.Equals(right);

    #endregion Sobrescrita Object
}