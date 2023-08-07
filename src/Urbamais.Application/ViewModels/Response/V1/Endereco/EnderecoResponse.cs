using Urbamais.Application.ViewModels.Response.V1.Cidade;

namespace Urbamais.Application.ViewModels.Response.V1.Endereco;

public class EnderecoResponse
{
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? CodigoPostal { get; set; }
    public CidadeResponse? Cidade { get; set; }
}