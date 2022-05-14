using DatabaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.UserInterface
{
    public interface IUserRL
    {
        public User AddUser(UserPostModel user);
        public string LoginUser(string email, string password);
        public bool ForgetPassword(string email);
        public bool ChangePassword(string emai, PasswordValidation valid);



    }
}
