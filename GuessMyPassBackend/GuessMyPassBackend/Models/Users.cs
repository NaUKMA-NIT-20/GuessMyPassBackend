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
        public ObjectId dbID { get; set; }
        [BsonElement("Id")]
        public string Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; } = String.Empty;
        [BsonElement("HashedPassword")]
        public string HashedPassword { get; set; } = String.Empty;
        [BsonElement("Confirmed")]
        public bool Confirmed { get; set; } = false;
        [BsonElement("PasswordHelp")]
        public string PasswordHelp { get; set; } = String.Empty;
        [BsonElement("CreatedOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [BsonElement("DataReference")]
        public List<Data> DataReference { get; set; } = new List<Data>();
        


    }
}
