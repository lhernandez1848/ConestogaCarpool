using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.BusinessLogic
{
    public class ReviewLogic : IReviewLogic
    {
        private IReviewRepository _reviewRepository;

        public ReviewLogic(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<Review>> GetReviews()
        {
            List<Review> reviews = await _reviewRepository.GetReviews();
            return reviews;
        }

        public async Task<List<Review>> GetDriverReviews(int? driverId)
        {
            List<Review> driverReviews = await _reviewRepository.GetDriverReviews(driverId);
            return driverReviews;
        }

        public async Task<List<Review>> GetPassengerReviews(int? passengerId)
        {
            List<Review> passengerReviews = await _reviewRepository.GetPassengerReviews(passengerId);
            return passengerReviews;
        }

        public async Task<Review> GetSingleReview(int? reviewId)
        {
            Review review = await _reviewRepository.GetSingleReview(reviewId);
            return review;
        }

        public void CreateReview(Review review)
        {
            _reviewRepository.CreateReview(review);
        }
        public void UpdateReview(Review review)
        {
            _reviewRepository.UpdateReview(review);
        }

        public void DeleteReview(int? reviewId)
        {
            _reviewRepository.DeleteReview(reviewId);
        }

        public async Task Save()
        {
            await _reviewRepository.Save();
        }

        public bool ReviewExists(int id)
        {
            return _reviewRepository.ReviewExists(id);
        }

        public bool ReviewExistsForRide(int rideId)
        {
            return _reviewRepository.ReviewExists(rideId);
        }
    }
}
