using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool
{
    public class PostValidation
    {
        public static bool PostDateValidation(DateTime date)
        {
            DateTime today = DateTime.Today;

            if (date == today)
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
