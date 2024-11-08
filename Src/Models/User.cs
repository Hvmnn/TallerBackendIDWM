using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace tallerBackendIDWM.Src.Models
{
    public class User
    {
        public int Id { get ; set; }
        public required string Rut { get; set; }

        [StringLength(255, MinimumLength = 8, ErrorMessage = "El nombre debe tener entre 8 y 255 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener caracteres alfabéticos en español")]
        public required string Name { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), nameof(FechaNacimientoValida))]
        public required DateTime Birthdate { get; set; }

        [EmailAddress(ErrorMessage = "El correp electrónico no es válido")]
        public required string Email { get; set; }

        [RegularExpression(@"Masculino|Femenino|Prefiero no decirlo|Otro", ErrorMessage = "El género debe ser Masculino, Femenino, Prefiero no decirlo u Otro")]
        public required string Gender { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "La contraseña solo puede contener caracteres alfanuméricos")]
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string Rol { get; set; }


        public static ValidationResult FechaNacimientoValida(DateTime fechaNacimiento, ValidationContext context)
    {
        return fechaNacimiento < DateTime.Today
            ? ValidationResult.Success!
            : new ValidationResult("La fecha de nacimiento debe ser anterior a la fecha actual.");
    }
    }
}