using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.User;

public class UserLoginRequest
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [EmailAddress(ErrorMessage = DataAnnotationsMessages.EMAIL)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(15, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string Password { get; set; } = string.Empty;
}