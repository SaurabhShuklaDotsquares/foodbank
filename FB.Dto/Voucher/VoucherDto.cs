using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class VoucherDto
    {
        public int FoodBankId { get; set; }
        public int VoucherId { get; set; }
        public int ParcelTypeId { get; set; }
        public string VoucherToken { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModfiedDate { get; set; }
        public DateTime? RedeemedDate { get; set; }
        public int ReferrerId { get; set; }
        public string ReferrerName { get; set; }
        public int FamilyId { get; set; }
        public int hdnFamilyId { get; set; }
        public string FamilyName { get; set; }
    }
}
