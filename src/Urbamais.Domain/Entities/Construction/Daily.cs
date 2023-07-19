using Core.Domain.Interfaces;
using Core.SeedWork;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Entities.Supplier;

namespace Urbamais.Domain.Entities.Construction;

public class Daily : BaseEntity, IAggregateRoot
{
    private List<FileStream>? _listPhotos = new();

    public int ConstructionId { get; private set; }
    public virtual Construction Construction { get; private set; }
    public DateTime Date { get; private set; }
    public int SupplierId { get; private set; }
    public virtual Supplier.Supplier Supplier { get; private set; }
    public string DescriptionActivities { get; private set; }
    public int CollaboratorId { get; private set; }
    public virtual Collaborator Collaborator { get; private set; }

    public IReadOnlyCollection<FileStream> Fotos
    {
        get => _listPhotos!;
        private set => _listPhotos = value.ToList();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Daily() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Daily(string idUserCreation, Construction construction, DateTime date, Supplier.Supplier supplier,
        string descriptionActivities, Collaborator collaborator, List<FileStream> photos)
    {
        Construction = construction;
        Date = date;
        Supplier = supplier;
        DescriptionActivities = descriptionActivities;
        Collaborator = collaborator;
        Fotos = photos;

        Validate();

        if (IsValid)
        {
            IdUserCreation = idUserCreation;
        }
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Construction.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Supplier.ValidationResult!.Errors);
        ValidationResult?.Errors.AddRange(Collaborator.ValidationResult!.Errors);

        Validate(this, new DailyValidator());

        if (!IsValid && Id == default)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                if (item.Name.Equals(nameof(Fotos)))
                {
                    _listPhotos = default;
                    continue;
                }
                item.SetValue(this, default);
            }
        }
    }

    public void Update(string idUserModification, Construction? construction = null, DateTime? date = null, Supplier.Supplier? supplier = null,
        string? descriptionActivities = null, Collaborator? collaborator = null, List<FileStream>? photos = null)
    {
        if (construction is not null) Construction = construction;
        if (date is not null) Date = (DateTime)date;
        if (supplier is not null) Supplier = supplier;
        if (descriptionActivities is not null) DescriptionActivities = descriptionActivities;
        if (collaborator is not null) Collaborator = collaborator;
        if (photos is not null) Fotos = photos;

        Validate();

        if (IsValid)
        {
            IdUserModification = idUserModification;
            ModificationDate = DateTime.Now;
        }
    }

    #region Sobrescrita Object

    public override string ToString() =>
        $"companie - Id: {Id}, ObraId: {ConstructionId}, Data: {Date}, FornecedorId: {SupplierId}, " +
        $"Descrição das Atividades: {DescriptionActivities}, ColaboradorId: {CollaboratorId}";

    public override bool Equals(object? obj)
    {
        return obj is Daily diario &&
            Id == diario.Id &&
            ConstructionId == diario.ConstructionId &&
            EqualityComparer<Construction>.Default.Equals(Construction, diario.Construction) &&
            Date == diario.Date &&
            SupplierId == diario.SupplierId &&
            EqualityComparer<Supplier.Supplier>.Default.Equals(Supplier, diario.Supplier) &&
            DescriptionActivities == diario.DescriptionActivities &&
            CollaboratorId == diario.CollaboratorId &&
            EqualityComparer<Collaborator>.Default.Equals(Collaborator, diario.Collaborator);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Id);
        hash.Add(ConstructionId);
        hash.Add(Construction);
        hash.Add(Date);
        hash.Add(SupplierId);
        hash.Add(Supplier);
        hash.Add(DescriptionActivities);
        hash.Add(CollaboratorId);
        hash.Add(Collaborator);
        return hash.ToHashCode();
    }

    #endregion Sobrescrita Object

    private class DailyValidator : AbstractValidator<Daily>
    {
        public DailyValidator()
        {
            RuleFor(x => x.Date)
                .Must(date => date != default);

            RuleFor(x => x.DescriptionActivities)
                .NotEmpty();
        }
    }
}