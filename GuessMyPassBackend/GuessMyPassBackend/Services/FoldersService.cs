using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Utils;

namespace GuessMyPassBackend.Services
{
    public class FoldersService
    {
        private readonly IMongoCollection<Folder> _foldersCollection;

        private string collectionName = "folders";

        private string jwtSecret = null;

        public FoldersService(IOptions<Settings> settings)
        {
            jwtSecret = settings.Value.JWT_SECRET;

            var client = new MongoClient(settings.Value.ConnectionString);
            if(client != null)
            {
                _foldersCollection = client.GetDatabase(settings.Value.Database).GetCollection<Folder>(collectionName);
            }
        }

        // Create new Folder
        public Folder CreateFolder(Folder folder, string token)
        {
            string id = Helpers.findIdentity(token);

            folder.UserId = id;
            _foldersCollection.InsertOne(folder);

            return folder;
        }

        // Get all Folders
        public List<Folder> GetAllFolders(string token)
        {
            string id = Helpers.findIdentity(token);

            return _foldersCollection.Find<Folder>(folder => folder.UserId == id).ToList();

        }

        // Update Folder
        public Folder UpdateFolder(Folder folder)
        {
            FilterDefinition<Folder> filter = Builders<Folder>.Filter.Eq("id", folder.id);

            UpdateDefinition<Folder> update = Builders<Folder>.Update
                .Set("name", folder.Name);

            return _foldersCollection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Folder> { ReturnDocument = ReturnDocument.After });

        }

        public string DeleteFolderById(string id, string token)
        {
            try
            {

                string owner = Helpers.findIdentity(token);

                Folder data = _foldersCollection.FindOneAndDelete(folder => folder.UserId.Equals(owner) && folder.id.Equals(id));

                if (data == null)
                {
                    return null;
                }

                return "Folder deleted successfully";

            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
