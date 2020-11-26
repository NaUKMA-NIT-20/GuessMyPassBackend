using System.Collections.Generic;
using Microsoft.Extensions.Options;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;

namespace GuessMyPassBackend.Contexts
{
    public interface IKeyContext
    {
        string UpdateDataByKey(List<Data> data, string token);
    }
    public class KeyContext : IKeyContext
    {
        private readonly KeyService _service = null;

        public KeyContext(IOptions<Settings> settings)
        {
            _service = new KeyService(settings);
        }

        public string UpdateDataByKey(List<Data> data, string token)
        {
            if(data == null)
            {
                return null;
            }

            return _service.UpdateDataByKey(data, token);
        }

    }
}
