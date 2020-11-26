using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Utils;

namespace GuessMyPassBackend.Services
{
    public class KeyService
    {

        private readonly IMongoCollection<Data> _dataCollection;

        private string collectionName = "data";
        public KeyService(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _dataCollection = client.GetDatabase(settings.Value.Database).GetCollection<Data>(collectionName);
            }

        }

        public string UpdateDataByKey(List<Data> data, string token)
        {

            try
            {
                string userId = Helpers.findIdentity(token);

                foreach (Data dataItem in data)
                {
                    FilterDefinition<Data> filter = Builders<Data>.Filter.Where(data => data.UserId == dataItem.UserId && dataItem.id == data.id && dataItem.UserId == userId);

                    UpdateDefinition<Data> update = Builders<Data>.Update
                        .Set("name", dataItem.Name)
                        .Set("password", dataItem.Password)
                        .Set("url", dataItem.Url)
                        .Set("notes", dataItem.Notes)
                        .Set("cardholderName", dataItem.CardholderName)
                        .Set("number", dataItem.Number)
                        .Set("cvv", dataItem.CVV)
                        .Set("folder", dataItem.Folder);

                    _dataCollection.FindOneAndUpdate(filter, update);
                }

                return "Success";
            } catch (Exception)
            {
                return null;
            }

            
        }

    }
}
