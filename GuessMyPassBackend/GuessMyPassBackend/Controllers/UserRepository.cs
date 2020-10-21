using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GuessMyPassBackend.Controllers
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersService _service = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _service = new UsersService(settings);
        }

        public string CreateUser(User user)
        {
            return _service.CreateUser(user);
        }

        public AuthedUser Login(string email, string password)
        {
            return _service.Login(email, password);
        }

        public string UpdatePassword(PasswordRestartRequest requestBody, string token)
        {
            return _service.UpdatePassword(requestBody, token);
        }

    }
}
