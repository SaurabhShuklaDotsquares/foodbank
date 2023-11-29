using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GoCardlessNotification
    {
        public int NotificationId { get; set; }
        public string EventId { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int? ResourceType { get; set; }
        public int? ResourceTypeAction { get; set; }
        public string ResourceTypeId { get; set; }
        public string Origin { get; set; }
        public int? Cause { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public bool? IsNew { get; set; }
        public int? CharityId { get; set; }
        public string Mandate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual Charity Charity { get; set; }
    }
}
