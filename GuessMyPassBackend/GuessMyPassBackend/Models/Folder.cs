using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuessMyPassBackend.Models
{
    public class Folder
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public string id { get; set; } = String.Empty;

        [BsonElement("userId")]
        public string UserId { get; set; } = String.Empty;

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        public Folder()
        {
            id = ObjectId.GenerateNewId().ToString();
        }
    }
}
