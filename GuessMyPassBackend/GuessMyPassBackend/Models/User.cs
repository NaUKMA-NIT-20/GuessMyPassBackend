using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuessMyPassBackend.Models
{
    public class User
    {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        [BsonRequired]
        public string id { get; set; } = String.Empty;
        
        [BsonElement("email")]
        [BsonRequired]
        public string Email { get; set; }
        
        [BsonElement("username")]
        [BsonRequired]
        public string Username { get; set; } = String.Empty;
        
        [BsonElement("password")]
        [BsonRequired]
        public string Password { get; set; } = String.Empty;
      
        [BsonElement("passwordHelp")]
        public string PasswordHelp { get; set; } = String.Empty;
        
        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public User()
        {
            id = ObjectId.GenerateNewId().ToString();
        }
    }
}
