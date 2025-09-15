using Core.Domain.Interfaces;

namespace Core.SeedWork;


public abstract class BaseEntity : BaseValidate, IEntity
{
    public int Id { get; protected set; }

    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public DateTime? DataExclusao { get; protected set; }

    public void Delete()
    {
        DataExclusao = DateTime.UtcNow;
    }

    public void Restore()
    {
        DataExclusao = null;
    }
}