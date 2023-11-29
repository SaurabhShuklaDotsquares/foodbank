using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CentralOffice
    {
        public CentralOffice()
        {
            Address = new HashSet<Address>();
            AdministrationLetter = new HashSet<AdministrationLetter>();
            Agent = new HashSet<Agent>();
            App = new HashSet<App>();
            AppDataAccessibility = new HashSet<AppDataAccessibility>();
            Audit = new HashSet<Audit>();
            BankTemplateMaster = new HashSet<BankTemplateMaster>();
            CentralOfficeApp = new HashSet<CentralOfficeApp>();
            Charity = new HashSet<Charity>();
            City = new HashSet<City>();
            Communication = new HashSet<Communication>();
            ConnectedCharity = new HashSet<ConnectedCharity>();
            ContactType = new HashSet<ContactType>();
            Country = new HashSet<Country>();
            County = new HashSet<County>();
            DataDevelopmentProcessFeeDetails = new HashSet<DataDevelopmentProcessFeeDetails>();
            DataImportLog = new HashSet<DataImportLog>();
            Envelope = new HashSet<Envelope>();
            EnvelopeSetting = new HashSet<EnvelopeSetting>();
            ExceptionLog = new HashSet<ExceptionLog>();
            Family = new HashSet<Family>();
            Letter = new HashSet<Letter>();
            Licence = new HashSet<Licence>();
            Method = new HashSet<Method>();
            MmoattendanceCode = new HashSet<MmoattendanceCode>();
            Mmocertificate = new HashSet<Mmocertificate>();
            MmocertificateIssuer = new HashSet<MmocertificateIssuer>();
            MmocontactType = new HashSet<MmocontactType>();
            MmodataImportLog = new HashSet<MmodataImportLog>();
            Mmogroup = new HashSet<Mmogroup>();
            MmogroupType = new HashSet<MmogroupType>();
            Mmolicence = new HashSet<Mmolicence>();
            MmomaritalStatus = new HashSet<MmomaritalStatus>();
            MmomasterTask = new HashSet<MmomasterTask>();
            MmomembershipCode = new HashSet<MmomembershipCode>();
            MmomembershipEnrolmentForm = new HashSet<MmomembershipEnrolmentForm>();
            MmomembershipType = new HashSet<MmomembershipType>();
            Mmomodules = new HashSet<Mmomodules>();
            MmorelationshipType = new HashSet<MmorelationshipType>();
            MmoreportLabel = new HashSet<MmoreportLabel>();
            Mmoskill = new HashSet<Mmoskill>();
            MmoskillGroup = new HashSet<MmoskillGroup>();
            MmotaskStatus = new HashSet<MmotaskStatus>();
            MmouserDefinedField = new HashSet<MmouserDefinedField>();
            MmovisitType = new HashSet<MmovisitType>();
            OrganizationLicenseHistory = new HashSet<OrganizationLicenseHistory>();
            OrganizationMmolicenseHistory = new HashSet<OrganizationMmolicenseHistory>();
            Person = new HashSet<Person>();
            PersonType = new HashSet<PersonType>();
            PgsdonorContact = new HashSet<PgsdonorContact>();
            Purpose = new HashSet<Purpose>();
            QuickDonorGift = new HashSet<QuickDonorGift>();
            ReportLabel = new HashSet<ReportLabel>();
            SmartFilter = new HashSet<SmartFilter>();
            StandardComments = new HashSet<StandardComments>();
            TaxYear = new HashSet<TaxYear>();
            User = new HashSet<User>();
        }

        public int CentralOfficeId { get; set; }
        public string OrganisationName { get; set; }
        public DateTime? DateJoined { get; set; }
        public bool? Active { get; set; }
        public int? LicenceValidYears { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsFeesPerBranch { get; set; }
        public DateTime? LicenceEndDate { get; set; }
        public string SerialNumber { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool IsDemoClaim { get; set; }
        public bool IsTestPayment { get; set; }
        public bool IsVerifyDonation { get; set; }
        public int? StandingOrderId { get; set; }
        public DateTime? MmolicenceEndDate { get; set; }
        public bool? IsMgo { get; set; }
        public bool IsMmo { get; set; }
        public bool? IsTrueLayerEnable { get; set; }
        public bool? IsMmosystemCreated { get; set; }
        public string TenantId { get; set; }
        public Guid? PublicKey { get; set; }
        public Guid? SecretKey { get; set; }
        public bool? HasAppPermission { get; set; }
        public bool? MsadminConsent { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<AdministrationLetter> AdministrationLetter { get; set; }
        public virtual ICollection<Agent> Agent { get; set; }
        public virtual ICollection<App> App { get; set; }
        public virtual ICollection<AppDataAccessibility> AppDataAccessibility { get; set; }
        public virtual ICollection<Audit> Audit { get; set; }
        public virtual ICollection<BankTemplateMaster> BankTemplateMaster { get; set; }
        public virtual ICollection<CentralOfficeApp> CentralOfficeApp { get; set; }
        public virtual ICollection<Charity> Charity { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Communication> Communication { get; set; }
        public virtual ICollection<ConnectedCharity> ConnectedCharity { get; set; }
        public virtual ICollection<ContactType> ContactType { get; set; }
        public virtual ICollection<Country> Country { get; set; }
        public virtual ICollection<County> County { get; set; }
        public virtual ICollection<DataDevelopmentProcessFeeDetails> DataDevelopmentProcessFeeDetails { get; set; }
        public virtual ICollection<DataImportLog> DataImportLog { get; set; }
        public virtual ICollection<Envelope> Envelope { get; set; }
        public virtual ICollection<EnvelopeSetting> EnvelopeSetting { get; set; }
        public virtual ICollection<ExceptionLog> ExceptionLog { get; set; }
        public virtual ICollection<Family> Family { get; set; }
        public virtual ICollection<Letter> Letter { get; set; }
        public virtual ICollection<Licence> Licence { get; set; }
        public virtual ICollection<Method> Method { get; set; }
        public virtual ICollection<MmoattendanceCode> MmoattendanceCode { get; set; }
        public virtual ICollection<Mmocertificate> Mmocertificate { get; set; }
        public virtual ICollection<MmocertificateIssuer> MmocertificateIssuer { get; set; }
        public virtual ICollection<MmocontactType> MmocontactType { get; set; }
        public virtual ICollection<MmodataImportLog> MmodataImportLog { get; set; }
        public virtual ICollection<Mmogroup> Mmogroup { get; set; }
        public virtual ICollection<MmogroupType> MmogroupType { get; set; }
        public virtual ICollection<Mmolicence> Mmolicence { get; set; }
        public virtual ICollection<MmomaritalStatus> MmomaritalStatus { get; set; }
        public virtual ICollection<MmomasterTask> MmomasterTask { get; set; }
        public virtual ICollection<MmomembershipCode> MmomembershipCode { get; set; }
        public virtual ICollection<MmomembershipEnrolmentForm> MmomembershipEnrolmentForm { get; set; }
        public virtual ICollection<MmomembershipType> MmomembershipType { get; set; }
        public virtual ICollection<Mmomodules> Mmomodules { get; set; }
        public virtual ICollection<MmorelationshipType> MmorelationshipType { get; set; }
        public virtual ICollection<MmoreportLabel> MmoreportLabel { get; set; }
        public virtual ICollection<Mmoskill> Mmoskill { get; set; }
        public virtual ICollection<MmoskillGroup> MmoskillGroup { get; set; }
        public virtual ICollection<MmotaskStatus> MmotaskStatus { get; set; }
        public virtual ICollection<MmouserDefinedField> MmouserDefinedField { get; set; }
        public virtual ICollection<MmovisitType> MmovisitType { get; set; }
        public virtual ICollection<OrganizationLicenseHistory> OrganizationLicenseHistory { get; set; }
        public virtual ICollection<OrganizationMmolicenseHistory> OrganizationMmolicenseHistory { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<PersonType> PersonType { get; set; }
        public virtual ICollection<PgsdonorContact> PgsdonorContact { get; set; }
        public virtual ICollection<Purpose> Purpose { get; set; }
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual ICollection<ReportLabel> ReportLabel { get; set; }
        public virtual ICollection<SmartFilter> SmartFilter { get; set; }
        public virtual ICollection<StandardComments> StandardComments { get; set; }
        public virtual ICollection<TaxYear> TaxYear { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
