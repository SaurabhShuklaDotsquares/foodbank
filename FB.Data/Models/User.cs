using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class User
    {
        public User()
        {
            AgentCreatedByNavigation = new HashSet<Agent>();
            AgentUser = new HashSet<Agent>();
            AgentUserNavigation = new HashSet<AgentUser>();
            AppLoginToken = new HashSet<AppLoginToken>();
            BankStatementDonorMapped = new HashSet<BankStatementDonorMapped>();
            BankStatementTemplate = new HashSet<BankStatementTemplate>();
            BankTemplateMaster = new HashSet<BankTemplateMaster>();
            Branch = new HashSet<Branch>();
            CentralOffice = new HashSet<CentralOffice>();
            ContactLessDonorToken = new HashSet<ContactLessDonorToken>();
            ExceptionLog = new HashSet<ExceptionLog>();
            FamilyNotes = new HashSet<FamilyNotes>();
            Fbaddress = new HashSet<Fbaddress>();
            FoodbankInsertByNavigation = new HashSet<Foodbank>();
            FoodbankUser = new HashSet<Foodbank>();
            FoodbankUserDataAccessibility = new HashSet<FoodbankUserDataAccessibility>();
            ForgotPassword = new HashSet<ForgotPassword>();
            Log = new HashSet<Log>();
            Mmonotes = new HashSet<Mmonotes>();
            MmouserDataAccessibility = new HashSet<MmouserDataAccessibility>();
            MmowebsiteButton = new HashSet<MmowebsiteButton>();
            MyGivingTaskUserSeen = new HashSet<MyGivingTaskUserSeen>();
            QuickDonorGift = new HashSet<QuickDonorGift>();
            Referrers = new HashSet<Referrers>();
            TwoFactorEmailLogin = new HashSet<TwoFactorEmailLogin>();
            UserDataAccessibility = new HashSet<UserDataAccessibility>();
            Volunteer = new HashSet<Volunteer>();
            WebsiteButton = new HashSet<WebsiteButton>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PrimaryMobile { get; set; }
        public string AlternateMobile { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public byte Gender { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public int? StatusId { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public bool IsBlockedBySuperAdmin { get; set; }
        public int? CreatedBy { get; set; }
        public string Ip { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int? PersonId { get; set; }
        public int? CentralOfficeId { get; set; }
        public string CityName { get; set; }
        public string Postcode { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }
        public int? LoginAttempt { get; set; }
        public DateTime? LoginAttemptDate { get; set; }
        public string Cofeguid { get; set; }
        public bool? IsMgo { get; set; }
        public bool IsMmo { get; set; }
        public bool? IsMmosystemCreated { get; set; }
        public string MsuserObjectId { get; set; }
        public string TenantId { get; set; }
        public bool? IsTeamManager { get; set; }
        public string MsuserPrincipalName { get; set; }
        public bool IsPrivateNotesAccess { get; set; }
        public bool IsTfa { get; set; }
        public int? FoodbankId { get; set; }
        public bool IsFoodbank { get; set; }

        public virtual Branch BranchNavigation { get; set; }
        public virtual CentralOffice CentralOfficeNavigation { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual Person Person { get; set; }
        public virtual UserPreference UserPreference { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Agent> AgentCreatedByNavigation { get; set; }
        public virtual ICollection<Agent> AgentUser { get; set; }
        public virtual ICollection<AgentUser> AgentUserNavigation { get; set; }
        public virtual ICollection<AppLoginToken> AppLoginToken { get; set; }
        public virtual ICollection<BankStatementDonorMapped> BankStatementDonorMapped { get; set; }
        public virtual ICollection<BankStatementTemplate> BankStatementTemplate { get; set; }
        public virtual ICollection<BankTemplateMaster> BankTemplateMaster { get; set; }
        public virtual ICollection<Branch> Branch { get; set; }
        public virtual ICollection<CentralOffice> CentralOffice { get; set; }
        public virtual ICollection<ContactLessDonorToken> ContactLessDonorToken { get; set; }
        public virtual ICollection<ExceptionLog> ExceptionLog { get; set; }
        public virtual ICollection<FamilyNotes> FamilyNotes { get; set; }
        public virtual ICollection<Fbaddress> Fbaddress { get; set; }
        public virtual ICollection<Foodbank> FoodbankInsertByNavigation { get; set; }
        public virtual ICollection<Foodbank> FoodbankUser { get; set; }
        public virtual ICollection<FoodbankUserDataAccessibility> FoodbankUserDataAccessibility { get; set; }
        public virtual ICollection<ForgotPassword> ForgotPassword { get; set; }
        public virtual ICollection<Log> Log { get; set; }
        public virtual ICollection<Mmonotes> Mmonotes { get; set; }
        public virtual ICollection<MmouserDataAccessibility> MmouserDataAccessibility { get; set; }
        public virtual ICollection<MmowebsiteButton> MmowebsiteButton { get; set; }
        public virtual ICollection<MyGivingTaskUserSeen> MyGivingTaskUserSeen { get; set; }
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual ICollection<Referrers> Referrers { get; set; }
        public virtual ICollection<TwoFactorEmailLogin> TwoFactorEmailLogin { get; set; }
        public virtual ICollection<UserDataAccessibility> UserDataAccessibility { get; set; }
        public virtual ICollection<Volunteer> Volunteer { get; set; }
        public virtual ICollection<WebsiteButton> WebsiteButton { get; set; }
    }
}
