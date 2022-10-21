using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelBL labelBL, FundoContext fundocontext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<LabelController> _logger)
        {
            this.labelBL = labelBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }

        [Authorize]
        [HttpPost("Create")]

        public IActionResult CreateLabel(long notesId, string LabelName)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.CreateLabel(UserId, notesId, LabelName);
                if (result != null)
                {
                    _logger.LogInformation("Label created Successfully ");
                    return this.Ok(new { success = true, message = "Label created Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Label is not Created ");
                    return this.BadRequest(new { success = false, message = "Label is not Created" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }

        }
        [Authorize]
        [HttpGet("GetLabel")]

        public IActionResult GetLabel()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.GetLabel(UserId);
                if (result != null)
                {
                    _logger.LogInformation("Label Retrieved Successfully ");
                    return this.Ok(new { success = true, message = "Label Retrieved Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Unable to retrieve Label ");
                    return this.BadRequest(new { success = false, message = "Unable to retrieve Label" });
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
        [Route("Update")]
        public ActionResult UpdateLabel(long labelId, string newLabelName)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.UpdateLabel(labelId, newLabelName);
                if (result != null)
                {
                    _logger.LogInformation("Label Successfully Updated ");
                    return Ok(new { success = true, message = "Label Successfully Updated", data = result });
                }
                else
                {
                    _logger.LogInformation("Label Could Not Be Updated ");
                    return BadRequest(new { success = false, message = "Label Could Not Be Updated" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteLabel(long labelId, long notesId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.LabelDelete(notesId, labelId);
                if (result != null)
                {
                    _logger.LogInformation("Label Successfully Deleted ");
                    return Ok(new { success = true, message = "Label Successfully Deleted", data = result });
                }
                else
                {
                    _logger.LogInformation("Label Could Not Be Deleted ");
                    return BadRequest(new { success = false, message = "Label Could Not Be Deleted" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            try
            {
                var cacheKey = "LabelList";
                string serializedLabelList;
                var labelList = new List<LabelEntity>();
                var redisLabelList = await distributedCache.GetAsync(cacheKey);
                if (redisLabelList != null)
                {
                    _logger.LogInformation("Label Retrived Successfully ");
                    serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                    labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
                }
                else
                {
                    _logger.LogInformation("Label Retrived Unsuccessfully ");
                    labelList = await fundocontext.LabelTable.ToListAsync();
                    serializedLabelList = JsonConvert.SerializeObject(labelList);
                    redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisLabelList, options);
                }
                return Ok(labelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
    }
}
