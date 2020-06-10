using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class VehicleImage
    {
        public int VehicleImageId { get; set; }
        public int VehicleId { get; set; }
        public byte[] Image { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
