using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.Usuario;

public class UsuarioLoginRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [MaxLength(15, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string Senha { get; set; } = string.Empty;
}