using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool
{
    public class DriverValidation
    {
        public static bool DriverExperienceValidation(int experience)
        {
            if (experience > 0)
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
