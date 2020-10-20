using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace GuessMyPassBackend.Models
{
    public class User
    {

        [BsonId]
        public ObjectId DbId { get; set; }
       /* [BsonElement("id")]
        public string Id { get; set; }*/
        
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

    }
}
