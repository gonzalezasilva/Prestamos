
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPrestamos.Entities
{
    public class Loan: EntityBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int ThingId { get; set; }
        public Thing Thing { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public  DateTime ReturnDate { get; set; } = DateTime.UtcNow;


    }
}
