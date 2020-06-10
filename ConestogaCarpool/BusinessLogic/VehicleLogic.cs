using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public class VehicleLogic : IVehicleLogic
    {
        private IVehicleRepository _vehicleRepository;

        public VehicleLogic(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> GetSingleVehicle(int? vehicleId)
        {
            Vehicle vehicle = await _vehicleRepository.GetSingleVehicle(vehicleId);
            return vehicle;
        }

        public async Task<List<Vehicle>> GetVehiclesOwned(int? userId)
        {
            try
            {
                if (userId <= 0)
                    throw new Exception("invalid UserId");

                // Get list of vehicles
                List<Vehicle> vehiclesOwned = await _vehicleRepository.GetVehiclesOwned(userId);

                return vehiclesOwned;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CreateVehicle(Vehicle vehicle)
        {
            _vehicleRepository.CreateVehicle(vehicle);
        }

        public void DeleteVehicle(int? vehicleId)
        {
            _vehicleRepository.DeleteVehicle(vehicleId);
        }

        public async Task Save()
        {
            await _vehicleRepository.Save();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _vehicleRepository.UpdateVehicle(vehicle);
        }

        public bool VehicleExists(int id)
        {
            return _vehicleRepository.VehicleExists(id);
        }
    }
}
