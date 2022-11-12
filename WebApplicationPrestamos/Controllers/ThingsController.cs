using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPrestamos.DataAccess;
using WebApplicationPrestamos.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplicationPrestamos.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ThingsController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public ThingsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost]
        public Thing Create([FromBody]Thing thing) //Tengamos presente que normalmente las entidades NO se utilizan como request de APIs
        {
            uow.ThingRepository.Add(thing);
            uow.Complete();

            return thing; //Observar que devuelve el ID. Como es posible?
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var deleted = uow.ThingRepository.Delete(id);
            if (!deleted)
                return NotFound();

            uow.Complete();

            return NoContent();


        }

        [HttpGet]
        public ActionResult<List<Thing>> GetAll()
        {
            return uow.ThingRepository.GetAll();
        }
       
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Thing> GetById(int id)
        {
           
            return uow.ThingRepository.GetById(id);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Thing> Update(int id, [FromBody] Thing thing)
        {
           if (id <= 0)
                return BadRequest("Id must be higher than 0");

        //    //En proyectos mas grandes es posible que primero busquen si el registro existe, si no existe, se puede devolver NotFound(). Si existe, se realiza el update.
   
            var savedEntity = uow.ThingRepository.Update(thing);
  
            return savedEntity;
        }
    }
}