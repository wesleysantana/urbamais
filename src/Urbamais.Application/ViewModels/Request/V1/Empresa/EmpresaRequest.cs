using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.ViewModels.Request.V1.Empresa;

public class EmpresaRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(20, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public List<Telefone> Telefones { get; set; } = new();

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [EmailAddress(ErrorMessage = DataAnnotationsMessages.EMAIL)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public List<Email> Emails { get; set; } = new();

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public List<Endereco> Enderecos { get; set; } = new();

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? NomeFantasia { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? RazaoSocial { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(14, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Cnpj { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? InscricaoEstadual { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? InscricaoMunicipal { get; set; }
}