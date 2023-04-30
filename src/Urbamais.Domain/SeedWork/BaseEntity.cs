using Urbamais.Domain.SeedWork;

namespace Urbamais.Domain.Entities;

public abstract class BaseEntity : BaseValidate
{
    public int Id { get; protected set; }
}