using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Register(UserRegistration userRegistration);
        public string Login(UserLoginModel userLogin);

        public string ForgotPassword(string Email);
    }
}
