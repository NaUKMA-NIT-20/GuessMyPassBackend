using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.Extensions.Options;

namespace GuessMyPassBackend.Contexts
{

    public interface IUserContext
    {
        string CreateUser(User user);
        AuthedUser Login(string username, string password);
        string UpdatePassword(UserOptions requestBody, string token);
        string UpdateUsername(UserOptions requestBody, string token);
    }
    public class UserContext : IUserContext
    {
        private readonly UsersService _service = null;

        public UserContext(IOptions<Settings> settings)
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

        public string UpdatePassword(UserOptions requestBody, string token)
        {
            return _service.UpdatePassword(requestBody, token);
        }

        public string UpdateUsername(UserOptions requestBody, string token)
        {
            return _service.UpdateUsername(requestBody, token);
        }
    }
}
