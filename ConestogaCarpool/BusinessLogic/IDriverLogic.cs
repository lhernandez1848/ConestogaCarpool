using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public interface IDriverLogic
    {
        Task<Driver> GetSingleDriver(int? driverId);
        Boolean IsUserDriver(int? userId);
        Task<List<Driver>> GetDrivers(int? userId);
        Task<Driver> GetDriver(int? driverId);
        void CreateDriver(Driver driver);
        void DeleteDriver(int? driverId);
        void UpdateDriver(Driver driver);
        Task Save();
        bool DriverExists(int id);
    }
}
