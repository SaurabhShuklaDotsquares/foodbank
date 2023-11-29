using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ReportFamilyDto
    {
        public ReportFamilyDto()
        {
            FamailyMemberDetails = new List<FamailyMemberDetails>();
            ParcelsDetails = new List<ParcelsDetails>();
            FullAddressDto = new FullAddressDto();
        }
        public string FamilyName { get; set; }
        public string FamilyAddress { get; set; }
        public string NoofAdults { get; set; }
        public string NoofChildren { get; set; }
        public string AddedDate { get; set; }
        public string Branch { get; set; }
        public string NoMember { get; set; }
        public bool IsMemberDetails { get; set; }
        public bool IsParcelDetails  { get; set; }
        public List<FamailyMemberDetails> FamailyMemberDetails { get; set; }
        public List<ParcelsDetails> ParcelsDetails { get; set; }
        public FullAddressDto FullAddressDto { get; set; }
    }

    public class ParcelsDetails
    {
        public string Type { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveredDate { get; set; }
        public string Status { get; set; }
    }

}
