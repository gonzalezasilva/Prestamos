using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess

{
    public class PersonRepository: GenericRepository<Person>, IPersonRepository
    { 
        public PersonRepository(ThingsContext context)
            : base(context)
        {
        }
    }
}
