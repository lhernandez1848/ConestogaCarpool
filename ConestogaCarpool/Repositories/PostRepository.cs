using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.Repositories
{
    public class PostRepository : IPostRepository
    {
        private ConestogaCarpoolContext _context;
        public PostRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            List<Post> posts = await _context.Post
                .Include(p => p.Driver)
                .Include(p => p.PostStatus)
                .Include(p => p.Vehicle)
                .ToListAsync();

            return posts;
        }

        public async Task<List<Post>> GetAvailableDrivers(string location, string destination)
        {
            List<Post> availableDrivers = await _context.Post
                .Include(p => p.Driver)
                .Include(p => p.PostStatus)
                .Include(p => p.Vehicle)
                .Include(p => p.Driver.User)
                .Where(p => p.Location == location && p.Destination == destination)
                .ToListAsync();

            return availableDrivers;
        }

        public async Task<List<Post>> GetDriverPosts(int? driverId)
        {
            List<Post> driverPosts = await _context.Post
                .Include(p => p.Driver)
                .Include(p => p.PostStatus)
                .Include(p => p.Vehicle)
                .Include(p => p.Driver.User)
                .Where(x => x.DriverId == driverId)
                .ToListAsync();

            return driverPosts;
        }

        public async Task<Post> GetSinglePost(int? postId)
        {
            Post post = await _context.Post
                .Include(p => p.Driver)
                .Include(p => p.PostStatus)
                .Include(p => p.Vehicle)
                .Include(p => p.Driver.User)
                .Include(p => p.Driver.LicenceClass)
                .FirstOrDefaultAsync(m => m.PostId == postId);

            return post;
        }

        public void CreatePost(Post post)
        {
            _context.Post.Add(post);
        }
        public void UpdatePost(Post post)
        {
            _context.Post.Update(post);
        }

        public async void DeletePost(int? postId)
        {
            Post post = await _context.Post.FindAsync(postId);
            _context.Post.Remove(post);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
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
        // ~PostRepository() {
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
