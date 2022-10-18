using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBL userBL, ILogger<UserController> _logger)
        {
            this.userBL = userBL;
            this._logger = _logger;
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Registration(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Register(userRegistration);
                if (result != null)
                {
                    _logger.LogInformation("Registration Succesfull");
                    return Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    _logger.LogInformation("Unsuccessfull");
                    return NotFound(new { success = false, message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserLoginModel userLogin)
        {
            try
            {
                var result = userBL.Login(userLogin);
                if (result != null)
                {
                    _logger.LogInformation("Login is Successfull");
                    return Ok(new { success = true, message = "Login is Successfull", data = result });
                }
                else
                {
                    _logger.LogInformation("Login is Not Successfull");
                    return BadRequest(new { success = false, message = "Login is Not Successfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                var result = userBL.ForgotPassword(Email);
                if (result != null)
                {
                    _logger.LogInformation("Email Sent Successfully");
                    return Ok(new { success = true, message = "Email Sent Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Email reset Could Not Be Sent");
                    return BadRequest(new { success = false, message = "Email reset Could Not Be Sent" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(string Password, string ConfirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                if (userBL.ResetPassword(Password, ConfirmPassword))
                {
                    _logger.LogInformation("Reset Password is Succesfull");
                    return Ok(new { success = true, message = "Reset Password is Succesfull" });
                }
                else
                {
                    _logger.LogInformation("Reset Password Link Could Not Be Sent");
                    return BadRequest(new { success = false, message = "Reset Password Link Could Not Be Sent" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}