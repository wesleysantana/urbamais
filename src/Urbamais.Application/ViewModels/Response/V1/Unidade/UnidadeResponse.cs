namespace Urbamais.Application.ViewModels.Response.V1.Unidade;

public class UnidadeResponse : ValidateViewModel
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public string? Sigla { get; set; }   
}