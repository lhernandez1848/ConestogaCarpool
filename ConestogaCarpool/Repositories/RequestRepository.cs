using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private ConestogaCarpoolContext _context;
        public RequestRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        // NOTE: Filter by driver once user accounts are created
        public async Task<List<Request>> GetAllRequests()
        {
            List<Request> requests = await _context.Request
                .Include(r => r.Passenger)
                .Include(r => r.Post)
                .Include(r => r.RequestStatus)
                .Where(r => r.Post.PostStatusId == 1)
                .ToListAsync();

            return requests;
        }

        public async Task<List<Request>> GetDriverRequests(int? driverId)
        {
            List<Request> driverRequests = await _context.Request
                .Include(r => r.Passenger)
                .Include(r => r.Post)
                .Include(r => r.RequestStatus)
                .Where(x => x.Post.DriverId == driverId)
                .Where(b => b.RequestStatusId == 4)
                .OrderByDescending(y => y.RequestId)
                .ToListAsync();

            return driverRequests;
        }

        public async Task<List<Request>> GetPassengerRequests(int? passengerId)
        {
            List<Request> passengerRequests = await _context.Request
                .Include(r => r.Passenger)
                .Include(r => r.Post)
                .Include(r => r.RequestStatus)
                .Include(r => r.Post.Driver.User)
                .Where(x => x.PassengerId == passengerId)
                .OrderByDescending(y => y.RequestId)
                .ToListAsync();

            return passengerRequests;
        }

        public async Task<Request> GetSingleRequest(int? requestId)
        {
            Request request = await _context.Request
                .Include(r => r.Passenger)
                .Include(r => r.Post)
                .Include(r => r.RequestStatus)
                .Include(r => r.Post.Driver.User)
                .FirstOrDefaultAsync(m => m.RequestId == requestId);

            return request;
        }

        public async Task<Request> GetPost(int? postId)
        {
            Request request = await _context.Request
                .Include(r => r.Passenger)
                .Include(r => r.Post)
                .Include(r => r.RequestStatus)
                .Include(r => r.Post.Driver.User)
                .FirstOrDefaultAsync(m => m.PostId == postId);

            return request;
        }

        public void CreateRequest(Request request)
        {
            _context.Request.Add(request);
        }

        public void UpdateRequest(Request request)
        {
            _context.Request.Update(request);
        }

        public void AcceptRequest(Request request)
        {
            // Update PostStatus and RequestStatus
            request.Post.PostStatusId = 2;
            request.RequestStatusId = 1;

            _context.Request.Update(request);
        }

        public void DeclineRequest(Request request)
        {
            // Update PostStatus and RequestStatus
            request.Post.PostStatusId = 2;
            request.RequestStatusId = 2;

            _context.Request.Update(request);
        }

        public async void DeleteRequest(int? requestId)
        {
            Request request = await _context.Request.FindAsync(requestId);
            _context.Request.Remove(request);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.RequestId == id);
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
        // ~RequestRepository() {
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
