using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using EASendMail;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ConestogaCarpoolContext _context;

        public UserRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetSingleUser(string username)
        {
            List<User> user = await _context.User.
                Where(m => m.Username == username).
                ToListAsync();
            return user;
        }

        public async Task<User> FindUserId(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(m => m.Username == username);

            return user;
        }

        public void CreateUser(User user)
        {
            user.Password = PasswordHash.HashPassword(user.Password);
            user.VerifiedEmail = "no";

            _context.User.Add(user);
        }
        public async Task<User> GetUser(int? userId)
        {
            var user = await _context.User.FindAsync(userId);

            return user;
        }

        public async Task<User> FindUserById(int? userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == userId);

            return user;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(m => m.Email == email);

            return user;
        }

        public void UpdateUser(User user)
        {
            _context.User.Update(user);
        }

        public void UpdateEmailVerification(User user)
        {
            user.VerifiedEmail = "yes";

            _context.User.Update(user);
        }

        public async void DeleteUser(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            _context.User.Remove(user);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public Boolean OldPasswordIsMatch(int? UserId, string Password)
        {
            var userOldPassword = _context.User.Where(x => x.UserId == UserId
            && PasswordHash.ValidatePassword(x.Password, Password) == true);

            if (!userOldPassword.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean UserLogin(string Username, string Password)
        {
            try
            {
                var conestogaCarpoolContext = _context.User
                        .Where(x => x.Username == Username
                        && PasswordHash.ValidatePassword(x.Password, Password) == true)
                        .Include(u => u.UserId);

                if (!conestogaCarpoolContext.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
                return false;
            }
        }

        public Boolean IsEmailVerified(string Username, string Password)
        {
            try
            {
                var userVerified = _context.User
                       .Where(x => x.Username == Username && x.VerifiedEmail == "yes"
                       && PasswordHash.ValidatePassword(x.Password, Password) == true);

                if (!userVerified.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
                return false;
            }
        }

        public async Task<User> ShowDetails(int? userId)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == userId);

            return user;
        }


        public void SendEmail(string email)
        {
            
        }

        public bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
