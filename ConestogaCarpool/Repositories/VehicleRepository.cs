using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;

namespace ConestogaCarpool.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private ConestogaCarpoolContext _context;

        public VehicleRepository(ConestogaCarpoolContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetSingleVehicle(int? vehicleId)
        {
            Vehicle vehicle = await _context.Vehicle
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VehicleId == vehicleId);

            return vehicle;
        }

        public async Task<List<Vehicle>> GetVehiclesOwned(int? userId)
        {
            List<Vehicle> vehiclesOwned = await _context.Vehicle
                .Where(x => x.UserId == userId).ToListAsync();

            return vehiclesOwned;
        }


        public void CreateVehicle(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
        }

        public async void DeleteVehicle(int? vehicleId)
        {
            Vehicle vehicle = await _context.Vehicle.FindAsync(vehicleId);
            _context.Vehicle.Remove(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicle.Update(vehicle);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.VehicleId == id);
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
