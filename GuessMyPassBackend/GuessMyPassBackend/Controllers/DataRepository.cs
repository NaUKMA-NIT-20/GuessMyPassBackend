using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GuessMyPassBackend.Controllers
{
    public class DataRepository : IDataRepository
    {
        private readonly DataService _service = null;

        public DataRepository(IOptions<Settings> settings)
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

        public Task<Data> UpdateData(Data data)
        {
            return _service.UpdateData(data);
        }
    }
}
