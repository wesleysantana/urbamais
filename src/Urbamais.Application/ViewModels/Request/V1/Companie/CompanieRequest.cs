using Core.Constants;
using System.ComponentModel.DataAnnotations;
using Urbamais.Domain.Entities.EntitiesOfCore;

namespace Urbamais.Application.ViewModels.Request.V1.Companie;

public class CompanieRequest : RequestBase
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(20, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public List<Phone> Phones { get; set; } = new();

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [EmailAddress(ErrorMessage = DataAnnotationsMessages.EMAIL)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public List<Email> Emails { get; set; } = new();

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    public List<Address> Addresses { get; set; } = new();

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? TradeName { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(255, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? CorporateName { get; set; }

    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [MaxLength(14, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? Cnpj { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? StateRegistration { get; set; }

    [MaxLength(50, ErrorMessage = DataAnnotationsMessages.MAXLENGHT)]
    public string? MunicipalRegistration { get; set; }
}