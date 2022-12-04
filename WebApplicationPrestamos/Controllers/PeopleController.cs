using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPrestamos.DataAccess;
using WebApplicationPrestamos.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplicationPrestamos.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PeopleController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public PeopleController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost]
        public Person Create([FromBody]Person person) 
        {
            uow.PersonRepository.Add(person);
            uow.Complete();

            return person; 
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var deleted = uow.PersonRepository.Delete(id);
            if (!deleted)
                return NotFound();

            uow.Complete();

            return Ok();


        }


        [HttpGet]
        public ActionResult<List<Person>> GetAll()
        {
            return uow.PersonRepository.GetAll();
        }
       
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Person> GetById(int id)
        {
           
            return uow.PersonRepository.GetById(id);
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Person> UpdatePersonData(int id, [FromBody] Person person)
        {
            if (string.IsNullOrWhiteSpace(person?.Name))
                return BadRequest("El nombre de la persona no puede ser vacía.");
            if (string.IsNullOrWhiteSpace(person?.Email))
                return BadRequest("El mail de la persona no puede ser vacía.");
            if (string.IsNullOrWhiteSpace(person?.Phone))
                return BadRequest("El teléfono de la persona no puede ser vacía.");

            var dbPerson = uow.PersonRepository.GetById(id);
            if (dbPerson == null)
                return NotFound();

            dbPerson.Name = person.Name;
            dbPerson.Email = person.Email;
            dbPerson.Phone = person.Phone;

            uow.Complete();

            return person;
        }

    }
}