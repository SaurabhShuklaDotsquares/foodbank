using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ReportReferrerDto
    {

        public ReportReferrerDto()
        {
            FamailyMemberDetails = new List<FamailyMemberDetails>();
            FullAddressDto = new FullAddressDto();
        }
        public string FamilyName { get; set; }
        public string FamilyAddress { get; set; }
        public string NoofAdults { get; set; }
        public string NoofChildren { get; set; }
        public string AddedDate { get; set; }
        public string Branch { get; set; }
        public string NoMember { get; set; }
        public List<FamailyMemberDetails> FamailyMemberDetails { get; set; }
        public FullAddressDto FullAddressDto { get; set; }
    }
    public class FamailyMemberDetails
    {
        public int FamilyMemberId { get; set; }
        public string FamilyMemberName { get; set; }
        public string Dob { get; set; }
    }

}
