using Microsoft.AspNetCore.Mvc;
using WebApplicationPrestamos.Entities;

namespace WebApplicationPrestamos.DataAccess
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ThingsContext context)
            : base(context) 
        {
        }
        public Category GetByDescription(string description)
        {
            return context.Categories.FirstOrDefault(c => c.Description == description);
        
        }
    }
}
