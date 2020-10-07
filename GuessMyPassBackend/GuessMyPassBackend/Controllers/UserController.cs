﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
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

        [HttpGet]
        [Route("test")]
        public String Login()
        {
            return "Izi dla menia. Ludshiu v mire za rabotoi";
        }


        [HttpPost]
        [Route("login")]
        public User Login([FromBody] User user)
        {
            user = _userContext.GetUser(user.Email, user.HashedPassword);
            return user;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<bool> Register([FromBody] User user)
        {
            _userContext.CreateUser(user);
            return true;
        }

        [HttpGet]
        public Task<IEnumerable<User>> Get()
        {
            return _userContext.GetAllNotes();
        }
    }
}
