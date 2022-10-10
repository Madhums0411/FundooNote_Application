using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly FundoContext fundocontext;

        public NotesController(INotesBL notesBL, FundoContext fundocontext)
        {
            this.notesBL = notesBL;
            this.fundocontext = fundocontext;
        }
        [Authorize]
        [HttpPost]
        [Route("Create Note")]
        public ActionResult Create(NotesModel notes)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Create(notes, UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created Successfully", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Notes is not created " });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Get Notes")]
        public ActionResult GetNotes()
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.GetNote(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Read all notes", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Unable to read notes" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("Delete Note")]
        public IActionResult DeleteNotesOfUser(long notesid)
        {
            try

            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                if (this.notesBL.DeleteNote(notesid))
                {
                    return this.Ok(new { Success = true, message = "Note deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Noteid not Found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("Update Note")]
        public IActionResult UpdateNote(NotesModel notesModel, long noteId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var notes = this.notesBL.UpdateNote(notesModel, noteId);
                if (notes != false)
                {
                    return this.Ok(new { Success = true, message = "Note updated successfully", data = notesModel });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Noteid not Found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
