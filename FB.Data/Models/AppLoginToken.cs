using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AppLoginToken
    {
        public int Id { get; set; }
        public int? AppId { get; set; }
        public int? UserId { get; set; }
        public byte CallType { get; set; }
        public string LoginToken { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual App App { get; set; }
        public virtual User User { get; set; }
    }
}
