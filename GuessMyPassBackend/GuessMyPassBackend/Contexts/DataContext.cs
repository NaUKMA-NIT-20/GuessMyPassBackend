using System.Collections.Generic;
using Microsoft.Extensions.Options;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;

namespace GuessMyPassBackend.Contexts
{

    public interface IDataContext
    {
        List<Data> GetAllData(string token);
        Data CreateData(Data data, string token);
        Data UpdateData(Data data);
        string DeleteDataById(string id, string token);
    }
    public class DataContext : IDataContext
    {
        private readonly DataService _service = null;

        public DataContext(IOptions<Settings> settings)
        {
            _service = new DataService(settings);
        }

        public List<Data> GetAllData(string token) 
        {
            return _service.GetAllData(token);
        }
        
        public Data CreateData(Data data, string token)
        {
            return _service.CreateData(data, token);
        }

        public Data UpdateData(Data data)
        {
            return _service.UpdateData(data);
        }

        public string DeleteDataById(string id, string token)
        {
            return _service.DeleteDataById(id, token);
        }
    }
}
