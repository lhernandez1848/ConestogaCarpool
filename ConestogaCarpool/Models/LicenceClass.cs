using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class LicenceClass
    {
        public LicenceClass()
        {
            Driver = new HashSet<Driver>();
        }

        public int LicenceClassId { get; set; }
        public string LicenceClass1 { get; set; }

        public ICollection<Driver> Driver { get; set; }
    }
}
