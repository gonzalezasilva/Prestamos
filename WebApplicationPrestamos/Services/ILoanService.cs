using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.Services
{
    public interface ILoanService
    {
       
     
            Loan GetById(int id);

            void Return(int id);

       
  
    }
}
