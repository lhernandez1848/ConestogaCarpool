using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;

namespace ConestogaCarpool.BusinessLogic
{
    public class PostLogic : IPostLogic
    {
        private IPostRepository _postRepository;

        public PostLogic(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<List<Post>> GetAllPosts()
        {
            List<Post> posts = await _postRepository.GetAllPosts();
            return posts;

        }

        public async Task<List<Post>> GetAvailableDrivers(string location, string destination)
        {
            List<Post> availableDrivers = await _postRepository.GetAvailableDrivers(location, destination);
            return availableDrivers;
        }

        public async Task<List<Post>> GetDriverPosts(int? driverId)
        {
            List<Post> driverPosts = await _postRepository.GetDriverPosts(driverId);
            return driverPosts;
        }

        public async Task<Post> GetSinglePost(int? postId)
        {
            Post post = await _postRepository.GetSinglePost(postId);
            return post;
        }
        public void CreatePost(Post post)
        {
            _postRepository.CreatePost(post);
        }
        public void UpdatePost(Post post)
        {
            _postRepository.UpdatePost(post);
        }

        public void DeletePost(int? postId)
        {
            _postRepository.DeletePost(postId);
        }

        public async Task Save()
        {
            await _postRepository.Save();
        }

        public bool PostExists(int id)
        {
            return _postRepository.PostExists(id);
        }
    }
}
