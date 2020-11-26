using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using GuessMyPassBackend.Models;
using GuessMyPassBackend.Contexts;


namespace GuessMyPassBackend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("update")]
    public class KeyController : Controller
    {
        private readonly IKeyContext _keycontext;

        public KeyController(IKeyContext keycontext)
        {
            _keycontext = keycontext;
        }

        [HttpPost]
        [Route("key")]
        public ActionResult UpdateKey([FromBody] UpdateKeyRequest request)
        {

            
            string message = _keycontext.UpdateDataByKey(request.data, HttpContext.Request.Headers["Authorization"]);

            if(message == null)
            {
                return BadRequest(new { error = "Error" });
            }

            return Ok(new { message });
          
        }


    }
}
