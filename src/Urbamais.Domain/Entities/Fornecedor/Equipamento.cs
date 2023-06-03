using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Equipamento : BaseEntity, IAggregateRoot
{
    public NomeVO Nome { get; private set; }
    public DescricaoVO Descricao { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Equipamento()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Equipamento(NomeVO nome, DescricaoVO descricao)
    {
        Nome = nome;
        Descricao = descricao;

        Validar();
    }

    private void Validar()
    {
        ValidationResult.Errors.AddRange(Nome.ValidationResult.Errors);
        ValidationResult.Errors.AddRange(Descricao.ValidationResult.Errors);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
    }

    public void Update(NomeVO? nome = null, DescricaoVO? descricao = null)
    {
        if (nome is not null) Nome = nome;
        if (descricao is not null) Descricao = descricao;

        Validar();
    }

    #region Sobrescrita Object

    public override string ToString() => $"Equipamento - Id: {Id}, Nome: {Nome}, Descrição: {Descricao}";

    public override bool Equals(object? obj)
    {
        return obj is Equipamento equipamento &&
            Id == equipamento.Id &&
            EqualityComparer<NomeVO>.Default.Equals(Nome, equipamento.Nome) &&
            EqualityComparer<DescricaoVO>.Default.Equals(Descricao, equipamento.Descricao);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nome, Descricao);
    }

    public static bool operator ==(Equipamento left, Equipamento right) => left.Equals(right);

    public static bool operator !=(Equipamento left, Equipamento right) => !left.Equals(right);

    #endregion Sobrescrita Object
}