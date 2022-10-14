using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly FundoContext fundocontext;
        public LabelController(ILabelBL labelBL, FundoContext fundocontext)
        {
            this.labelBL = labelBL;
            this.fundocontext = fundocontext;

        }

        [Authorize]
        [HttpPost]
        [Route("CreateLabel")]

        public IActionResult CreateLabel(long NotesId, string LabelName)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var result = labelBL.CreateLabel(UserId, NotesId, LabelName);
            if (result != null)
                return this.Ok(new { success = true, message = "Label created Successfully", data = result });
            else
                return this.BadRequest(new { success = false, message = "Label is not Created" });
        }
        
    }
}
