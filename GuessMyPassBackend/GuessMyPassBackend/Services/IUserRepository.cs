using GuessMyPassBackend.Models;


namespace GuessMyPassBackend.Services
{
    public interface IUserRepository
    {
        string CreateUser(User user);

        AuthedUser Login(string username, string password);

        string UpdatePassword(PasswordRestartRequest requestBody, string token);
    }
}
