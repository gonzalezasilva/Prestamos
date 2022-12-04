using Microsoft.AspNetCore.Mvc;
using WebApplicationPrestamos.Entities;
using WebApplicationPrestamos.DataAccess;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationPrestamos.Services
{
    public class LoanService : ILoanService
    {

        private readonly IUnitOfWork uow;

        public LoanService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Loan GetById(int id)
        {
            return uow.LoanRepository.GetById(id);
        }

    
        public void Return(int id)
        {
            var dbLoan = GetById(id);
            dbLoan.ReturnDate = DateTime.UtcNow;
            uow.Complete();
        }
    }
}
