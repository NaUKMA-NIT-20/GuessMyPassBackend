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
        private readonly UsersContext _context = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new UsersContext(settings);
        }

        public async Task<IEnumerable<User>> GetAllNotes()
        {
            return await _context.Users.Find(a => true).ToListAsync(); 
        }

        public string CreateUser(User user)
        {
            return _context.CreateUser(user);
        }

        public User GetUser(string email, string password)
        {
            return _context.GetUser(email, password);
        }
    }
}
