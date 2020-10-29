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
using System.Linq;


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

        // Add user to db
        public string CreateUser(User user)
        {
            string errorToReturn = "Empty request";
            string errorToReturn1 = "Wrong request. User already exists";

            string userCreated = "User was created";

            if (user.Username.Length == 0 || user.Password.Length == 0 || user.Email.Length == 0) return errorToReturn;
            if (!UserExists(user.Username, user.Email)) return errorToReturn1;

            user.Password = BC.HashPassword(user.Password);

            _database.GetCollection<User>(users).InsertOne(user);
            return userCreated;
        }

        // Check if user exists and return AuthedUser instance
        public AuthedUser Login(string email, string password)
        {
            AuthedUser returnUser;
            try
            {
                returnUser = _database.GetCollection<AuthedUser>("users").Find(a => a.Email == email).First();

                // Verify password from request and db

                if (password == null || !BC.Verify(password, returnUser.Password)) throw new System.InvalidOperationException();

                returnUser.Token = generateJwtToken(returnUser.Username, returnUser.Email);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

            return returnUser;
        }

        // Update user password
        public string UpdatePassword(UserOptions requestBody, string tokenString)
        {

            try
            {

                JwtSecurityToken token = new JwtSecurityToken(tokenString);

                string email = token.Claims.First(c => c.Type == "email").Value;

                // Get user by email from token
                User originalUser = _database.GetCollection<User>("users").Find(a => a.Email == email).First();

                

                if (requestBody.NewPassword == null || requestBody.Password == null || !BC.Verify(requestBody.Password, originalUser.Password)) throw new Exception();

               
                // Hash new Password
                string newPasswordHashed = BC.HashPassword(requestBody.NewPassword);

                FilterDefinition<User> filter = Builders<User>.Filter.Eq("email", email);
                UpdateDefinition<User> update = Builders<User>.Update.Set("password", newPasswordHashed);

                _database.GetCollection<User>("users").FindOneAndUpdate<User>(filter, update);

                return "Password updated";

            } catch (Exception)
            {
                return null;
            }

        }

        // Update username
        public string UpdateUsername(UserOptions requestBody, string tokenString)
        {
            try
            {

                JwtSecurityToken token = new JwtSecurityToken(tokenString);

                string email = token.Claims.First(c => c.Type == "email").Value;

                // Get user by email from token
                User originalUser = _database.GetCollection<User>("users").Find(a => a.Email == email).First();

                if (requestBody.NewUsername == null || requestBody.Username == null || requestBody.Username != originalUser.Username ) throw new Exception();

                // Check if user with newUsername already exists
                if (!UserExists(requestBody.NewUsername, "1"))
                {
                    return "User with same username already exists";
                }

                FilterDefinition<User> filter = Builders<User>.Filter.Eq("username", requestBody.Username);
                UpdateDefinition<User> update = Builders<User>.Update.Set("username", requestBody.NewUsername);

                _database.GetCollection<User>("users").FindOneAndUpdate<User>(filter, update);

                return "Username updated";

            }
            catch (Exception)
            {
                return null;
            }
        }


        // Generate token that will be valid for 7 days
        private string generateJwtToken(string username, string email)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jwtSecret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {

                // Decode payload ( username, email )

                Subject = new ClaimsIdentity(new[] { new Claim("username", username), new Claim("email", email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // Check if user can be created ( find occurrences in db )

        public bool UserExists(string username, string email)
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