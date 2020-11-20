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
        public string id { get; set; } = String.Empty;

        [BsonElement("email")]
        public string Email { get; set; }
        
        [BsonElement("username")]
        public string Username { get; set; } = String.Empty;
        
        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;
      
        [BsonElement("passwordHelp")]
        public string PasswordHelp { get; set; } = String.Empty;
        
        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        public User()
        {
            _id = ObjectId.GenerateNewId();
        }
    }
}
