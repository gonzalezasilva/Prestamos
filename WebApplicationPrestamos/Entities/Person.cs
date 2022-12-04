namespace WebApplicationPrestamos.Entities
{
    public class Person: EntityBase
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    }
}
