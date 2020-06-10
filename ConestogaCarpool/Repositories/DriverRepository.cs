using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private ConestogaCarpoolContext _context;
        public DriverRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        public async Task<Driver> GetSingleDriver(int? driverId)
        {
            Driver driver = await _context.Driver
                .Include(d => d.LicenceClass)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DriverId == driverId);

            return driver;
        }

        public Boolean IsUserDriver(int? userId)
        {
            try
            {
                var userDriver = _context.Driver
                        .Where(x => x.UserId == userId);

                if (!userDriver.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Driver>> GetDrivers(int? userId)
        {
            List<Driver> drivers = await _context.Driver
                .Where(u => u.UserId == userId)
                .Include(d => d.LicenceClass)
                .Include(d => d.User).ToListAsync();

            return drivers;
        }

        public async Task<Driver> GetDriver(int? driverId)
        {
            Driver driver = await _context.Driver.FindAsync(driverId);

            return driver;
        }


        public void CreateDriver(Driver driver)
        {
            _context.Driver.Add(driver);
        }

        public async void DeleteDriver(int? driverId)
        {
            var driver = await _context.Driver.FindAsync(driverId);
            _context.Driver.Remove(driver);
        }

        public void UpdateDriver(Driver driver)
        {
            _context.Driver.Update(driver);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool DriverExists(int id)
        {
            return _context.Driver.Any(e => e.DriverId == id);
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
        // ~VehicleRepository() {
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
