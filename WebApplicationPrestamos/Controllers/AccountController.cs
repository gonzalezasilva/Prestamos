using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
//using WebApplicationPrestamos.Dto;
using WebApplicationPrestamos.Handlers;
using WebApplicationPrestamos.Models;

namespace WebApplicationPrestamos.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IJwtHandler jwtHandler;

        public AccountsController(IJwtHandler jwtHandler)
        {
            this.jwtHandler = jwtHandler;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserInfo user) 
        {
           
            if (ModelState.IsValid)
            {
                if (user.Username=="bea" && user.Password=="bea")
                {
                     var roles = user.Username == "bea" ?
                     new List<string> { "Admin" } :
                     new List<string> { "User" };


                    var bearer = jwtHandler.GenerateToken(user, roles);
                    return Ok(new
                    {
                        token = bearer,
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }
    }
}
