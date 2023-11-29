using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Checked
    {
        public Checked()
        {
            PersonCheck = new HashSet<PersonCheck>();
        }

        public int CheckedId { get; set; }
        public DateTime? DateChecked { get; set; }
        public int? CheckType { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? MustBeRenewed { get; set; }

        public virtual CheckType CheckTypeNavigation { get; set; }
        public virtual ICollection<PersonCheck> PersonCheck { get; set; }
    }
}
