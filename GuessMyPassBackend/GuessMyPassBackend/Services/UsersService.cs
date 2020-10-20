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
using BC = BCrypt.Net.BCrypt;


namespace GuessMyPassBackend
{
   
    public class UsersService
    {
        private readonly IMongoDatabase _database = null;

        private string users = "users";

        private string jwtSecret = null;
        public UsersService(IOptions<Settings> settings)
        {
            jwtSecret = settings.Value.JWT_SECRET;
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
                
        }

        // add user to db
        public string CreateUser(User user)
        {
            string errorToReturn = "Empty request";
            string errorToReturn1 = "Wrong request. User already exists";

            string userCreated = "User was created";

            if (user.Username.Length == 0 || user.Password.Length == 0 || user.Email.Length == 0) return errorToReturn;
            if (!CanCreateUser(user.Username, user.Email)) return errorToReturn1;

            user.Password = BC.HashPassword(user.Password);

            _database.GetCollection<User>(users).InsertOne(user);
            return userCreated;
        }

        // check if user exists and return AuthedUser instance
        public AuthedUser Login(string email, string password)
        {
            AuthedUser returnUser;
            try
            {
                returnUser = _database.GetCollection<AuthedUser>("users").Find(a => a.Email == email).First();

                // verify password from request and db

                if (password == null || !BC.Verify(password, returnUser.Password)) throw new System.InvalidOperationException();

                returnUser.Token = generateJwtToken(returnUser.Username, returnUser.Email);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

            return returnUser;
        }

        // generate token that will be valid for 7 days
        private string generateJwtToken(string username, string email)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jwtSecret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {

                // decode payload ( username, email )

                Subject = new ClaimsIdentity(new[] { new Claim("username", username), new Claim("email", email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // check if user can be created ( find occurrences in db )

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