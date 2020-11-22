using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Utils;


namespace GuessMyPassBackend.Services
{
    public class DataService
    {

        private readonly IMongoCollection<Data> _dataCollection;

        private string collectionName = "data";
        public DataService(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _dataCollection = client.GetDatabase(settings.Value.Database).GetCollection<Data>(collectionName);
            }

        }

        public List<Data> GetAllData(string token)
        {

            string id = Helpers.findIdentity(token);

            return _dataCollection.Find<Data>(data => data.UserId == id).ToList();
        }

        public Data CreateData(Data data, string token)
        {

            string id = Helpers.findIdentity(token);

            data.UserId = id;

            _dataCollection.InsertOne(data);

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
                .Set("cvv", data.CVV)
                .Set("folder", data.Folder);


            return _dataCollection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Data> { ReturnDocument = ReturnDocument.After });
            
        }

        // delete data by id
        public string DeleteDataById(string id, string token)
        {

            try
            {

                string owner = Helpers.findIdentity(token);

                Data data = _dataCollection.FindOneAndDelete(d => d.UserId.Equals(owner) && d.id.Equals(id));

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

    }
}
