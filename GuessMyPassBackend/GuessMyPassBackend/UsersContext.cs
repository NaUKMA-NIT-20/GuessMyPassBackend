using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core;
using GuessMyPassBackend.Models;
using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System; 

namespace GuessMyPassBackend
{
    public class UsersContext
    {
        private readonly IMongoDatabase _database = null;

        private string users = "users";

        private string jwtSecret = null;
        public UsersContext(IOptions<Settings> settings)
        {
            jwtSecret = settings.Value.JWT_SECRET;
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

            if (user.Username.Length == 0 || user.Password.Length == 0 || user.Email.Length == 0) return errorToReturn;
            if (!CanCreateUser(user.Username, user.Email)) return errorToReturn1;

            _database.GetCollection<User>(users).InsertOne(user);
            return userCreated;
        }

        public AuthedUser Login(string email, string password)
        {
            AuthedUser returnUser;
            try
            {
                returnUser = _database.GetCollection<AuthedUser>("users").Find(a => a.Password == password && a.Email == email).First();
                returnUser.Token = generateJwtToken(returnUser.Username, returnUser.Email);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

            return returnUser;
        }

        private string generateJwtToken(string username, string email)
        {
            // generate token that is valid for 7 days
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jwtSecret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username), new Claim("email", email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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