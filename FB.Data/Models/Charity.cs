using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Charity
    {
        public Charity()
        {
            Address = new HashSet<Address>();
            AdministrationLetter = new HashSet<AdministrationLetter>();
            AppDataAccessibility = new HashSet<AppDataAccessibility>();
            Branch = new HashSet<Branch>();
            CharityApp = new HashSet<CharityApp>();
            CharityConnectedCharity = new HashSet<CharityConnectedCharity>();
            CharityContactPerson = new HashSet<CharityContactPerson>();
            CharityGoCardLessPlan = new HashSet<CharityGoCardLessPlan>();
            Claim = new HashSet<Claim>();
            Communication = new HashSet<Communication>();
            ConnectedCharity1 = new HashSet<ConnectedCharity>();
            ContactLessTerminal = new HashSet<ContactLessTerminal>();
            ContactType = new HashSet<ContactType>();
            Country = new HashSet<Country>();
            Envelope = new HashSet<Envelope>();
            EnvelopeSetting = new HashSet<EnvelopeSetting>();
            Event = new HashSet<Event>();
            ExceptionLog = new HashSet<ExceptionLog>();
            FailedBtdonationEmailDetail = new HashSet<FailedBtdonationEmailDetail>();
            Family = new HashSet<Family>();
            Gasds = new HashSet<Gasds>();
            GoCardlessNotification = new HashSet<GoCardlessNotification>();
            GoCardlessSubscription = new HashSet<GoCardlessSubscription>();
            InitialContact = new HashSet<InitialContact>();
            JustGivingPaymentRef = new HashSet<JustGivingPaymentRef>();
            KycXcharity = new HashSet<KycXcharity>();
            Letter = new HashSet<Letter>();
            Method = new HashSet<Method>();
            MmoattendanceCode = new HashSet<MmoattendanceCode>();
            MmobraintreeUserDefinedField = new HashSet<MmobraintreeUserDefinedField>();
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
            QuickDonorGift = new HashSet<QuickDonorGift>();
            ReportLabel = new HashSet<ReportLabel>();
            StandardComments = new HashSet<StandardComments>();
            TrueLayerStandingOrder = new HashSet<TrueLayerStandingOrder>();
            User = new HashSet<User>();
            WebsiteButton = new HashSet<WebsiteButton>();
        }

        public int? CentralOfficeId { get; set; }
        public int CharityId { get; set; }
        public string CharityName { get; set; }
        public string Hmrcreference { get; set; }
        public string Prefix { get; set; }
        public string Position { get; set; }
        public DateTime? FinancialYearEnd { get; set; }
        public string Nominee { get; set; }
        public string Notes { get; set; }
        public decimal? PercentageFee { get; set; }
        public decimal? FlatRate { get; set; }
        public decimal? Threshold { get; set; }
        public decimal? PercentageGreterThanThreshold { get; set; }
        public decimal? PercentageLessThanThreshold { get; set; }
        public bool IsActive { get; set; }
        public string SignatoryTitle { get; set; }
        public string SignatoryForename { get; set; }
        public string SignatorySurname { get; set; }
        public string RegulatedBy { get; set; }
        public string RegulatorNumber { get; set; }
        public string OnlineUserId { get; set; }
        public string OnlinePassword { get; set; }
        public int? ConnectedCharity { get; set; }
        public string Url { get; set; }
        public string AuxiliaryReference { get; set; }
        public string SignatoryEmail { get; set; }
        public string SignatoryPhone { get; set; }
        public string SignatoryAddress { get; set; }
        public string SignatoryPostcode { get; set; }
        public bool IsAddressOversease { get; set; }
        public string CharityReference { get; set; }
        public byte? Gasdstype { get; set; }
        public int? CreatedBy { get; set; }
        public int? DefaultPurposeId { get; set; }
        public int? DefaultGivingType { get; set; }
        public byte? ReferenceType { get; set; }
        public byte? FeesType { get; set; }
        public byte? ClaimType { get; set; }
        public int? AgentId { get; set; }
        public string StripeApiKey { get; set; }
        public string StripePublishableKey { get; set; }
        public string GoCardLessMerchantId { get; set; }
        public string GoCardLessAppId { get; set; }
        public string GoCardLessAppSecret { get; set; }
        public string PaypalClientId { get; set; }
        public string PaypalClientSecret { get; set; }
        public string JustGivingCharityId { get; set; }
        public string BraintreeMerchantId { get; set; }
        public string BraintreePublicKey { get; set; }
        public string BraintreePrivateKey { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string GoCardLessAccessKey { get; set; }
        public byte? PayToAoN { get; set; }
        public string Cpkclient { get; set; }
        public string OnlinePasswordSalt { get; set; }
        public string BraintreeAccountMerchantId { get; set; }
        public string JustGivingAppId { get; set; }
        public bool? IsBraintreePayPalEnabled { get; set; }
        public int? PaypalCurrency { get; set; }
        public string GoCardlessOrganisationId { get; set; }
        public bool? IsMgo { get; set; }
        public bool IsMmo { get; set; }
        public string ScreenImage { get; set; }
        public bool? IsImageSeenByDonor { get; set; }
        public string DonateRegularlyReturnUrl { get; set; }
        public string PayAcharityMerchantId { get; set; }
        public string PayAcharityAccountMerchantId { get; set; }
        public string PayAcharityPublicKey { get; set; }
        public string PayAcharityPrivateKey { get; set; }
        public string PaccharityId { get; set; }
        public bool? IncludeTransactionFee { get; set; }
        public bool? IncludeTransactionFeeForBrainTree { get; set; }
        public bool? IncludeTransactionFeeForPac { get; set; }
        public string GoogleReCaptchaKey { get; set; }
        public string GoogleReCaptchaSecret { get; set; }
        public bool? IsTrueLayerStobeta { get; set; }
        public string BrainTreeDonateRedirectUrl { get; set; }
        public string TrueLayerSortCode { get; set; }
        public string TrueLayerAccountNumber { get; set; }
        public string TrueLayerAccountName { get; set; }
        public string TrueLayerReferencePrefix { get; set; }
        public decimal? TrueLayerAnnualIncrease { get; set; }
        public bool? IncludeTransactionFeeTl { get; set; }
        public bool? EnableBtemailVerification { get; set; }
        public bool? EnableNoOfBtemailDonation { get; set; }
        public int? AllowNoOfBtemailDonation { get; set; }
        public int? FailedBtdonationLimit { get; set; }
        public bool? IsMmosystemCreated { get; set; }
        public string TenantId { get; set; }
        public byte? SourceType { get; set; }
        public decimal? TpdonationAmountLimit { get; set; }
        public bool? MsadminConsent { get; set; }
        public string Mccpkorganization { get; set; }
        public int? CharityEnrolmentFormId { get; set; }
        public bool? IsCoi { get; set; }
        public string MailChimpToken { get; set; }
        public string CharityToken { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual ConnectedCharity ConnectedCharityNavigation { get; set; }
        public virtual Purpose DefaultPurpose { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<AdministrationLetter> AdministrationLetter { get; set; }
        public virtual ICollection<AppDataAccessibility> AppDataAccessibility { get; set; }
        public virtual ICollection<Branch> Branch { get; set; }
        public virtual ICollection<CharityApp> CharityApp { get; set; }
        public virtual ICollection<CharityConnectedCharity> CharityConnectedCharity { get; set; }
        public virtual ICollection<CharityContactPerson> CharityContactPerson { get; set; }
        public virtual ICollection<CharityGoCardLessPlan> CharityGoCardLessPlan { get; set; }
        public virtual ICollection<Claim> Claim { get; set; }
        public virtual ICollection<Communication> Communication { get; set; }
        public virtual ICollection<ConnectedCharity> ConnectedCharity1 { get; set; }
        public virtual ICollection<ContactLessTerminal> ContactLessTerminal { get; set; }
        public virtual ICollection<ContactType> ContactType { get; set; }
        public virtual ICollection<Country> Country { get; set; }
        public virtual ICollection<Envelope> Envelope { get; set; }
        public virtual ICollection<EnvelopeSetting> EnvelopeSetting { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<ExceptionLog> ExceptionLog { get; set; }
        public virtual ICollection<FailedBtdonationEmailDetail> FailedBtdonationEmailDetail { get; set; }
        public virtual ICollection<Family> Family { get; set; }
        public virtual ICollection<Gasds> Gasds { get; set; }
        public virtual ICollection<GoCardlessNotification> GoCardlessNotification { get; set; }
        public virtual ICollection<GoCardlessSubscription> GoCardlessSubscription { get; set; }
        public virtual ICollection<InitialContact> InitialContact { get; set; }
        public virtual ICollection<JustGivingPaymentRef> JustGivingPaymentRef { get; set; }
        public virtual ICollection<KycXcharity> KycXcharity { get; set; }
        public virtual ICollection<Letter> Letter { get; set; }
        public virtual ICollection<Method> Method { get; set; }
        public virtual ICollection<MmoattendanceCode> MmoattendanceCode { get; set; }
        public virtual ICollection<MmobraintreeUserDefinedField> MmobraintreeUserDefinedField { get; set; }
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
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual ICollection<ReportLabel> ReportLabel { get; set; }
        public virtual ICollection<StandardComments> StandardComments { get; set; }
        public virtual ICollection<TrueLayerStandingOrder> TrueLayerStandingOrder { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<WebsiteButton> WebsiteButton { get; set; }
    }
}
