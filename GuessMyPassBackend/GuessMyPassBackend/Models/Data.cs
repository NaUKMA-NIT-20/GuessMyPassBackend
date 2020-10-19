using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuessMyPassBackend.Models
{
    public class Data
    {
        [BsonId]
        private ObjectId DbId { get; set; }
        [BsonElement("owner")]
        private string Owner { get; set; } = String.Empty;

        [BsonElement("name")]
        private string Name {get;set;} = String.Empty;
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

        
    }


    enum Type 
    {
        NONE,
        ACCOUNT,
        CREDITCARD,
        NOTE
    }
}
