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
        public ObjectId dbID { get; set; }
        [BsonElement("name")]
        private string Name {get;set;} = String.Empty;
        [BsonElement("userId")]
        public string UserId { get; set; } = String.Empty;


    }


    enum Type 
    {
        ACCOUNT,
        CREDITCARD,
        NOTE
    }

}
