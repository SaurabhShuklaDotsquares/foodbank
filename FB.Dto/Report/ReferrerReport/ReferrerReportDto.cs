using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class ReferrerReportDto
    {
        [DisplayName("Referrers")]
        public int ReferrersId { get; set; }
        [DisplayName("Include family member details ")]
        public bool IncludeFamailyMemberDetails { get; set; }
        public string FamailyIds { get; set; }
    }
}
