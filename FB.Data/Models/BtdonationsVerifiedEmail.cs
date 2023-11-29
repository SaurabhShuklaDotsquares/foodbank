using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BtdonationsVerifiedEmail
    {
        public int Id { get; set; }
        public Guid EmailLinkId { get; set; }
        public string Email { get; set; }
        public int? BranchId { get; set; }
        public DateTime? EmailDate { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public int? CharityId { get; set; }
    }
}
