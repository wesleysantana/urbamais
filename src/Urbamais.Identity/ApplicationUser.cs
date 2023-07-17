using Microsoft.AspNetCore.Identity;

namespace Urbamais.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string IdUserCreation { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string? IdUserModification { get; set; }
    public DateTime? ModificationDate { get; set; }
    public string? IdUserDeletion { get; set; }
    public DateTime? DeletionDate { get; set; }

    public void Delete(string userId)
    {
        DeletionDate = DateTime.Now;
        IdUserDeletion = userId;
    }
}