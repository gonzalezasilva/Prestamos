
namespace WebApplicationPrestamos.Entities
{
    public class Loan: EntityBase
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Thing Thing { get; set; }
        public Person Person { get; set; }
    }
}
