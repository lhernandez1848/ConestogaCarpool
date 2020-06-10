using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.Repositories
{
    public class RideRepository : IRideRepository
    {
        private ConestogaCarpoolContext _context;
        public RideRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        public async Task<List<Ride>> GetAllRides()
        {
            List<Ride> rides = await _context.Ride
                .Include(r => r.Post)
                .Include(r => r.Request)
                .Include(r => r.RideStatus)
                .Include(r => r.Post.Driver.User)
                .ToListAsync();

            return rides;
        }

        public async Task<List<Ride>> GetDriverRides(int? driverId)
        {
            List<Ride> driverHistory = await _context.Ride
                .Include(r => r.Post)
                .Include(r => r.Request)
                .Include(r => r.RideStatus)
                .Include(r => r.Request.Passenger)
                .Where(x => x.Post.DriverId == driverId)
                .ToListAsync();

            return driverHistory;
        }

        public async Task<List<Ride>> GetPassengerRides(int? passengerId)
        {
            List<Ride> passengerHistory = await _context.Ride
                .Include(r => r.Post)
                .Include(r => r.Request)
                .Include(r => r.RideStatus)
                .Include(r => r.Post.Driver.User)
                .Where(x => x.Request.PassengerId == passengerId)
                .ToListAsync();

            return passengerHistory;
        }

        public async Task<Ride> GetSingleRide(int? rideId)
        {
            Ride ride = await _context.Ride
                .Include(r => r.Post)
                .Include(r => r.Request)
                .Include(r => r.RideStatus)
                .Include(r => r.Post.Driver.User)
                .Include(r => r.Request.Passenger)
                .FirstOrDefaultAsync(m => m.RideId == rideId);

            return ride;
        }

        public void CreateRide(Ride ride)
        {
            _context.Ride.Add(ride);
        }

        public void UpdateRide(Ride ride)
        {
            _context.Ride.Update(ride);
        }

        public async void DeleteRide(int? rideId)
        {
            Ride ride = await _context.Ride.FindAsync(rideId);
            _context.Ride.Remove(ride);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool RideExists(int id)
        {
            return _context.Ride.Any(e => e.RideId == id);
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
        // ~RideRepository() {
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
