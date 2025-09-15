using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Fornecedor;

namespace Urbamais.Domain.Entities.Obra;

public class Diario : BaseEntity, IAggregateRoot
{
    private List<FileStream>? _listFotos = new();

    public int ObraId { get; private set; }
    public virtual Obra Obra { get; private set; }
    public DateTime Data { get; private set; }
    public int FornecedorId { get; private set; }
    public virtual Fornecedor.Fornecedor Fornecedor { get; private set; }
    public string DescricaoAtividades { get; private set; }
    public int ColaboradorId { get; private set; }
    public virtual Colaborador Colaborador { get; private set; }

    public IReadOnlyCollection<FileStream> Fotos
    {
        get => _listFotos!;
        private set => _listFotos = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Diario() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Diario(Obra obra, DateTime data, Fornecedor.Fornecedor fornecedor,
        string descricaoAtividades, Colaborador colaborador, List<FileStream> fotos)
    {
        Obra = obra;
        Data = data;
        Fornecedor = fornecedor;
        DescricaoAtividades = descricaoAtividades;
        Colaborador = colaborador;
        Fotos = fotos;

        Validar();
    }

    private void Validar()
    {
        //ValidationResult?.Errors.AddRange(Obra.ValidationResult!.Errors);
        //ValidationResult?.Errors.AddRange(Fornecedor.ValidationResult!.Errors);
        //ValidationResult?.Errors.AddRange(Colaborador.ValidationResult!.Errors);

        //Validate(this, new DiarioValidator());

        //if (!IsValid && Id == default)
        //{
        //    var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        //    foreach (var item in propriedades)
        //    {
        //        if (item.Name.Equals(nameof(Fotos)))
        //        {
        //            _listFotos = default;
        //            continue;
        //        }
        //        item.SetValue(this, default);
        //    }
        //}

        Validate(this, new DiarioValidator());
        AddErrorsFrom(Obra);
        AddErrorsFrom(Fornecedor);
        AddErrorsFrom(Colaborador);

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var p in propriedades)
                p.SetValue(this, default);
        }
    }

    public void Update(Obra? obra = null, DateTime? data = null, Fornecedor.Fornecedor? fornecedor = null,
        string? descricaoAtividades = null, Colaborador? colaborador = null, List<FileStream>? fotos = null)
    {
        if (obra is not null) Obra = obra;
        if (data is not null) Data = (DateTime)data;
        if (fornecedor is not null) Fornecedor = fornecedor;
        if (descricaoAtividades is not null) DescricaoAtividades = descricaoAtividades;
        if (colaborador is not null) Colaborador = colaborador;
        if (fotos is not null) Fotos = fotos;

        Validar();
    }

    private class DiarioValidator : AbstractValidator<Diario>
    {
        public DiarioValidator()
        {
            RuleFor(x => x.Data)
                .Must(date => date != default);

            RuleFor(x => x.DescricaoAtividades)
                .NotEmpty();
        }
    }
}