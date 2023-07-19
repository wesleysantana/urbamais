namespace Core.SeedWork;

public abstract class BaseEntity : BaseValidate
{
    public int Id { get; protected set; }
    public string IdUserCreation { get; protected set; } = string.Empty;
    public DateTime CreationDate { get; init; } = DateTime.Now;
    public string? IdUserModification { get; protected set; }
    public DateTime? ModificationDate { get; protected set; }
    public string? IdUserDeletion { get; protected set; }
    public DateTime? DeletionDate { get; protected set; }

    public void Delete(string idUserDeletion)
    {
        IdUserDeletion = idUserDeletion;
        DeletionDate = DateTime.Now;
    }
}