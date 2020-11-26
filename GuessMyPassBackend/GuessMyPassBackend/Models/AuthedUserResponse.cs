using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuessMyPassBackend.Models
{
    public class AuthedUserResponse: User
    {
      
        [BsonElement("token")]
        public string Token { get; set; }

        public AuthedUserResponse()
        {

        }
        public AuthedUserResponse(User user) : base()
        {
            this.CreatedOn = user.CreatedOn;
            this.Email = user.Email;
            this.id = user.id;
            this.Password = user.Password;
            this.PasswordHelp = user.PasswordHelp;
            this.Username = user.Username;
        }
    }
}
