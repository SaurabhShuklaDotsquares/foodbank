using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CheckType
    {
        public CheckType()
        {
            Checked = new HashSet<Checked>();
        }

        public int CheckTypeId { get; set; }
        public string CheckTypeDescription { get; set; }
        public string CheckAuthority { get; set; }

        public virtual ICollection<Checked> Checked { get; set; }
    }
}
