using System;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


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
        public ActionResult Register([FromBody] User user)
        {

            string message = _userContext.CreateUser(user);

            if (!message.Equals("User was created"))
            {
                return BadRequest(message);
            }

            return Ok(message);
        }


        // /user/options/password
        [HttpPut]
        [Route("options/password")]
        public ActionResult UpdatePassword([FromBody] UserOptions requestBody)
        {

            string message = _userContext.UpdatePassword(requestBody, HttpContext.Request.Headers["Authorization"]);

            if(message == null)
            {
                return BadRequest("Wrong Password");
            }

            return Ok(message);

        }

        [HttpPut]
        [Route("options/username")]
        public ActionResult UpdateUsername([FromBody] UserOptions requestBody)
        {

            string message = _userContext.UpdateUsername(requestBody, HttpContext.Request.Headers["Authorization"]);

            if (message == null || message.Equals("User with same username already exists"))
            {

                return (message == null) ? BadRequest("Wrong username") : BadRequest(message);
            }

            return Ok(message);

        }

    }
}
