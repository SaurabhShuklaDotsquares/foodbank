using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class MMOUserDataAccessibilityDto
    {
        public int UserAccessID { get; set; }

        public int UserID { get; set; }

        [DisplayName("Charity :")]
        public string CharityName { get; set; }

        public int BranchID { get; set; }

        [DisplayName("User Full Name :")]
        public string UserFullName { get; set; }

        [DisplayName("Organisation")]
        public int CentralOfficeID { get; set; }

        [DisplayName("Organisation :")]
        public string CentralOfficeName { get; set; }

        public string BranchesId { get; set; }

        [DisplayName("Allow all data to be accessed (if new data will be inserted in future)")]
        public bool IsFullAccess { get; set; }

        [DisplayName("Is View Private Notes")]
        public bool IsPrivateNotesAccess { get; set; }
    }
    public class FoodbankUserDataAccessibilityDto
    {
        public int UserAccessID { get; set; }

        public int UserID { get; set; }

        [DisplayName("Charity :")]
        public string CharityName { get; set; }

        public int BranchID { get; set; }

        [DisplayName("User Full Name :")]
        public string UserFullName { get; set; }

        [DisplayName("Organisation")]
        public int CentralOfficeID { get; set; }

        [DisplayName("Organisation :")]
        public string CentralOfficeName { get; set; }

        public string BranchesId { get; set; }

        [DisplayName("Allow all data to be accessed (if new data will be inserted in future)")]
        public bool IsFullAccess { get; set; }

        [DisplayName("Is View Private Notes")]
        public bool IsPrivateNotesAccess { get; set; }
    }
}
