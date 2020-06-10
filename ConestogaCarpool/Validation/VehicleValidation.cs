using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool
{
    public class VehicleValidation
    {
        public static bool VehicleYearValidation(int year)
        {
            int _year = DateTime.Now.Year + 1;

            if ((year > 1900) && (year <= _year))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
