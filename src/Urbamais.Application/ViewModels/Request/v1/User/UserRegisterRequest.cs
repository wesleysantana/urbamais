using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.User;

public class UserRegisterRequest
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(100, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [EmailAddress(ErrorMessage = DataAnnotationsMessages.EMAIL)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = DataAnnotationsMessages.COMPAREPASSWORD)]
    public string ConfirmationPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 2)]
    public string Role { get; set; } = string.Empty;
}