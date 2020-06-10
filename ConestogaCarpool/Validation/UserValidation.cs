using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool
{
    public class UserValidation
    {
        public static bool UserEmailValidation(string email)
        {
            if (email.Contains("@conestogac.on.ca"))
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
