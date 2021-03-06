using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuessMyPassBackend.Models
{
    public class Data
    {

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        public string id { get; set;} = String.Empty;

        [BsonElement("userId")]
        public string UserId { get; set; } = String.Empty;

        [BsonElement("folder")]
        public string Folder { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;

        [BsonElement("type")]
        private Type Type { get; set; } = Type.NONE;

        [BsonElement("url")]
        public string Url { get; set; } = String.Empty;

        [BsonElement("notes")]
        public string Notes { get; set; } = String.Empty;

        [BsonElement("cardholderName")]
        public string CardholderName { get; set; } = String.Empty;

        [BsonElement("number")]
        public string Number { get; set; } = String.Empty;

        [BsonElement("cvv")]
        public string CVV { get; set; } = String.Empty;

        public Data()
        {
            id = ObjectId.GenerateNewId().ToString();
        }

    }



    enum Type 
    {
        ACCOUNT,
        CREDITCARD,
        NOTE,
        NONE
    }

}
