using System.ComponentModel.DataAnnotations;

namespace TallerBackendIDWM.Src.Helpers.Validator
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > _maxFileSize)
            {
                double maxFileSizeInMb = _maxFileSize / (1024.0 * 1024.0);
                return new ValidationResult(
                    ErrorMessage ?? $"El tamaño máximo del archivo es {maxFileSizeInMb:F2} MB."
                );
            }
        }

        return ValidationResult.Success;
    }
    }
}