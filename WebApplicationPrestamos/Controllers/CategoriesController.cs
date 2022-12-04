using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplicationPrestamos.DataAccess;
using WebApplicationPrestamos.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplicationPrestamos.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {

        private readonly IUnitOfWork uow;


        public CategoriesController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category category) 
        {
            var categorysearch = new Category();
            categorysearch = uow.CategoryRepository.GetByDescription(category.Description);

            if (categorysearch == null)
            {
                uow.CategoryRepository.Add(category);
                uow.Complete();
                return category;
            }
           else
             { 
                return BadRequest("Ya existe la categoría.");
            };
            
                 
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var deleted = uow.CategoryRepository.Delete(id);
            if (!deleted)
                return NotFound();

            uow.Complete();

            return NoContent();


        }

        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return uow.CategoryRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Category> GetById(int id)
        {

            return uow.CategoryRepository.GetById(id);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Category> Update(int id, [FromBody] Category category)
        {
            if (id <= 0)
                return BadRequest("El Id debe ser mayor a cero.");

            if (uow.CategoryRepository.GetById(id) is null)
                return NotFound();

            var savedEntity = uow.CategoryRepository.Update(category);

            return savedEntity;
        }


        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Category> UpdateCategoryDescription(int id, [FromBody] Category category)
        {
            if (string.IsNullOrWhiteSpace(category?.Description))
                return BadRequest("La descripción de la categoría no puede ser vacía.");

            var dbCategory = uow.CategoryRepository.GetById(id);
            if (dbCategory == null)
                return NotFound();

            dbCategory.Description = category.Description;
            uow.Complete();

            return category;
        }
        
       
    }
}