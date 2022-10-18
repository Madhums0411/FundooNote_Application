using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NotesController> _logger;
        public NotesController(INotesBL notesBL, FundoContext fundocontext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<NotesController> _logger)
        {
            this.notesBL = notesBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(NotesModel notes)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Create(notes, UserID);
                if (result != null)
                {
                    _logger.LogInformation("Notes Created Successfully");
                    return Ok(new { success = true, message = "Notes Created Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Notes is not created");
                    return NotFound(new { success = false, message = "Notes is not created " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [HttpPost]
        [Route("GetAll")]
        public ActionResult GetNotes()
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.GetNote(UserID);
                if (result != null)
                {
                    _logger.LogInformation("Read all notes");
                    return Ok(new { success = true, message = "Read all notes", data = result });
                }
                else
                {
                    _logger.LogInformation("Unable to read notes");
                    return NotFound(new { success = false, message = "Unable to read notes" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotesOfUser(long NotesId)
        {
            try

            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                if (this.notesBL.DeleteNote(NotesId))
                {
                    _logger.LogInformation("Note deleted successfully");
                    return this.Ok(new { Success = true, message = "Note deleted successfully" });
                }
                else
                {
                    _logger.LogInformation("Noteid not Found");
                    return this.BadRequest(new { Success = false, message = "Noteid not Found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateNote(NotesModel notesModel, long NotesId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var notes = this.notesBL.UpdateNote(notesModel, NotesId);
                if (notes != false)
                {
                    _logger.LogInformation("Note updated successfully");
                    return this.Ok(new { Success = true, message = "Note updated successfully", data = notesModel });
                }
                else
                {
                    _logger.LogInformation("Noteid not Found");
                    return this.BadRequest(new { Success = false, message = "Noteid not Found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public ActionResult PinNote(long NotesId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.PinNote(NotesId, UserID);
                if (result != null)
                {
                    _logger.LogInformation("Note Pinned Successfully");
                    return Ok(new { success = true, message = "Note Pinned Successfully", data = result });
                }
                else if (result == null)
                {
                    _logger.LogInformation("Note Pinned UnSuccessfully");
                    return Ok(new { success = false, message = "Note Pinned UnSuccessfully" });
                }
                return BadRequest(new { success = false, message = "Could not perform Pin Operation" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult ArchiveNote(long notesId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NoteArchive(notesId, userID);
                if (result != null)
                {
                    _logger.LogInformation("Archived Note Successfully");
                    return Ok(new { success = true, message = "Archived Note Successfully", data = result });
                }
                else if (result == null)
                {
                    _logger.LogInformation("Archived Note UnSuccessfull");
                    return Ok(new { success = true, message = "Archived Note UnSuccessfull", data = result });
                }
                else
                {
                    _logger.LogInformation("Could not perform Archive Operation");
                    return BadRequest(new { success = false, message = "Could not perform Archive Operation" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult TrashNote(long NoteId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NoteTrash(UserID, NoteId);
                if (result != false)
                {
                    _logger.LogInformation("Trashed Note");
                    return Ok(new { success = true, message = "Trashed Note" });
                }
                else if (result != true)
                {
                    _logger.LogInformation("Notes Trashed Successfully");
                    return Ok(new { success = true, message = "Notes Trashed Successfully" });
                }
                else
                {
                    _logger.LogInformation("Could not Perform Trash Operation");
                    return BadRequest(new { success = false, message = "Could not Perform Trash Operation" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Colour")]
        public IActionResult ColourChange(long notesId, string Colour)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var resdata = notesBL.NoteColourChange(notesId, Colour);
                if (resdata != null)
                {
                    _logger.LogInformation("Colour changed Successfully");
                    return this.Ok(new { success = true, message = "Colour changed Successfully", data = resdata });
                }
                else
                {
                    _logger.LogInformation("Colour not Changed");
                    return this.BadRequest(new { success = false, message = "Colour not Changed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile image, long notesId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Image(UserID, notesId, image);
                if (result != null)
                {
                    _logger.LogInformation("Image Uploaded Succesfully");
                    return Ok(new { success = true, message = "Image Uploaded Succesfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Image Upload Unsuccessfull");
                    return BadRequest(new { success = false, message = "Image Upload Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            try
            {

                var cacheKey = "NotesList";
                string serializedNotesList;
                var notesList = new List<NotesEntity>();
                var redisNotesList = await distributedCache.GetAsync(cacheKey);
                if (redisNotesList != null)
                {
                    _logger.LogInformation("Notes Retrived Succesfully");
                    serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                    notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
                }
                else
                {
                    _logger.LogInformation("Notes Retrived Unsuccesfull");
                    notesList = await fundocontext.NotesTable.ToListAsync();
                    serializedNotesList = JsonConvert.SerializeObject(notesList);
                    redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisNotesList, options);
                }
                return Ok(notesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }

    }
}
