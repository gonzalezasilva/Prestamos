namespace WebApplicationPrestamos.DataAccess
{
    public interface IUnitOfWork
    {
        IThingRepository ThingRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPersonRepository PersonRepository { get; }
        ILoanRepository LoanRepository { get; }


        int Complete();
    }
}