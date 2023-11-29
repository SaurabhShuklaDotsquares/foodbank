using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Address = new HashSet<Address>();
            AdministrationLetter = new HashSet<AdministrationLetter>();
            AppDataAccessibility = new HashSet<AppDataAccessibility>();
            Batch = new HashSet<Batch>();
            BranchApp = new HashSet<BranchApp>();
            BranchContactPerson = new HashSet<BranchContactPerson>();
            Claim = new HashSet<Claim>();
            ConnectedCharity = new HashSet<ConnectedCharity>();
            ContactLessDonorToken = new HashSet<ContactLessDonorToken>();
            ContactLessSchedule = new HashSet<ContactLessSchedule>();
            ContactLessTerminal = new HashSet<ContactLessTerminal>();
            ContactType = new HashSet<ContactType>();
            Country = new HashSet<Country>();
            Donation = new HashSet<Donation>();
            Envelope = new HashSet<Envelope>();
            EnvelopeSetting = new HashSet<EnvelopeSetting>();
            ExceptionLog = new HashSet<ExceptionLog>();
            FailedBtdonationEmailDetail = new HashSet<FailedBtdonationEmailDetail>();
            Family = new HashSet<Family>();
            Gasds = new HashSet<Gasds>();
            Letter = new HashSet<Letter>();
            Method = new HashSet<Method>();
            MethodAccess = new HashSet<MethodAccess>();
            MmoattendanceCode = new HashSet<MmoattendanceCode>();
            Mmocertificate = new HashSet<Mmocertificate>();
            MmocertificateIssuer = new HashSet<MmocertificateIssuer>();
            MmocontactType = new HashSet<MmocontactType>();
            Mmogroup = new HashSet<Mmogroup>();
            MmogroupType = new HashSet<MmogroupType>();
            MmomaritalStatus = new HashSet<MmomaritalStatus>();
            MmomasterTask = new HashSet<MmomasterTask>();
            MmomembershipCode = new HashSet<MmomembershipCode>();
            MmomembershipEnrolmentForm = new HashSet<MmomembershipEnrolmentForm>();
            MmomembershipType = new HashSet<MmomembershipType>();
            MmorelationshipType = new HashSet<MmorelationshipType>();
            MmoreportLabel = new HashSet<MmoreportLabel>();
            Mmoskill = new HashSet<Mmoskill>();
            MmoskillGroup = new HashSet<MmoskillGroup>();
            MmotaskStatus = new HashSet<MmotaskStatus>();
            MmouserDefinedField = new HashSet<MmouserDefinedField>();
            MmovisitType = new HashSet<MmovisitType>();
            MmowebsiteButton = new HashSet<MmowebsiteButton>();
            Person = new HashSet<Person>();
            PersonType = new HashSet<PersonType>();
            PgsdonorContact = new HashSet<PgsdonorContact>();
            PurposeAccess = new HashSet<PurposeAccess>();
            QuickDonorGift = new HashSet<QuickDonorGift>();
            RegularGift = new HashSet<RegularGift>();
            ReportLabel = new HashSet<ReportLabel>();
            StandardComments = new HashSet<StandardComments>();
            StandingGift = new HashSet<StandingGift>();
            User = new HashSet<User>();
            WebsiteButton = new HashSet<WebsiteButton>();
        }

        public int BranchId { get; set; }
        public int CharityId { get; set; }
        public string ManagerName { get; set; }
        public string BranchDescription { get; set; }
        public bool CommunityBuilding { get; set; }
        public string BranchReference { get; set; }
        public string CommunityName { get; set; }
        public string CommunityAddress { get; set; }
        public string CommunityPostcode { get; set; }
        public byte? Gasdstype { get; set; }
        public byte? FeesType { get; set; }
        public decimal? PercentageFee { get; set; }
        public decimal? FlatRate { get; set; }
        public decimal? Threshold { get; set; }
        public decimal? PercentageGreterThanThreshold { get; set; }
        public decimal? PercentageLessThanThreshold { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDefault { get; set; }
        public string SignatoryEmail { get; set; }
        public string SignatoryPhone { get; set; }
        public string SignatoryAddress { get; set; }
        public string SignatoryPostcode { get; set; }
        public byte ReferenceType { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool IsActive { get; set; }
        public string ContactEmail { get; set; }
        public string Cpkclient { get; set; }
        public string Notes { get; set; }
        public bool? IsMgo { get; set; }
        public bool IsMmo { get; set; }
        public string PacReference { get; set; }
        public string ParishFundName { get; set; }
        public bool? IsMmosystemCreated { get; set; }
        public byte? SourceType { get; set; }
        public string Mccpkorganization { get; set; }
        public string MailChimpToken { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual BranchToken BranchToken { get; set; }
        public virtual MmobranchToken MmobranchToken { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<AdministrationLetter> AdministrationLetter { get; set; }
        public virtual ICollection<AppDataAccessibility> AppDataAccessibility { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
        public virtual ICollection<BranchApp> BranchApp { get; set; }
        public virtual ICollection<BranchContactPerson> BranchContactPerson { get; set; }
        public virtual ICollection<Claim> Claim { get; set; }
        public virtual ICollection<ConnectedCharity> ConnectedCharity { get; set; }
        public virtual ICollection<ContactLessDonorToken> ContactLessDonorToken { get; set; }
        public virtual ICollection<ContactLessSchedule> ContactLessSchedule { get; set; }
        public virtual ICollection<ContactLessTerminal> ContactLessTerminal { get; set; }
        public virtual ICollection<ContactType> ContactType { get; set; }
        public virtual ICollection<Country> Country { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<Envelope> Envelope { get; set; }
        public virtual ICollection<EnvelopeSetting> EnvelopeSetting { get; set; }
        public virtual ICollection<ExceptionLog> ExceptionLog { get; set; }
        public virtual ICollection<FailedBtdonationEmailDetail> FailedBtdonationEmailDetail { get; set; }
        public virtual ICollection<Family> Family { get; set; }
        public virtual ICollection<Gasds> Gasds { get; set; }
        public virtual ICollection<Letter> Letter { get; set; }
        public virtual ICollection<Method> Method { get; set; }
        public virtual ICollection<MethodAccess> MethodAccess { get; set; }
        public virtual ICollection<MmoattendanceCode> MmoattendanceCode { get; set; }
        public virtual ICollection<Mmocertificate> Mmocertificate { get; set; }
        public virtual ICollection<MmocertificateIssuer> MmocertificateIssuer { get; set; }
        public virtual ICollection<MmocontactType> MmocontactType { get; set; }
        public virtual ICollection<Mmogroup> Mmogroup { get; set; }
        public virtual ICollection<MmogroupType> MmogroupType { get; set; }
        public virtual ICollection<MmomaritalStatus> MmomaritalStatus { get; set; }
        public virtual ICollection<MmomasterTask> MmomasterTask { get; set; }
        public virtual ICollection<MmomembershipCode> MmomembershipCode { get; set; }
        public virtual ICollection<MmomembershipEnrolmentForm> MmomembershipEnrolmentForm { get; set; }
        public virtual ICollection<MmomembershipType> MmomembershipType { get; set; }
        public virtual ICollection<MmorelationshipType> MmorelationshipType { get; set; }
        public virtual ICollection<MmoreportLabel> MmoreportLabel { get; set; }
        public virtual ICollection<Mmoskill> Mmoskill { get; set; }
        public virtual ICollection<MmoskillGroup> MmoskillGroup { get; set; }
        public virtual ICollection<MmotaskStatus> MmotaskStatus { get; set; }
        public virtual ICollection<MmouserDefinedField> MmouserDefinedField { get; set; }
        public virtual ICollection<MmovisitType> MmovisitType { get; set; }
        public virtual ICollection<MmowebsiteButton> MmowebsiteButton { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<PersonType> PersonType { get; set; }
        public virtual ICollection<PgsdonorContact> PgsdonorContact { get; set; }
        public virtual ICollection<PurposeAccess> PurposeAccess { get; set; }
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
        public virtual ICollection<ReportLabel> ReportLabel { get; set; }
        public virtual ICollection<StandardComments> StandardComments { get; set; }
        public virtual ICollection<StandingGift> StandingGift { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<WebsiteButton> WebsiteButton { get; set; }
    }
}
