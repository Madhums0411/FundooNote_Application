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
        public bool ResetPassword(string Password, string ConfirmPassword);
        public string EncryptPassword(string password);
        public string Decrypt(string base64EncodedData);
    }
}
