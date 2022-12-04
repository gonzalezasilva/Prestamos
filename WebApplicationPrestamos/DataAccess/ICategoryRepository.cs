using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        Category GetByDescription(string description);
    }
}
