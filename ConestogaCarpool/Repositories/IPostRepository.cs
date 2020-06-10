using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Repositories
{
    public interface IPostRepository : IDisposable
    {
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetDriverPosts(int? driverId);
        Task<List<Post>> GetAvailableDrivers(string location, string destination);
        Task<Post> GetSinglePost(int? postId);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int? postId);
        Task Save();
        bool PostExists(int id);
    }
}
