using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(ThingsContext context)
            : base(context)
        {
        }
    }
}
