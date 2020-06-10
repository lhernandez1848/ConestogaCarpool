using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Repositories
{
    public interface IUserRepository : IDisposable
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
        bool UserExists(int id);
    }
}
