using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FoodbankSettingDto
    {
        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public string LogoImage { get; set; }
        public string DashboardImage { get; set; }
        public string TailoredNotes { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
        public string ReferralLimit { get; set; }
        public string PhoneNumber { get; set; }
        public string ParcelLimit { get; set; }
    }
}
