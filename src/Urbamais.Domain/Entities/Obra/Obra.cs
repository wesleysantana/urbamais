using System.Reflection;

namespace Urbamais.Domain.Entities.Obra;

public class Obra : BaseEntity, IAggregateRoot
{
    public int EmpresaId { get; private set; }
    public Empresa Empresa { get; private set; }
    public string Descricao { get; private set; }

    public Obra(Empresa empresa, string descricao)
    {
        Empresa = empresa;
        Descricao = descricao;

        Validar();
    }

    private void Validar()
    {
        ValidationResult.Errors.AddRange(Empresa.ValidationResult.Errors);

        if (!IsValid)
        {
            var propriedades = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var item in propriedades)
            {                
                item.SetValue(this, default);
            }
        }
    }

    public void Update(string descricao)
    {
        Descricao = descricao.Trim();
        Validar();
    }

    #region Sobrescrita Object

    public override bool Equals(object? obj)
    {
        return obj is Obra obra &&
            Id == obra.Id &&
            Descricao == obra.Descricao &&
            EmpresaId == obra.EmpresaId &&
            EqualityComparer<Empresa>.Default.Equals(Empresa, obra.Empresa);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Descricao, EmpresaId, Empresa);
    }

    public static bool operator ==(Obra left, Obra right) => left.Equals(right);

    public static bool operator !=(Obra left, Obra right) => !left.Equals(right);

    #endregion Sobrescrita Object
}