using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Urbamais.Application.ViewModels.Request.v1.Role;

public class RoleRequest
{
    [Required(ErrorMessage = DataAnnotationsMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationsMessages.STRINGLENGHT, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
    public IDictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();

    public class ClaimsDTO
    {
        [Required]
        [RegularExpression("^(C|R|U|D)$", ErrorMessage = "A valor deve ser 'C', 'R', 'U' ou 'D'.")]
        public string? Value { get; set; }
    }

    public class ItemValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var item = (RoleRequest)validationContext.ObjectInstance;

            if (item.Claims != null && item.Claims.ContainsKey("value"))
            {
                var operationValue = item.Claims["value"];

                var validationContextItem = new ValidationContext(item) { MemberName = "Value[value]" };
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateProperty(operationValue, validationContextItem, validationResults))
                {
                    var errorMessages = validationResults.Select(r => r.ErrorMessage);
                    return new ValidationResult(string.Join(" ", errorMessages));
                }
            }

            return ValidationResult.Success!;
        }
    }
}