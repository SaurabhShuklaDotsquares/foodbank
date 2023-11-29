using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class App
    {
        public App()
        {
            AppClaimRequest = new HashSet<AppClaimRequest>();
            AppDataAccessibility = new HashSet<AppDataAccessibility>();
            AppDonationXref = new HashSet<AppDonationXref>();
            AppLoginToken = new HashSet<AppLoginToken>();
            BranchApp = new HashSet<BranchApp>();
            CentralOfficeApp = new HashSet<CentralOfficeApp>();
            CharityApp = new HashSet<CharityApp>();
        }

        public int AppId { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AppName { get; set; }
        public Guid? PublicKey { get; set; }
        public Guid? SecretKey { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public string RedirectUri { get; set; }
        public string AppImage { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual ICollection<AppClaimRequest> AppClaimRequest { get; set; }
        public virtual ICollection<AppDataAccessibility> AppDataAccessibility { get; set; }
        public virtual ICollection<AppDonationXref> AppDonationXref { get; set; }
        public virtual ICollection<AppLoginToken> AppLoginToken { get; set; }
        public virtual ICollection<BranchApp> BranchApp { get; set; }
        public virtual ICollection<CentralOfficeApp> CentralOfficeApp { get; set; }
        public virtual ICollection<CharityApp> CharityApp { get; set; }
    }
}
