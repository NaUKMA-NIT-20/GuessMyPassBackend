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

        public void CreateUser(User user)
        {
            if (user == null) return;
            _database.GetCollection<User>(users).InsertOne(user);
        }

        public User GetUser(string email, string password)
        {
            // Check for password or login are valid
            return _database.GetCollection<User>("users").Find(a => a.HashedPassword == password && a.Email == email).First();
        }

    }
}