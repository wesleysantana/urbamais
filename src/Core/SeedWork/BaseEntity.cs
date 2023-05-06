namespace Core.SeedWork;

public abstract class BaseEntity : BaseValidate
{
    public int Id { get; protected set; }
}