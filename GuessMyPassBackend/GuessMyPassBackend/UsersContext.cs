using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core;
using GuessMyPassBackend.Models;
using MongoDB.Driver;

namespace GuessMyPassBackend
{
    public class UsersContext
    {
        private readonly IMongoDatabase _database = null;

        private string users = "users";

        public UsersContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
                
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("users");
            }
        }

        public string CreateUser(User user)
        {
            string errorToReturn = "Empty request";
            string errorToReturn1 = "Wrong request. User already exists";

            string userCreated = "User was created";

            if (user.Username.Length == 0 || user.HashedPassword.Length == 0 || user.Email.Length == 0) return errorToReturn;
            if (!CanCreateUser(user.Username, user.Email)) return errorToReturn1;

            _database.GetCollection<User>(users).InsertOne(user);
            return userCreated;
        }

        public User GetUser(string email, string password)
        {
            User returnUser;
            try
            {
                returnUser = _database.GetCollection<User>("users").Find(a => a.HashedPassword == password && a.Email == email).First();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

            return returnUser;
        }

        public bool CanCreateUser(string username, string email)
        {
            User returnUser;
            try
            {
                returnUser = _database.GetCollection<User>("users").Find(a => a.Username == username || a.Email == email).First();
            }
            catch (System.InvalidOperationException)
            {
                return true;
            }
            return false;
        }

    }
}