using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegistration userRegistration);
        public string Login(UserLoginModel userLogin);

        public string ForgotPassword(string Email);
    }
}