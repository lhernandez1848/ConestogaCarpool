using ConestogaCarpool.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public interface IUserLogic
    {
        Task<List<User>> GetSingleUser(string username);
        Task<User> FindUserId(string username);
        void CreateUser(User user);
        Task<User> GetUser(int? userId);
        Task<User> FindUserById(int? userId);
        Task<User> FindUserByEmail(string email);
        void UpdateUser(User user);
        void UpdateEmailVerification(User user);
        void DeleteUser(int userId);
        Task Save();
        Boolean UserLogin(string Username, string Password);
        Boolean OldPasswordIsMatch(int? UserId, string Password);
        Boolean IsEmailVerified(string Username, string Password);
        Task<User> ShowDetails(int? userId);
        Boolean NotEmpty(User user);
        void SendEmail(string email);
        Boolean VerifyCode(string typedCode);
        bool UserExists(int id);
    }
}
