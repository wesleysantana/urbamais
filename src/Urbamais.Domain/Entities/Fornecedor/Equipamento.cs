using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Equipamento : BaseEntity, IAggregateRoot
{
    public NomeVO Nome { get; private set; }
    public DescricaoVO Descricao { get; private set; }
    public ICollection<Fornecedor>? Fornecedores { get; private set; }

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
        AddErrorsFrom(Nome);
        AddErrorsFrom(Descricao);       

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

    public void Update(NomeVO? nome = null, DescricaoVO? descricao = null)
    {
        if (nome is not null) Nome = nome;
        if (descricao is not null) Descricao = descricao;

        Validar();
    }    
}