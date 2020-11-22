using System.Collections.Generic;
using Microsoft.Extensions.Options;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;

namespace GuessMyPassBackend.Contexts
{

    public interface IFolderContext
    {
        Folder CreateFolder(Folder user, string token);
        List<Folder> GetAllFolders(string token);
        Folder UpdateFolderName(Folder folder, string token);
        string DeleteFolderById(string id, string token);

    }
        public class FolderContext : IFolderContext
        {
            private readonly FoldersService _service;

            public FolderContext(IOptions<Settings> settings)
            {
                _service = new FoldersService(settings);
            }

            public Folder CreateFolder(Folder user, string token)
            {
                return _service.CreateFolder(user, token);
            }

            public string DeleteFolderById(string id, string token)
            {
                return _service.DeleteFolderById(id, token);
            }
            
            public Folder UpdateFolderName(Folder folder, string token)
            {
                return _service.UpdateFolder(folder);
            }

            public List<Folder> GetAllFolders(string token)
            {
                return _service.GetAllFolders(token);
            }
        }
    
}
