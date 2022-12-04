using System.ComponentModel.DataAnnotations;

namespace WebApplicationPrestamos.Entities
{
    public class Thing : EntityBase
    { 
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La descripción solo puede tener 100 caracteres.")] 
        public string Description { get; set; }


        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public Category Category { get; set; }

        [Required(ErrorMessage = "El objeto debe pertenecer a una categoría.")]
        public int CategoryId { get; set; }
    }
}
