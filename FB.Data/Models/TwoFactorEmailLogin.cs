using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TwoFactorEmailLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Guid { get; set; }
        public bool IsExpire { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SessionId { get; set; }

        public virtual User User { get; set; }
    }
}
