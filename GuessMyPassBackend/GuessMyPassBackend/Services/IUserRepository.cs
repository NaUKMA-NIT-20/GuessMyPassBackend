using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessMyPassBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuessMyPassBackend.Services
{
    public interface IUserRepository
    {
        string CreateUser(User user);

        AuthedUser Login(string username, string password);

        string UpdatePassword(PasswordRestartRequest requestBody, string token);
    }
}
