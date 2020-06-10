using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Repositories
{
    public interface IVehicleRepository : IDisposable
    {
        Task<Vehicle> GetSingleVehicle(int? vehicleId);
        Task<List<Vehicle>> GetVehiclesOwned(int? userId);
        void CreateVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(int? vehicleId);
        Task Save();
        bool VehicleExists(int id);
    }
}
