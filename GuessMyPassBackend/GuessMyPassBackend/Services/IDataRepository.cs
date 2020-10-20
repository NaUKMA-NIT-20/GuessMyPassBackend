using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessMyPassBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuessMyPassBackend.Services
{
    public interface IDataRepository
    {
        List<Data> GetAllData(string token);
        Data CreateData(Data data, string token);
        Task<Data> UpdateData(Data data);
    }
}
