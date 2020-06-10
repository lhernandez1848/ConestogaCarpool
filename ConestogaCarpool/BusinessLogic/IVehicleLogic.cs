using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public interface IVehicleLogic
    {
        Task<List<Vehicle>> GetVehiclesOwned(int? userId);
        Task<Vehicle> GetSingleVehicle(int? vehicleId);
        void CreateVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(int? vehicleId);
        Task Save();
        bool VehicleExists(int id);
    }
}
