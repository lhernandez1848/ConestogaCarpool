using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConestogaCarpool.Models
{
    public partial class PostStatus
    {
        public PostStatus()
        {
            Post = new HashSet<Post>();
        }

        public int PostStatusId { get; set; }

        [Display(Name = "Post Status")]
        public string PostStatusDescription { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
