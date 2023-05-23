namespace Core.SeedWork;

public abstract class BaseEntity : BaseValidate
{
    public int Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataAlteracao { get; protected set; }
    public DateTime? DataExclusao { get; protected set; }

    public void Delete() => DataExclusao = DateTime.Now;
}