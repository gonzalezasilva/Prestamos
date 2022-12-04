using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public class ThingRepository : GenericRepository<Thing>, IThingRepository
    {
        public ThingRepository(ThingsContext context) 
            : base(context) 
        {
        }
    }
}
