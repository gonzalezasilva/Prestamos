namespace WebApplicationPrestamos.DataAccess
{
    public interface IUnitOfWork
    {
        IThingRepository ThingRepository { get; }

        int Complete();
    }
}