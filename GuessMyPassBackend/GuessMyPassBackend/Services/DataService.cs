using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core;
using GuessMyPassBackend.Models;
using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Reflection.Metadata;


namespace GuessMyPassBackend.Services
{
    public class DataService
    {
        private readonly IMongoDatabase _database = null;

        private string data = "data";
        public DataService(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }

        }

        public List<Data> GetAllData(string token)
        {

            string email = DecodeJwtEmail(token);

            return _database.GetCollection<Data>("data").Find<Data>(data => data.Owner == email).ToList();
        }

        public Data CreateData(Data data, string token)
        {

            string decoded = DecodeJwtEmail(token);

            data.Owner = decoded;
            data.id = data._id.ToString();
            _database.GetCollection<Data>("data").InsertOne(data);

            return data;
        }

        public Data UpdateData(Data data)
        {
            
            FilterDefinition<Data> filter = Builders<Data>.Filter.Eq("id", data.id);
            
            UpdateDefinition<Data> update = Builders<Data>.Update
                .Set("name", data.Name)
                .Set("password",data.Password)
                .Set("url", data.Url)
                .Set("notes", data.Notes)
                .Set("cardholderName", data.CardholderName)
                .Set("number", data.Number)
                .Set("cvv", data.CVV);


            return _database.GetCollection<Data>("data").FindOneAndUpdate<Data>(filter, update, new FindOneAndUpdateOptions<Data> { ReturnDocument = ReturnDocument.After });
            
        }

        // delete data by id
        public string DeleteDataById(string id, string token)
        {

            try
            {

                string email = DecodeJwtEmail(token);

                Data data = _database.GetCollection<Data>("data").FindOneAndDelete(d => d.Owner.Equals(email) && d.id.Equals(id));

                if(data == null)
                {
                    return null;
                }

                return "Data deleted successfully";

            } catch (Exception)
            {
                return null;
            }

        }

        private string DecodeJwtEmail(string tokenString)
        {

            try
            {
                JwtSecurityToken token = new JwtSecurityToken(tokenString);

                string email = token.Claims.First(c => c.Type == "email").Value;

                return email;
            } catch (Exception) {

                return "";

            }

            
        }

    }
}
