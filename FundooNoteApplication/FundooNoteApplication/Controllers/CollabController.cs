using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private readonly ILogger<CollabController> _logger;

        public CollabController(ICollabBL collabBL, FundoContext fundocontext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<CollabController> _logger)
        {
            this.collabBL = collabBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this._logger = _logger;
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
                    _logger.LogInformation("Added Colabarator Successfully");
                    return Ok(new { success = true, message = "Added Colabarator Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Adding Collabarator is Unsuccessfull");
                    return BadRequest(new { success = false, message = "Adding Collabarator is Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public ActionResult CollabDelete(long collabId, string email)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = collabBL.DeleteCollab(collabId, email);
                if (result != null)
                {
                    _logger.LogInformation("Collaborator Successfully Deleted");
                    return Ok(new { success = true, message = "Collaborator Successfully Deleted", data = result });
                }
                else
                {
                    _logger.LogInformation("Collaborator Could Not Be Deleted");
                    return BadRequest(new { success = false, message = "Collaborator Could Not Be Deleted" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpGet("GetCollab")]
        public IActionResult GetCollab()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var result = collabBL.GetCollab(UserId);
            if (result != null)
            {
                _logger.LogInformation("Collabarator Retreived Successfully");
                return this.Ok(new { success = true, message = "Collabarator Retreived Successfully", data = result });
            }
            else
            {
                _logger.LogInformation("Collabarator couldnot be retrieved");
                return this.BadRequest(new { success = false, message = "Collabarator couldnot be retrieved" });
            }

        }
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            try
            {
                var cacheKey = "CollabList";
                string serializedCollabList;
                var collabList = new List<CollabEntity>();
                var redisCollabList = await distributedCache.GetAsync(cacheKey);
                if (redisCollabList != null)
                {
                    _logger.LogInformation("Added Collab Successfully");
                    serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                    collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
                }
                else
                {
                    _logger.LogInformation("Added Collab Successfully");
                    collabList = await fundocontext.CollabTable.ToListAsync();
                    serializedCollabList = JsonConvert.SerializeObject(collabList);
                    redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisCollabList, options);
                }
                return Ok(collabList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
    }
}
