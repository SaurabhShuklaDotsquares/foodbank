using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CharityApp
    {
        public int Id { get; set; }
        public int? AppId { get; set; }
        public int? CharityId { get; set; }
        public Guid? ExternalId { get; set; }

        public virtual App App { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
