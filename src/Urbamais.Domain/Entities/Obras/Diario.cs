using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Fornecedores;

namespace Urbamais.Domain.Entities.Obras;

public class Diario : BaseEntity, IAggregateRoot
{
    private List<FileStream>? _listFotos = new();

    public int ObraId { get; private set; }
    public virtual Obra Obra { get; private set; }
    public DateTime Data { get; private set; }
    public int FornecedorId { get; private set; }
    public virtual Fornecedor Fornecedor { get; private set; }
    public string DescricaoAtividade { get; private set; }
    public int ColaboradorId { get; private set; }
    public virtual Colaborador Colaborador { get; private set; }

    public IReadOnlyCollection<FileStream> Fotos
    {
        get => _listFotos!;
        private set => _listFotos = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Diario()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Diario(string idUserCreation, Obra obra, DateTime data, Fornecedores.Fornecedor fornecedor,
        string descricaoAtividade, Colaborador colaborador, List<FileStream> fotos)
    {
        Obra = obra;
        Data = data;
        Fornecedor = fornecedor;
        DescricaoAtividade = descricaoAtividade;
        Colaborador = colaborador;
        Fotos = fotos;

        Validate(this, new DiarioValidator());

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                if (item.Name.Equals(nameof(Fotos)))
                {
                    _listFotos = default;
                    continue;
                }
                item.SetValue(this, default);
            }
        }
        else
            IdUserCreation = idUserCreation;
    }    

    public void Update(string idUserModification, Obra? obra = null, DateTime? data = null, Fornecedores.Fornecedor? fornecedor = null,
        string? descricaoAtividade = null, Colaborador? colaborador = null, List<FileStream>? fotos = null)
    {
        var memento = CreateMemento();

        if (obra is not null) Obra = obra;
        if (data is not null) Data = (DateTime)data;
        if (fornecedor is not null) Fornecedor = fornecedor;
        if (descricaoAtividade is not null) DescricaoAtividade = descricaoAtividade;
        if (colaborador is not null) Colaborador = colaborador;
        if (fotos is not null) Fotos = fotos;

        Validate(this, new DiarioValidator());

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
        else
            RestoreMemento(memento);
    }

    #region Memento

    private object CreateMemento()
    {
        return new
        {
            ObraId,
            Data,
            FornecedorId,
            DescricaoAtividade,
            ColaboradorId
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        ObraId = state.ConstructionId;
        Data = state.Date;
        FornecedorId = state.SupplierId;
        DescricaoAtividade = state.DescriptionActivities;
        ColaboradorId = state.CollaboratorId;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() =>
        $"companie - Id: {Id}, ObraId: {ObraId}, Data: {Data}, FornecedorId: {FornecedorId}, " +
        $"Descrição das Atividades: {DescricaoAtividade}, ColaboradorId: {ColaboradorId}";

    public override bool Equals(object? obj)
    {
        return obj is Diario diario &&
            Id == diario.Id &&
            ObraId == diario.ObraId &&
            EqualityComparer<Obra>.Default.Equals(Obra, diario.Obra) &&
            Data == diario.Data &&
            FornecedorId == diario.FornecedorId &&
            EqualityComparer<Fornecedores.Fornecedor>.Default.Equals(Fornecedor, diario.Fornecedor) &&
            DescricaoAtividade == diario.DescricaoAtividade &&
            ColaboradorId == diario.ColaboradorId &&
            EqualityComparer<Colaborador>.Default.Equals(Colaborador, diario.Colaborador);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Id);
        hash.Add(ObraId);
        hash.Add(Obra);
        hash.Add(Data);
        hash.Add(FornecedorId);
        hash.Add(Fornecedor);
        hash.Add(DescricaoAtividade);
        hash.Add(ColaboradorId);
        hash.Add(Colaborador);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class DiarioValidator : AbstractValidator<Diario>
    {
        public DiarioValidator()
        {
            RuleFor(x => x.Data)
                .Must(date => date != default);

            RuleFor(x => x.DescricaoAtividade)
                .NotEmpty();
        }
    }
}