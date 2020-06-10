using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class UserImage
    {
        public int UserImageId { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }

        public User User { get; set; }
    }
}
