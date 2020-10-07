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
        Task<IEnumerable<User>> GetAllNotes();
        string CreateUser(User user);

        User GetUser(string username, string password);
    }
}
