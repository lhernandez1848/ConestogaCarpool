using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public class DriverLogic : IDriverLogic
    {
        private IDriverRepository _driverRepository;

        public DriverLogic(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<Driver> GetSingleDriver(int? vehicleId)
        {
            Driver driver = await _driverRepository.GetSingleDriver(vehicleId);
            return driver;
        }

        public Boolean IsUserDriver(int? userId)
        {
            bool userDriver = _driverRepository.IsUserDriver(userId);

            return userDriver;
        }

        public async Task<List<Driver>> GetDrivers(int? userId)
        {
            List<Driver> drivers = await _driverRepository.GetDrivers(userId);
            return drivers;
        }

        public async Task<Driver> GetDriver(int? driverId)
        {
            Driver driver = await _driverRepository.GetDriver(driverId);

            return driver;
        }

        public void CreateDriver(Driver driver)
        {
            _driverRepository.CreateDriver(driver);
        }

        public void DeleteDriver(int? driverId)
        {
            _driverRepository.DeleteDriver(driverId);
        }

        public async Task Save()
        {
            await _driverRepository.Save();
        }

        public void UpdateDriver(Driver driver)
        {
            _driverRepository.UpdateDriver(driver);
        }

        public bool DriverExists(int id)
        {
            return _driverRepository.DriverExists(id);
        }

    }
}
