using System.Reflection;
using Urbamais.Domain.ValueObjects;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Equipamento : BaseEntity, IAggregateRoot
{
    public NomeVO Nome { get; set; }
    public DescricaoVO Descricao { get; private set; }

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

        if (!IsValid)
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

    #endregion Sobrescrita Object
}