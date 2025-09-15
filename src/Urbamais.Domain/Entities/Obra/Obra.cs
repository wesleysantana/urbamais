using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using System.Reflection;

namespace Urbamais.Domain.Entities.Obra;

public class Obra : BaseEntity, IAggregateRoot
{
    public int EmpresaId { get; private set; }
    public virtual Empresa Empresa { get; private set; }
    public DescricaoVO Descricao { get; private set; }
    public virtual ICollection<Planejamento.Planejamento>? Planejamentos { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Obra()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Obra(Empresa empresa, DescricaoVO descricao)
    {
        Empresa = empresa;
        Descricao = descricao;

        Validar();
    }

    private void Validar()
    {
        //ValidationResult?.Errors.AddRange(Empresa.ValidationResult!.Errors);

        //if (!IsValid && Id == default)
        //{
        //    var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        //    foreach (var item in propriedades)
        //    {
        //        item.SetValue(this, default);
        //    }
        //}
        
        AddErrorsFrom(Empresa);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

    public void Update(DescricaoVO descricao)
    {
        Descricao = descricao;
        Validar();
    }
}