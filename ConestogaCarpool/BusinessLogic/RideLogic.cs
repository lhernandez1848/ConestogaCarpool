using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;

namespace ConestogaCarpool.BusinessLogic
{
    public class RideLogic : IRideLogic
    {
        private IRideRepository _rideRepository;

        public RideLogic(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        public async Task<List<Ride>> GetAllRides()
        {
            List<Ride> rides = await _rideRepository.GetAllRides();
            return rides;
        }

        public async Task<List<Ride>> GetDriverRides(int? driverId)
        {
            List<Ride> driverHistory = await _rideRepository.GetDriverRides(driverId);
            return driverHistory;

        }

        public async Task<List<Ride>> GetPassengerRides(int? passengerId)
        {
            List<Ride> passengerHistory = await _rideRepository.GetPassengerRides(passengerId);
            return passengerHistory;
        }

        public async Task<Ride> GetSingleRide(int? rideId)
        {
            Ride rideInfo = await _rideRepository.GetSingleRide(rideId);
            return rideInfo;
        }

        public void CreateRide(Ride ride)
        {
            _rideRepository.CreateRide(ride);
        }

        public void UpdateRide(Ride ride)
        {
            _rideRepository.UpdateRide(ride);
        }

        public void DeleteRide(int? rideId)
        {
            _rideRepository.DeleteRide(rideId);
        }

        public async Task Save()
        {
            await _rideRepository.Save();
        }

        public bool RideExists(int id)
        {
            return _rideRepository.RideExists(id);
        }
    }
}
