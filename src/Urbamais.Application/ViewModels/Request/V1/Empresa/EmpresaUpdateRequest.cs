using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.ViewModels.Request.V1.Empresa;

public class EmpresaUpdateRequest : DomainUpdate
{
    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    public string? NomeFantasia { get; set; }

    [StringLength(255, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 3)]
    public string? RazaoSocial { get; set; }

    [MaxLength(14, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Cnpj { get; set; }

    [MaxLength(20, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public List<Telefone>? Telefones { get; set; } = new();

    [EmailAddress(ErrorMessage = DataAnnotationsMessages.EMAIL)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public List<Email> Emails { get; set; } = new();

    public List<Endereco>? Enderecos { get; set; } = new();
}