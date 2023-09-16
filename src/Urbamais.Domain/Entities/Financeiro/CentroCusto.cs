using Core.Domain.Interfaces;
using Core.SeedWork;
using Core.ValueObjects;
using FluentValidation;
using System.Reflection;
using Urbamais.Domain.Constants;

namespace Urbamais.Domain.Entities.Financeiro;

public class CentroCusto : BaseEntity, IAggregateRoot
{
    public int Reduzido { get; private set; }
    public Descricao Descricao { get; private set; }
    public Natureza Natureza { get; private set; }
    public long Extenso { get; private set; }
    public virtual ICollection<RegistroFinanceiro>? RegistrosFinanceirosCentroCusto { get; private set; }
    public virtual ICollection<RegistroFinanceiro> RegistrosFinanceirosClasseFinanceira { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected CentroCusto()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CentroCusto(string idUserCreation, int reduzido, Descricao descricao, Natureza natureza, long extenso)
    {
        Reduzido = reduzido;
        Descricao = descricao;
        Natureza = natureza;
        Extenso = extenso;

        Validate();

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {
                item.SetValue(this, default);
            }
        }
        else
            IdUserCreation = idUserCreation;
    }

    private void Validate()
    {
        ValidationResult?.Errors.AddRange(Descricao.ValidationResult!.Errors);

        _ = Validate(this, new CentroCustoValidator());
    }

    public void Update(string idUserModification, int? reduzido = null, Descricao? descricao = null,
        Natureza? natureza = null, long? extenso = null)
    {
        var memento = CreateMemento();

        if (reduzido is not null) Reduzido = (int)reduzido;
        if (descricao is not null) Descricao = descricao;
        if (natureza is not null) Natureza = (Natureza)natureza;
        if (extenso is not null) Extenso = (long)extenso!;

        Validate();

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
            Reduzido,
            Descricao,
            Natureza,
            Extenso
        };
    }

    private void RestoreMemento(object memento)
    {
        if (memento is null) return;

        var state = (dynamic)memento;

        Reduzido = state.Reduzido;
        Descricao = state.Descricao;
        Natureza = state.Natureza;
        Extenso = state.Extenso;
    }

    #endregion Memento

    #region Sobrescrita Object

    public override string ToString() =>
        $"Centro de Custo - Id: {Id}, Descrição: {Descricao}, Reduzido: {Reduzido}, Natureza: {Natureza}, Extenso: {Extenso}";

    public override bool Equals(object? obj)
    {
        return obj is CentroCusto custo &&
            Id == custo.Id &&
            Reduzido == custo.Reduzido &&
            Descricao == custo.Descricao &&
            Natureza == custo.Natureza &&
            Extenso == custo.Extenso;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Reduzido, Descricao, Natureza, Extenso);
    }

    public static bool operator ==(CentroCusto left, CentroCusto right) => left.Equals(right);

    public static bool operator !=(CentroCusto left, CentroCusto right) => !left.Equals(right);

    #endregion Sobrescrita Object

    private class CentroCustoValidator : AbstractValidator<CentroCusto>
    {
        public CentroCustoValidator()
        {
            RuleFor(x => x.Reduzido)
                .NotNull();

            RuleFor(x => x.Natureza)
                .IsInEnum();

            RuleFor(x => x.Extenso)
                .GreaterThan(0);
        }
    }
}