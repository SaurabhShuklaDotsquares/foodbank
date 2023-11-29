using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Dcusers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
