using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConestogaCarpool.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            Request = new HashSet<Request>();
        }

        public int RequestStatusId { get; set; }

        [Display(Name = "Request Status")]
        public string RequestStatusDescription { get; set; }

        public ICollection<Request> Request { get; set; }
    }
}
