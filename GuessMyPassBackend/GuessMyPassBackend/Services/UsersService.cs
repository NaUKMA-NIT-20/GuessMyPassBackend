using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System;
using BC = BCrypt.Net.BCrypt;
using System.Linq;

using GuessMyPassBackend.Utils;
using GuessMyPassBackend.Models;

namespace GuessMyPassBackend
{
   
    public class UsersService
    {

        private readonly IMongoCollection<User> _usersCollection;

        private string collectionName = "users";

        private string jwtSecret = null;
        public UsersService(IOptions<Settings> settings)
        {
            jwtSecret = settings.Value.JWT_SECRET;
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _usersCollection = client.GetDatabase(settings.Value.Database).GetCollection<User>(collectionName);
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

            _usersCollection.InsertOne(user);
            return userCreated;
        }

        // Check if user exists and return AuthedUser instance
        public AuthedUserResponse Login(string email, string password)
        {
            User returnUser;
            try
            {
                returnUser = _usersCollection.Find(a => a.Email == email).First();
               

                // Verify password from request and db
                if (password == null || !BC.Verify(password, returnUser.Password)) throw new System.InvalidOperationException();

                AuthedUserResponse authedUser = new AuthedUserResponse(returnUser);

                authedUser.Token = Helpers.generateJwtToken(returnUser.id, jwtSecret);

                return authedUser;
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

        }

        // Update user password
        public string UpdatePassword(UserOptionsRequest requestBody, string tokenString)
        {
            try
            {
                JwtSecurityToken token = new JwtSecurityToken(tokenString);

                string id = token.Claims.First(c => c.Type == "id").Value;

                // Get user by email from token
                User originalUser = _usersCollection.Find(a => a.id == id).First();
                
                if (requestBody.NewPassword == null || requestBody.Password == null || !BC.Verify(requestBody.Password, originalUser.Password)) throw new Exception();

                // Hash new Password
                string newPasswordHashed = BC.HashPassword(requestBody.NewPassword);

                FilterDefinition<User> filter = Builders<User>.Filter.Eq("username", originalUser.Username);
                UpdateDefinition<User> update = Builders<User>.Update.Set("password", newPasswordHashed);

                _usersCollection.FindOneAndUpdate<User>(filter, update);

                return "Password updated";

            } catch (Exception)
            {
                return null;
            }

        }

        // Update username
        public string UpdateUsername(UserOptionsRequest requestBody, string tokenString)
        {
            try
            {

                JwtSecurityToken token = new JwtSecurityToken(tokenString);

                string id = token.Claims.First(c => c.Type == "id").Value;

                // Get user by id from token
                User originalUser = _usersCollection.Find(a => a.id == id && a.Username == requestBody.Username).First();

                if (requestBody.NewUsername == null || requestBody.Username == null || requestBody.Username != originalUser.Username ) throw new Exception();

                // Check if user with newUsername already exists
                if (!UserExists(requestBody.NewUsername, "1"))
                {
                    return "User with same username already exists";
                }

                FilterDefinition<User> filter = Builders<User>.Filter.Eq("username", requestBody.Username);
                UpdateDefinition<User> update = Builders<User>.Update.Set("username", requestBody.NewUsername);

                _usersCollection.FindOneAndUpdate<User>(filter, update);

                return "Username updated";

            }
            catch (Exception)
            {
                return null;
            }
        }

        // Check if user can be created ( find occurrences in db )

        public bool UserExists(string username, string email)
        {
            User returnUser;
            try
            {
                returnUser = _usersCollection.Find(a => a.Username == username || a.Email == email).First();
            }
            catch (System.InvalidOperationException)
            {
                return true;
            }
            return false;
        }

    }
}