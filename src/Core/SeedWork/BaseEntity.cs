namespace Core.SeedWork;

public abstract class BaseEntity : BaseValidate
{
    public int Id { get; protected set; }
    public DateTime CreationDate { get; protected set; } = DateTime.Now;
    public DateTime? ModificationDate { get; protected set; }
    public DateTime? DeletionDate { get; protected set; }

    public void Delete() => DeletionDate = DateTime.Now;
}