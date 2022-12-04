using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public class GenericRepository<TEntity> : ControllerBase, IGenericRepository<TEntity> 
        where TEntity : EntityBase
    {
        protected ThingsContext context;
        internal DbSet<TEntity> dbSet; 

        public GenericRepository(ThingsContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            var savedEntity = dbSet.Add(entity);
            return savedEntity.Entity;
        }

        public bool Delete(int id)
        {
        
            var savedEntity = dbSet.Find(id);
            if (savedEntity is null)
                return false;

            var result = dbSet.Remove(savedEntity);

            return !(result is null);

          }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList(); 
        }

        public TEntity GetById(int id)
        {
        
            return dbSet.FirstOrDefault(c => c.Id == id);
        }

        public TEntity Update(TEntity entity)
        {
           // if (entity.id <= 0)
           //     return BadRequest("Id must be higher than 0");

            //    //En proyectos mas grandes es posible que primero busquen si el registro existe, si no existe, se puede devolver NotFound(). Si existe, se realiza el update.

            var savedEntity = context.Update(entity);

            return savedEntity.Entity;
        }
    }
}
