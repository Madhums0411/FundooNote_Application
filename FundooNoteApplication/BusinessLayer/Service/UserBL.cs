using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserEntity Register(UserRegistration userRegistration)
        {
            try
            {
                return userRL.Register(userRegistration);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLoginModel userLogin)
        {
            try
            {
                return userRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgotPassword(string Email)
        {
            try
            {
                return userRL.ForgotPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string Password, string ConfirmPassword)
        {
            try
            {
                return userRL.ResetPassword(Password, ConfirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}