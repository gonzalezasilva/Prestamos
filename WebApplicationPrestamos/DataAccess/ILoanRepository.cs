using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        //Aca podriamos tener otros metodos relacionados solo a thing
        //GetByCategogyId(int categoryId) -> Por Ejemplo
    }
}

