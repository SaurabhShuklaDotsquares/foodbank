using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class FamilyReportDto
    {
        [DisplayName("Date Added")]
        public DateTime? DateAdded { get; set; }
        [DisplayName("Status")]
        public int? StatusId { get; set; }
        public string FamailyIds { get; set; }
        [DisplayName("Include family member details ")]
        public bool IncludeFamailyMemberDetails { get; set; }
        [DisplayName("Include Parcel details ")]
        public bool IncludeParcelDetails { get; set; }
    }
}
