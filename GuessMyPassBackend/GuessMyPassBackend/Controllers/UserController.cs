using System;
using System.Collections.Generic;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace GuessMyPassBackend.Controllers
{
    [Produces("application/json")]
    [Route("user")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userContext;

        public UsersController(IUserRepository userContext)
        {
            _userContext = userContext;
        }


        // /user/test
        [HttpGet]
        [Route("test")]
        public String Login()
        {
            return "Izi dla menia. Ludshiu v mire za rabotoi";
        }

        
        // /user/login
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] AuthenticateRequest userFromRequest)
        {
            AuthedUser user = null;

            user = _userContext.Login(userFromRequest.Email, userFromRequest.Password);

            if (user == null)
            {

                return StatusCode(404, "Wrong email or password");
            }

            return Ok(user);
        }


        // /user/register
        [HttpPost]
        [Route("register")]
        public ActionResult<string> Register([FromBody] User user)
        {
            return _userContext.CreateUser(user);
        }

    }
}
