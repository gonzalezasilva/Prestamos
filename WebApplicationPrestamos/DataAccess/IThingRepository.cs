using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public interface IThingRepository : IGenericRepository<Thing>
    {
        //Aca podriamos tener otros metodos relacionados solo a thing
        //GetByCategogyId(int categoryId) -> Por Ejemplo
    }
}
