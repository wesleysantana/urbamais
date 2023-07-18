using Microsoft.AspNetCore.Identity;

namespace Urbamais.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string IdUserCreation { get; set; } = string.Empty;
    public DateTime CreationDate { get; private set; } = DateTime.Now;
    public string? IdUserModification { get; private set; }
    public DateTime? ModificationDate { get; private set; }
    public string? IdUserDeletion { get; private set; }
    public DateTime? DeletionDate { get; private set; }    

    public void Update(string userId)
    {
        ModificationDate = DateTime.Now;
        IdUserModification = userId;
    }

    public void Delete(string userId)
    {
        DeletionDate = DateTime.Now;
        IdUserDeletion = userId;
    }   
}