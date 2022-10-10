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
        [Route("Create")]
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


    }
}
