using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
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
                    return Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
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
                    return Ok(new { success = true, message = "Login is Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login is Not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
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
                    return Ok(new { success = true, message = "Email Sent Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Eamil reset Could Not Be Sent" });
                }
            }
            catch (System.Exception)
            {

                throw;
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
                    return Ok(new { success = true, message = "Reset Password is Succesfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Password Link Could Not Be Sent" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}