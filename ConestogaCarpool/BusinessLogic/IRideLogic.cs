using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public interface IRideLogic
    {
        Task<List<Ride>> GetAllRides();
        Task<List<Ride>> GetDriverRides(int? driverId);
        Task<List<Ride>> GetPassengerRides(int? passengerId);
        Task<Ride> GetSingleRide(int? rideId);
        void CreateRide(Ride ride);
        void UpdateRide(Ride ride);
        void DeleteRide(int? rideId);
        Task Save();
        bool RideExists(int id);
    }
}
