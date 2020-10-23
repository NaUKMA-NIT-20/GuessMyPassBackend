using GuessMyPassBackend.Models;


namespace GuessMyPassBackend.Services
{
    public interface IUserRepository
    {
        string CreateUser(User user);

        AuthedUser Login(string username, string password);
        string UpdatePassword(UserOptions requestBody, string token);
        string UpdateUsername(UserOptions requestBody, string token);
    }
}
