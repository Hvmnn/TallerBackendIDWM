using System.ComponentModel.DataAnnotations;
using TallerBackendIDWM.Src.Helpers.Validator;

namespace TallerBackendIDWM.Src.DTOs.Product{
    public class CreateProductDto
    {
        [StringLength(64, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 64 caracteres")]
        public required string Name { get; set; }

        [RegularExpression(@"Poleras|Gorros|Jugueteria|Alimentacion|Libros")]
        public required string Type { get; set; }

        [Range(0, 100000000, ErrorMessage = "El precio debe estar entre 0 y 100000000")]
        public required decimal Price { get; set; }

        [Range(0, 100000, ErrorMessage = "El stock debe estar entre 0 y 100000")]
        public required int Stock { get; set; }

        [AllowedExtensionsAndMimeTypes(new[] { ".png", ".jpg" }, new[] { "image/png", "image/jpeg" })]
        [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "El tamaño máximo del archivo es 10 MB")]
        public required IFormFile? Image { get; set; }

    }
}