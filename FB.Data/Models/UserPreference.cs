using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class UserPreference
    {
        public int UserId { get; set; }
        public int? DefaultCityId { get; set; }
        public byte? DefaultDonorPage { get; set; }
        public bool? ShowInactiveRecords { get; set; }
        public bool? DisplayStartupForm { get; set; }
        public bool? DisplayGiftReminder { get; set; }
        public bool? ShowReportFinder { get; set; }
        public bool? HideHelpMeasels { get; set; }
        public byte? EmailFormat { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public string Smtpserver { get; set; }
        public string IspaccountName { get; set; }
        public string IspaccountPassword { get; set; }
        public int? Smtpport { get; set; }
        public byte? AuthenticationType { get; set; }
        public bool? IsSslenccyption { get; set; }
        public string TextAccountName { get; set; }
        public string TextPassword { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? NotifyNewDonations { get; set; }
        public bool NotifyDonationsVerificaton { get; set; }
        public bool? ShowInactiveCharityBranch { get; set; }
        public byte? OrderByCharityBranch { get; set; }
        public bool? AutosaveDonor { get; set; }

        public virtual City DefaultCity { get; set; }
        public virtual User User { get; set; }
    }
}
