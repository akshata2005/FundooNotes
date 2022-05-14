using DatabaseLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.UserInterface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRl : IUserRL
    {
        //Initializing class
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public UserRl(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.Configuration = configuration;
        }

        //Creating method to add user in database
        public User AddUser(UserPostModel user)
        {
            try
            {
                User user1 = new User();
                user1.userID = new User().userID;
                user1.firstName = user.firstName;
                user1.lastName = user.lastName;
                user1.email = user.email;
                user1.password = EncryptPassword(user.password);
                user1.registerdDate = DateTime.Now;
                user1.address = user.address;
                fundo.Users.Add(user1);
                fundo.SaveChanges();
                return user1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Creating method to login user
        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundo.Users.Where(u => u.email == email && u.password == password).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                return GetJWTToken(email, result.userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Generate JwT token
        public static string GetJWTToken(string email, int UserID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId",UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //Creating method to forget password which will return token
        public bool ForgetPassword(string email)
        {
            try
            {
                var result = fundo.Users.FirstOrDefault(u => u.email == email);
                if (result == null)
                {
                    return false;
                }
                // Addd message Queue
                MessageQueue queue;
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    queue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\FundooQueue");
                }
                Message myMessage = new Message();
                myMessage.Formatter = new BinaryMessageFormatter();
                myMessage.Body = GetJWTToken(email, result.userID);
                queue.Send(myMessage);
                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(email, myMessage.Body.ToString());
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                queue.BeginReceive();
                queue.Close();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GetJWTToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        }

        //Creating get jwt token method with only one parameter to get token 
        private string GetJWTToken(string email)
        {

            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email",email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //Creating method to encrypt the password which is entered by user
        public static string EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Creating method to decrypt the password given by user
        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (string.IsNullOrEmpty(encryptedPassword))
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Creating method to change the user password
        public bool ChangePassword(string email, PasswordValidation valid)
        {
            try
            {
                if (valid.newPassword.Equals(valid.confirmPassword))
                {
                    var user = fundo.Users.Where(x => x.email == email).FirstOrDefault();
                    user.password = EncryptPassword(valid.confirmPassword);
                    user.password=DecryptedPassword(valid.confirmPassword);
                    fundo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }

    }
}