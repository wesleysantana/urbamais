using Urbamais.Application.ViewModels.Response.V1.Endereco;
using Urbamais.Application.ViewModels.Response.V1.Email;
using Urbamais.Application.ViewModels.Response.V1.Telefone;

namespace Urbamais.Application.ViewModels.Response.V1.Empresa;

public class EmpresaResponse : ValidateViewModel
{
    public ICollection<TelefoneResponse>? Telefones { get; set; }
    public ICollection<EmailResponse>? Emails { get; set; }
    public ICollection<EnderecoResponse>? Enderecos { get; set; }

    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string? InscricaoEstadual { get; set; }
    public string? InscricaoMunicipal { get; set; }
}