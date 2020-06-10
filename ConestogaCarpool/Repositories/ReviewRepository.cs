using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private ConestogaCarpoolContext _context;

        public ReviewRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviews()
        {
            List<Review> reviews = await _context.Review
                .Include(r => r.Driver)
                .Include(r => r.Passenger)
                .Include(r => r.Ride)
                .ToListAsync();

            return reviews;
        }

        public async Task<List<Review>> GetDriverReviews(int? driverId)
        {
            List<Review> driverReviews = await _context.Review
                .Include(r => r.Driver)
                .Include(r => r.Passenger)
                .Include(r => r.Ride)
                .Include(r => r.Ride.Post)
                .Include(r => r.Ride.Request.Passenger)
                .Where(x => x.Ride.Post.DriverId == driverId)
                .ToListAsync();

            return driverReviews;
        }

        public async Task<List<Review>> GetPassengerReviews(int? passengerId)
        {
            List<Review> passengerReviews = await _context.Review
                .Include(r => r.Driver)
                .Include(r => r.Passenger)
                .Include(r => r.Ride)
                .Include(r => r.Driver.User)
                .Where(x => x.Ride.Request.PassengerId == passengerId)
                .ToListAsync();

            return passengerReviews;
        }

        public async Task<Review> GetSingleReview(int? reviewId)
        {
            Review review = await _context.Review
                .Include(r => r.Driver)
                .Include(r => r.Passenger)
                .Include(r => r.Ride)
                .Include(r => r.Ride.Request)
                .Include(r => r.Driver.User)
                .FirstOrDefaultAsync(m => m.ReviewId == reviewId);

            return review;
        }

        public void CreateReview(Review review)
        {
            _context.Review.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Review.Update(review);
        }

        public async void DeleteReview(int? reviewId)
        {
            Review review = await _context.Review.FindAsync(reviewId);
            _context.Review.Remove(review);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ReviewId == id);
        }

        public bool ReviewExistsForRide(int rideId)
        {
            return _context.Review.Any(e => e.RideId == rideId);
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
        // ~ReviewRepository() {
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
