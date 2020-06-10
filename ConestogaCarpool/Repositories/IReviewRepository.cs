using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Repositories
{
    public interface IReviewRepository : IDisposable
    {
        Task<List<Review>> GetReviews();
        Task<List<Review>> GetDriverReviews(int? driverId);
        Task<List<Review>> GetPassengerReviews(int? passengerId);
        Task<Review> GetSingleReview(int? reviewId);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int? reviewId);
        Task Save();
        bool ReviewExists(int id);
        bool ReviewExistsForRide(int rideId);
    }
}
