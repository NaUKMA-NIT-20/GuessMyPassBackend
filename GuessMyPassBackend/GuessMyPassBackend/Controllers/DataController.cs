using System;
using System.Collections.Generic;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace GuessMyPassBackend.Controllers
{
    [Produces("application/json")]
    [Route("data")]
    public class DataController : Controller
    {
        private readonly IDataRepository _datacontext;

        public DataController(IDataRepository datacontext)
        {
            _datacontext = datacontext;
        }


        [HttpPost]
        public ActionResult CreateData([FromBody] Data data)
        {
                Data newData;

                string token = HttpContext.Request.Headers["Authorization"];
                newData = _datacontext.CreateData(data, token); 

                return Ok(newData);
        }

        [HttpPut]
        public ActionResult UpdateData([FromBody] Data data)
        {

            Data updatedData = _datacontext.UpdateData(data);

            if(updatedData == null)
            {
                return BadRequest("Wrong data");
            }

            return Ok(updatedData);
        }


        // Get all data of user
        [HttpGet]
        public List<Data> Get()
        {
            string token = HttpContext.Request.Headers["Authorization"];
            return _datacontext.GetAllData(token);
        }

    }
}
