using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using GuessMyPassBackend.Contexts;
using GuessMyPassBackend.Models;

namespace GuessMyPassBackend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("folders")]
    public class FolderController : Controller
    {
        private readonly IFolderContext _folderContext;

        public FolderController(IFolderContext folderContext)
        {
            _folderContext = folderContext;
        }

        [HttpPost]
        public ActionResult Create([FromBody] Folder folder)
        {
            if(folder.Name == null || folder.Name.Length == 0)
            {
                return BadRequest(new { error = "Wrong name provided!" });
            }

            Folder newData;
            newData = _folderContext.CreateFolder(folder, HttpContext.Request.Headers["Authorization"]);

            return Ok(newData);
        }
        
        [HttpPut]
        public ActionResult Update([FromBody] Folder data)
        {

            Folder updatedData = _folderContext.UpdateFolderName(data, HttpContext.Request.Headers["Authorization"]);

            if (updatedData == null)
            {
                return BadRequest(new { error = "Wrong request! Try again!" });
            }

            return Ok(updatedData);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteById(string id)
        {
            string message = _folderContext.DeleteFolderById(id, HttpContext.Request.Headers["Authorization"]);

            if (message == null)
            {
                return BadRequest(new { error = "Folder with this id doesn't exist" });
            }

            return Ok(new { message });
        }

        [HttpGet]
        public List<Folder> Get()
        {
            return _folderContext.GetAllFolders(HttpContext.Request.Headers["Authorization"]);
        }

    }
}
