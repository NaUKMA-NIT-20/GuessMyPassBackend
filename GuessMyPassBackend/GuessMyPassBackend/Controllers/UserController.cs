using System;
using System.Collections.Generic;
using System.Diagnostics;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson.IO;

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

        [HttpGet]
        [Route("test")]
        public String Login()
        {
            return "Izi dla menia. Ludshiu v mire za rabotoi";
        }


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

        [HttpPost]
        [Route("register")]
        public ActionResult<string> Register([FromBody] User user)
        {
            return _userContext.CreateUser(user);
        }

        [HttpGet]
        public Task<IEnumerable<User>> Get()
        {
            return _userContext.GetAllNotes();
        }
    }
}
