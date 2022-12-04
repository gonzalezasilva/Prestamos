using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPrestamos.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace WebApplicationPrestamos.Controllers.EF
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UnitOfWorkController  : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult TestingUom()
        {
            var things = unitOfWork.ThingRepository.GetAll();
            return Ok(things);
        }
    }
}
