using System.ComponentModel.DataAnnotations;

namespace PARCIAL1D.utils
{
    public class SillasValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var sillas = (int)value;
            if (sillas > 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("El número de sillas debe ser mayor a 0");
            }
        }
    }
}
