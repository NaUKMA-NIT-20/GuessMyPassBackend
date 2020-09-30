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

        [BsonRepresentation(BsonType.ObjectId)]
        private string Id { get; set; }

        private string Name {get;set;} = String.Empty;

        public string UserId { get; set; } = String.Empty;

    }


    enum Type 
    {
        ACCOUNT,
        CREDITCARD,
        NOTE
    }

}
