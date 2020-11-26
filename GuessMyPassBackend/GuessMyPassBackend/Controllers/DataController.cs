using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Contexts;

namespace GuessMyPassBackend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("data")]
    public class DataController : Controller
    {
        private readonly IDataContext _datacontext;

        public DataController(IDataContext datacontext)
        {
            _datacontext = datacontext;
        }


        [HttpPost]
        public ActionResult CreateData([FromBody] Data data)
        {
            Data newData;
            newData = _datacontext.CreateData(data, HttpContext.Request.Headers["Authorization"]);

            return Ok(newData);
        }

        [HttpPut]
        public ActionResult UpdateData([FromBody] Data data)
        {

            Data updatedData = _datacontext.UpdateData(data);

            if (updatedData == null)
            {
                return BadRequest(new { error = "Wrong data! Try again!" });
            }

            return Ok(updatedData);
        }

        // delete data by id
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDataById(string id)
        {
            string message = _datacontext.DeleteDataById(id, HttpContext.Request.Headers["Authorization"]);

            if (message == null)
            {
                return BadRequest(new { error = "Data with this id doesn't exist" });
            }

            return Ok(new { message });
        }

        // Get all data of user
        [HttpGet]
        public List<Data> Get()
        {
            return _datacontext.GetAllData(HttpContext.Request.Headers["Authorization"]);
        }

    }
}
