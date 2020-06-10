using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using EASendMail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository _userRepository;
        public static string genCode = VerificationCodeGenerator();
        public string storeCode = genCode;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetSingleUser(string username)
        {
            var user = await _userRepository.GetSingleUser(username);

            return user;
        }

        public async Task<User> FindUserId(string username)
        {
            var user = await _userRepository.FindUserId(username);

            return user;
        }

        public void CreateUser(User user)
        {
            _userRepository.CreateUser(user);
        }
        
        public async Task<User> GetUser(int? userId)
        {
            var user = await _userRepository.GetUser(userId);

            return user;
        }

        public async Task<User> FindUserById(int? userId)
        {
            var user = await _userRepository.FindUserById(userId);

            return user;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _userRepository.FindUserByEmail(email);

            return user;
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void UpdateEmailVerification(User user)
        {
            _userRepository.UpdateEmailVerification(user);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        public async Task Save()
        {
            await _userRepository.Save();
        }

        public Boolean UserLogin(string Username, string Password)
        {
            bool userLoged = _userRepository.UserLogin(Username, Password);

            return userLoged;
        }

        public Boolean OldPasswordIsMatch(int? UserId, string Password)
        {
            bool passwordMatch = _userRepository.OldPasswordIsMatch(UserId, Password);

            return passwordMatch;
        }

        public Boolean IsEmailVerified(string Username, string Password)
        {
            bool emailVerified = _userRepository.IsEmailVerified(Username, Password);

            return emailVerified;
        }

        public async Task<User> ShowDetails(int? userId)
        {
            var user = await _userRepository.ShowDetails(userId);

            return user;
        }

        public Boolean NotEmpty(User user)
        {
            if ((user.Username == null)
                || (user.FirstName == null)
                || (user.LastName == null)
                || (user.Email == null))
            {
                return false;
            }

            return true;
        }

        public void SendEmail(string email)
        {
            SmtpMail mail = new SmtpMail("TryIt");

            // Your gmail email address
            mail.From = "carpoolconestoga@gmail.com";

            // Set recipient email address
            mail.To = email;

            // Set email subject
            mail.Subject = "Conestoga Carpool -- Verify Email";

            // Set email body
            mail.HtmlBody = @"<h2>Your Verification Code Is: </h2><br /><p>" + genCode + "</p>";

            // Gmail SMTP server address
            SmtpServer server = new SmtpServer("smtp.gmail.com");

            // Gmail user authentication
            // For example: your email is "gmailid@gmail.com", then the user should be the same
            server.User = "carpoolconestoga@gmail.com";
            server.Password = "pucaeqetmwcojzdf";

            // set 587 SSL port
            server.Port = 465;

            // detect SSL/TLS automatically
            server.ConnectType = SmtpConnectType.ConnectSSLAuto;

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(server, mail);
        }

        public Boolean VerifyCode(string typedCode)
        {
            if (storeCode == typedCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string VerificationCodeGenerator()
        {
            string genCode = "";
            Random ranNumber = new Random();
            int ranDigits;

            for (int i = 0; i < 6; i++)
            {
                ranDigits = ranNumber.Next(0, 10);
                genCode += ranDigits.ToString();
            }

            return genCode;
        }
        

        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }
    }
}
