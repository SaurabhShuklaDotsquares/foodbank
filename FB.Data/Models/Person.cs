using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Person
    {
        public Person()
        {
            Address = new HashSet<Address>();
            Attendance = new HashSet<Attendance>();
            Available = new HashSet<Available>();
            BankStatementDonorMapped = new HashSet<BankStatementDonorMapped>();
            Batch = new HashSet<Batch>();
            Communication = new HashSet<Communication>();
            ContactHistory = new HashSet<ContactHistory>();
            ContactLessDonorToken = new HashSet<ContactLessDonorToken>();
            Declaration = new HashSet<Declaration>();
            DeclarationHistory = new HashSet<DeclarationHistory>();
            DeclarationHistoryVerify = new HashSet<DeclarationHistoryVerify>();
            Donation = new HashSet<Donation>();
            DonorFoodbank = new HashSet<DonorFoodbank>();
            Envelope = new HashSet<Envelope>();
            FoodItem = new HashSet<FoodItem>();
            GoCardlessSubscription = new HashSet<GoCardlessSubscription>();
            GroupMemberGroup = new HashSet<GroupMember>();
            GroupMemberPerson = new HashSet<GroupMember>();
            Mmocontact = new HashSet<Mmocontact>();
            Mmocorrespondence = new HashSet<Mmocorrespondence>();
            MmogroupEventAttendance = new HashSet<MmogroupEventAttendance>();
            MmogroupMembers = new HashSet<MmogroupMembers>();
            MmomemberCertificate = new HashSet<MmomemberCertificate>();
            MmomemberRuleOppositePerson = new HashSet<MmomemberRule>();
            MmomemberRulePerson = new HashSet<MmomemberRule>();
            MmomemberSkill = new HashSet<MmomemberSkill>();
            Mmomembership = new HashSet<Mmomembership>();
            Mmonotes = new HashSet<Mmonotes>();
            MmorelationshipMemberOwner = new HashSet<MmorelationshipMember>();
            MmorelationshipMemberRelated = new HashSet<MmorelationshipMember>();
            MmotaskShift = new HashSet<MmotaskShift>();
            MmotaskWillingMember = new HashSet<MmotaskWillingMember>();
            Mmounavailability = new HashSet<Mmounavailability>();
            MmouserDefined = new HashSet<MmouserDefined>();
            Mmovisit = new HashSet<Mmovisit>();
            MmovisitLink = new HashSet<MmovisitLink>();
            PersonCheck = new HashSet<PersonCheck>();
            PersonEvent = new HashSet<PersonEvent>();
            PersonPaymentMethodInfo = new HashSet<PersonPaymentMethodInfo>();
            PersonTask = new HashSet<PersonTask>();
            PledgeDetail = new HashSet<PledgeDetail>();
            RegularGift = new HashSet<RegularGift>();
            TrueLayerStandingOrder = new HashSet<TrueLayerStandingOrder>();
            User = new HashSet<User>();
        }

        public int PersonId { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonTypeId { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int? JustGivingDonorId { get; set; }
        public string Title { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string Initials { get; set; }
        public string Suffix { get; set; }
        public string Hmrcaddress { get; set; }
        public bool HmrcaddressOverride { get; set; }
        public string HomePhone { get; set; }
        public bool IsHomePhoneExt { get; set; }
        public string MobilePhone { get; set; }
        public bool IsMobilePhoneExt { get; set; }
        public string OfficePhone { get; set; }
        public string OfficePhoneExt { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool IsTagged { get; set; }
        public string PreferredGreeting { get; set; }
        public int? DefaultMethodId { get; set; }
        public int? DefaultPurposeId { get; set; }
        public int? DefaultEnvelopeId { get; set; }
        public string Comment { get; set; }
        public byte? Anniversary { get; set; }
        public bool IsDefaultClaimTax { get; set; }
        public decimal? DefaultClaimTax { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
        public bool Deceased { get; set; }
        public DateTime DateModified { get; set; }
        public string Reference { get; set; }
        public string DonorReference { get; set; }
        public int? InitialContactSource { get; set; }
        public int? AdministrativeGroupId { get; set; }
        public string BraintreeCustomerId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool? IsNew { get; set; }
        public bool Overseas { get; set; }
        public long? CardInitialNumber { get; set; }
        public long? CardLastNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsGiftAid { get; set; }
        public bool IsReceiptRequested { get; set; }
        public string DonorReceiptEmail { get; set; }
        public string LinkedCode { get; set; }
        public byte? SourceType { get; set; }
        public string PayReference { get; set; }

        public virtual AdministrativeGroup AdministrativeGroup { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Household Household { get; set; }
        public virtual InitialContact InitialContactSourceNavigation { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual MmopersonAdditonalDetails MmopersonAdditonalDetails { get; set; }
        public virtual PgsdonorContact PgsdonorContact { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<Available> Available { get; set; }
        public virtual ICollection<BankStatementDonorMapped> BankStatementDonorMapped { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
        public virtual ICollection<Communication> Communication { get; set; }
        public virtual ICollection<ContactHistory> ContactHistory { get; set; }
        public virtual ICollection<ContactLessDonorToken> ContactLessDonorToken { get; set; }
        public virtual ICollection<Declaration> Declaration { get; set; }
        public virtual ICollection<DeclarationHistory> DeclarationHistory { get; set; }
        public virtual ICollection<DeclarationHistoryVerify> DeclarationHistoryVerify { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<DonorFoodbank> DonorFoodbank { get; set; }
        public virtual ICollection<Envelope> Envelope { get; set; }
        public virtual ICollection<FoodItem> FoodItem { get; set; }
        public virtual ICollection<GoCardlessSubscription> GoCardlessSubscription { get; set; }
        public virtual ICollection<GroupMember> GroupMemberGroup { get; set; }
        public virtual ICollection<GroupMember> GroupMemberPerson { get; set; }
        public virtual ICollection<Mmocontact> Mmocontact { get; set; }
        public virtual ICollection<Mmocorrespondence> Mmocorrespondence { get; set; }
        public virtual ICollection<MmogroupEventAttendance> MmogroupEventAttendance { get; set; }
        public virtual ICollection<MmogroupMembers> MmogroupMembers { get; set; }
        public virtual ICollection<MmomemberCertificate> MmomemberCertificate { get; set; }
        public virtual ICollection<MmomemberRule> MmomemberRuleOppositePerson { get; set; }
        public virtual ICollection<MmomemberRule> MmomemberRulePerson { get; set; }
        public virtual ICollection<MmomemberSkill> MmomemberSkill { get; set; }
        public virtual ICollection<Mmomembership> Mmomembership { get; set; }
        public virtual ICollection<Mmonotes> Mmonotes { get; set; }
        public virtual ICollection<MmorelationshipMember> MmorelationshipMemberOwner { get; set; }
        public virtual ICollection<MmorelationshipMember> MmorelationshipMemberRelated { get; set; }
        public virtual ICollection<MmotaskShift> MmotaskShift { get; set; }
        public virtual ICollection<MmotaskWillingMember> MmotaskWillingMember { get; set; }
        public virtual ICollection<Mmounavailability> Mmounavailability { get; set; }
        public virtual ICollection<MmouserDefined> MmouserDefined { get; set; }
        public virtual ICollection<Mmovisit> Mmovisit { get; set; }
        public virtual ICollection<MmovisitLink> MmovisitLink { get; set; }
        public virtual ICollection<PersonCheck> PersonCheck { get; set; }
        public virtual ICollection<PersonEvent> PersonEvent { get; set; }
        public virtual ICollection<PersonPaymentMethodInfo> PersonPaymentMethodInfo { get; set; }
        public virtual ICollection<PersonTask> PersonTask { get; set; }
        public virtual ICollection<PledgeDetail> PledgeDetail { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
        public virtual ICollection<TrueLayerStandingOrder> TrueLayerStandingOrder { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
