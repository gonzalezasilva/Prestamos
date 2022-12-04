using System.ComponentModel.DataAnnotations;

namespace WebApplicationPrestamos.Models
{
    public class ThingsDto
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }
    
}
}
