using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBackendIDWM.Src.DTOs.User
{
    public class EditUserDto
    {
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(8, ErrorMessage = "El nombre debe tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre debe tener a lo más 255 caracteres.")]
        public string? Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public int? GenderId { get; set; }
    }
}