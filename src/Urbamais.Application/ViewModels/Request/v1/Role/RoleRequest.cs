using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.Role;

public class RoleRequest
{ 
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
    public IDictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();    
}