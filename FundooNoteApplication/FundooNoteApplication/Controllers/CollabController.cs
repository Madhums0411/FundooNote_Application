using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly FundoContext fundocontext;
        public CollabController(ICollabBL collabBL, FundoContext fundocontext)
        {
            this.collabBL = collabBL;
            this.fundocontext = fundocontext;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult CollabAdd(long notesId, string email)
        {
            try
            {

                var result = collabBL.AddCollab(notesId, email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Added Colabarator Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Adding Collabarator is Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
