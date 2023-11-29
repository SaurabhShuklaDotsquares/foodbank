using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FB.Data.Models
{
    public partial class MMOOlgaDevContext : DbContext
    {
        public MMOOlgaDevContext()
        {
        }

        public MMOOlgaDevContext(DbContextOptions<MMOOlgaDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<AdministrationLetter> AdministrationLetter { get; set; }
        public virtual DbSet<AdministrativeGroup> AdministrativeGroup { get; set; }
        public virtual DbSet<Agencies> Agencies { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentUser> AgentUser { get; set; }
        public virtual DbSet<Allergies> Allergies { get; set; }
        public virtual DbSet<App> App { get; set; }
        public virtual DbSet<AppClaimRequest> AppClaimRequest { get; set; }
        public virtual DbSet<AppDataAccessibility> AppDataAccessibility { get; set; }
        public virtual DbSet<AppDonationXref> AppDonationXref { get; set; }
        public virtual DbSet<AppErrorLog> AppErrorLog { get; set; }
        public virtual DbSet<AppLoginToken> AppLoginToken { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Availability> Availability { get; set; }
        public virtual DbSet<Available> Available { get; set; }
        public virtual DbSet<BankStatementDonorMapped> BankStatementDonorMapped { get; set; }
        public virtual DbSet<BankStatementDonorMappedElement> BankStatementDonorMappedElement { get; set; }
        public virtual DbSet<BankStatementTemplate> BankStatementTemplate { get; set; }
        public virtual DbSet<BankTemplateMaster> BankTemplateMaster { get; set; }
        public virtual DbSet<Batch> Batch { get; set; }
        public virtual DbSet<BraintreeTransaction> BraintreeTransaction { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<BranchApp> BranchApp { get; set; }
        public virtual DbSet<BranchContactPerson> BranchContactPerson { get; set; }
        public virtual DbSet<BranchToken> BranchToken { get; set; }
        public virtual DbSet<BtdonationsVerifiedEmail> BtdonationsVerifiedEmail { get; set; }
        public virtual DbSet<CentralOffice> CentralOffice { get; set; }
        public virtual DbSet<CentralOfficeApp> CentralOfficeApp { get; set; }
        public virtual DbSet<Charity> Charity { get; set; }
        public virtual DbSet<CharityApp> CharityApp { get; set; }
        public virtual DbSet<CharityConnectedCharity> CharityConnectedCharity { get; set; }
        public virtual DbSet<CharityContactPerson> CharityContactPerson { get; set; }
        public virtual DbSet<CharityGoCardLessPlan> CharityGoCardLessPlan { get; set; }
        public virtual DbSet<CheckType> CheckType { get; set; }
        public virtual DbSet<Checked> Checked { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<ClaimLog> ClaimLog { get; set; }
        public virtual DbSet<ClaimNumberGeneration> ClaimNumberGeneration { get; set; }
        public virtual DbSet<CommType> CommType { get; set; }
        public virtual DbSet<Communication> Communication { get; set; }
        public virtual DbSet<ConnectedCharity> ConnectedCharity { get; set; }
        public virtual DbSet<ContactHistory> ContactHistory { get; set; }
        public virtual DbSet<ContactLessDonorToken> ContactLessDonorToken { get; set; }
        public virtual DbSet<ContactLessSchedule> ContactLessSchedule { get; set; }
        public virtual DbSet<ContactLessScheduleDate> ContactLessScheduleDate { get; set; }
        public virtual DbSet<ContactLessTerminal> ContactLessTerminal { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<DataDevelopmentProcessFeeDetails> DataDevelopmentProcessFeeDetails { get; set; }
        public virtual DbSet<DataImportErrorLog> DataImportErrorLog { get; set; }
        public virtual DbSet<DataImportLog> DataImportLog { get; set; }
        public virtual DbSet<DataImportStatus> DataImportStatus { get; set; }
        public virtual DbSet<DataNotImport> DataNotImport { get; set; }
        public virtual DbSet<Dcusers> Dcusers { get; set; }
        public virtual DbSet<Declaration> Declaration { get; set; }
        public virtual DbSet<DeclarationHistory> DeclarationHistory { get; set; }
        public virtual DbSet<DeclarationHistoryVerify> DeclarationHistoryVerify { get; set; }
        public virtual DbSet<DeleteTablesData> DeleteTablesData { get; set; }
        public virtual DbSet<Donation> Donation { get; set; }
        public virtual DbSet<DonationGoToLive> DonationGoToLive { get; set; }
        public virtual DbSet<DonationType> DonationType { get; set; }
        public virtual DbSet<DonorFoodbank> DonorFoodbank { get; set; }
        public virtual DbSet<DonorToken> DonorToken { get; set; }
        public virtual DbSet<Envelope> Envelope { get; set; }
        public virtual DbSet<EnvelopeSetting> EnvelopeSetting { get; set; }
        public virtual DbSet<Ethnicity> Ethnicity { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLog { get; set; }
        public virtual DbSet<FailedBtdonationEmailDetail> FailedBtdonationEmailDetail { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<FamilyAddress> FamilyAddress { get; set; }
        public virtual DbSet<FamilyAgency> FamilyAgency { get; set; }
        public virtual DbSet<FamilyMember> FamilyMember { get; set; }
        public virtual DbSet<FamilyMemberAllergy> FamilyMemberAllergy { get; set; }
        public virtual DbSet<FamilyNotes> FamilyNotes { get; set; }
        public virtual DbSet<FamilyParcelFoodItem> FamilyParcelFoodItem { get; set; }
        public virtual DbSet<Fbaddress> Fbaddress { get; set; }
        public virtual DbSet<Fbcontact> Fbcontact { get; set; }
        public virtual DbSet<Fblocation> Fblocation { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<FeedbackFormDetails> FeedbackFormDetails { get; set; }
        public virtual DbSet<FeedbackMaster> FeedbackMaster { get; set; }
        public virtual DbSet<FeedbackMasterFieldOption> FeedbackMasterFieldOption { get; set; }
        public virtual DbSet<FilterCriteria> FilterCriteria { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodItem> FoodItem { get; set; }
        public virtual DbSet<FoodItemAllergy> FoodItemAllergy { get; set; }
        public virtual DbSet<Foodbank> Foodbank { get; set; }
        public virtual DbSet<FoodbankFamily> FoodbankFamily { get; set; }
        public virtual DbSet<FoodbankMenu> FoodbankMenu { get; set; }
        public virtual DbSet<FoodbankRecipe> FoodbankRecipe { get; set; }
        public virtual DbSet<FoodbankRecipeFoodItem> FoodbankRecipeFoodItem { get; set; }
        public virtual DbSet<FoodbankRoleMenuPrivilege> FoodbankRoleMenuPrivilege { get; set; }
        public virtual DbSet<FoodbankSetting> FoodbankSetting { get; set; }
        public virtual DbSet<FoodbankUserDataAccessibility> FoodbankUserDataAccessibility { get; set; }
        public virtual DbSet<ForgotPassword> ForgotPassword { get; set; }
        public virtual DbSet<Frequency> Frequency { get; set; }
        public virtual DbSet<Gasds> Gasds { get; set; }
        public virtual DbSet<GoCardLessPayment> GoCardLessPayment { get; set; }
        public virtual DbSet<GoCardlessNotification> GoCardlessNotification { get; set; }
        public virtual DbSet<GoCardlessSubscription> GoCardlessSubscription { get; set; }
        public virtual DbSet<Granter> Granter { get; set; }
        public virtual DbSet<GranterReceipt> GranterReceipt { get; set; }
        public virtual DbSet<Grantor> Grantor { get; set; }
        public virtual DbSet<GrantorReceipt> GrantorReceipt { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupGroupType> GroupGroupType { get; set; }
        public virtual DbSet<GroupMember> GroupMember { get; set; }
        public virtual DbSet<GroupType> GroupType { get; set; }
        public virtual DbSet<Household> Household { get; set; }
        public virtual DbSet<ImportLog> ImportLog { get; set; }
        public virtual DbSet<ImportLogDetail> ImportLogDetail { get; set; }
        public virtual DbSet<InitialContact> InitialContact { get; set; }
        public virtual DbSet<JustGivingPaymentRef> JustGivingPaymentRef { get; set; }
        public virtual DbSet<KycXcharity> KycXcharity { get; set; }
        public virtual DbSet<Letter> Letter { get; set; }
        public virtual DbSet<Licence> Licence { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Mcusers> Mcusers { get; set; }
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<MeetingSchedule> MeetingSchedule { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Method> Method { get; set; }
        public virtual DbSet<MethodAccess> MethodAccess { get; set; }
        public virtual DbSet<MmoactivityLog> MmoactivityLog { get; set; }
        public virtual DbSet<MmoattendanceCode> MmoattendanceCode { get; set; }
        public virtual DbSet<MmobraintreeTransaction> MmobraintreeTransaction { get; set; }
        public virtual DbSet<MmobraintreeUserDefinedField> MmobraintreeUserDefinedField { get; set; }
        public virtual DbSet<MmobranchToken> MmobranchToken { get; set; }
        public virtual DbSet<Mmocertificate> Mmocertificate { get; set; }
        public virtual DbSet<MmocertificateIssuer> MmocertificateIssuer { get; set; }
        public virtual DbSet<Mmocontact> Mmocontact { get; set; }
        public virtual DbSet<MmocontactType> MmocontactType { get; set; }
        public virtual DbSet<Mmocorrespondence> Mmocorrespondence { get; set; }
        public virtual DbSet<MmodataImportErrorLog> MmodataImportErrorLog { get; set; }
        public virtual DbSet<MmodataImportLog> MmodataImportLog { get; set; }
        public virtual DbSet<MmodataImportStatus> MmodataImportStatus { get; set; }
        public virtual DbSet<MmodataNotImport> MmodataNotImport { get; set; }
        public virtual DbSet<Mmogroup> Mmogroup { get; set; }
        public virtual DbSet<MmogroupEvent> MmogroupEvent { get; set; }
        public virtual DbSet<MmogroupEventAttendance> MmogroupEventAttendance { get; set; }
        public virtual DbSet<MmogroupLink> MmogroupLink { get; set; }
        public virtual DbSet<MmogroupMembers> MmogroupMembers { get; set; }
        public virtual DbSet<MmogroupMembersFee> MmogroupMembersFee { get; set; }
        public virtual DbSet<MmogroupType> MmogroupType { get; set; }
        public virtual DbSet<MmoimportLog> MmoimportLog { get; set; }
        public virtual DbSet<MmoimportLogDetail> MmoimportLogDetail { get; set; }
        public virtual DbSet<Mmolicence> Mmolicence { get; set; }
        public virtual DbSet<MmomaritalStatus> MmomaritalStatus { get; set; }
        public virtual DbSet<MmomasterTask> MmomasterTask { get; set; }
        public virtual DbSet<MmomemberCertificate> MmomemberCertificate { get; set; }
        public virtual DbSet<MmomemberRule> MmomemberRule { get; set; }
        public virtual DbSet<MmomemberSkill> MmomemberSkill { get; set; }
        public virtual DbSet<Mmomembership> Mmomembership { get; set; }
        public virtual DbSet<MmomembershipCode> MmomembershipCode { get; set; }
        public virtual DbSet<MmomembershipEnrolmentForm> MmomembershipEnrolmentForm { get; set; }
        public virtual DbSet<MmomembershipEnrolmentFormUserDefinedFields> MmomembershipEnrolmentFormUserDefinedFields { get; set; }
        public virtual DbSet<MmomembershipMembersFee> MmomembershipMembersFee { get; set; }
        public virtual DbSet<MmomembershipType> MmomembershipType { get; set; }
        public virtual DbSet<Mmomenu> Mmomenu { get; set; }
        public virtual DbSet<Mmomodules> Mmomodules { get; set; }
        public virtual DbSet<Mmonotes> Mmonotes { get; set; }
        public virtual DbSet<MmopersonAdditonalDetails> MmopersonAdditonalDetails { get; set; }
        public virtual DbSet<MmorelationshipMember> MmorelationshipMember { get; set; }
        public virtual DbSet<MmorelationshipType> MmorelationshipType { get; set; }
        public virtual DbSet<MmoreportLabel> MmoreportLabel { get; set; }
        public virtual DbSet<MmoroleMenuPrivilege> MmoroleMenuPrivilege { get; set; }
        public virtual DbSet<Mmoskill> Mmoskill { get; set; }
        public virtual DbSet<MmoskillGroup> MmoskillGroup { get; set; }
        public virtual DbSet<Mmospresult> Mmospresult { get; set; }
        public virtual DbSet<MmotaskShift> MmotaskShift { get; set; }
        public virtual DbSet<MmotaskSkill> MmotaskSkill { get; set; }
        public virtual DbSet<MmotaskStatus> MmotaskStatus { get; set; }
        public virtual DbSet<MmotaskWillingMember> MmotaskWillingMember { get; set; }
        public virtual DbSet<Mmounavailability> Mmounavailability { get; set; }
        public virtual DbSet<MmouserDataAccessibility> MmouserDataAccessibility { get; set; }
        public virtual DbSet<MmouserDefined> MmouserDefined { get; set; }
        public virtual DbSet<MmouserDefinedField> MmouserDefinedField { get; set; }
        public virtual DbSet<MmouserDefinedFieldOption> MmouserDefinedFieldOption { get; set; }
        public virtual DbSet<Mmovisit> Mmovisit { get; set; }
        public virtual DbSet<MmovisitLink> MmovisitLink { get; set; }
        public virtual DbSet<MmovisitType> MmovisitType { get; set; }
        public virtual DbSet<MmowebsiteButton> MmowebsiteButton { get; set; }
        public virtual DbSet<MyGivingTaskUserSeen> MyGivingTaskUserSeen { get; set; }
        public virtual DbSet<MyGivingTasks> MyGivingTasks { get; set; }
        public virtual DbSet<OrganisationPreference> OrganisationPreference { get; set; }
        public virtual DbSet<OrganizationLicenseHistory> OrganizationLicenseHistory { get; set; }
        public virtual DbSet<OrganizationMmolicenseHistory> OrganizationMmolicenseHistory { get; set; }
        public virtual DbSet<PacerrorLog> PacerrorLog { get; set; }
        public virtual DbSet<Paragraph> Paragraph { get; set; }
        public virtual DbSet<ParcelFoodItem> ParcelFoodItem { get; set; }
        public virtual DbSet<ParcelType> ParcelType { get; set; }
        public virtual DbSet<Parcels> Parcels { get; set; }
        public virtual DbSet<PayAcharityTempData> PayAcharityTempData { get; set; }
        public virtual DbSet<PaymentImport> PaymentImport { get; set; }
        public virtual DbSet<PaypalTransaction> PaypalTransaction { get; set; }
        public virtual DbSet<PendingPayment> PendingPayment { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonCheck> PersonCheck { get; set; }
        public virtual DbSet<PersonEvent> PersonEvent { get; set; }
        public virtual DbSet<PersonPaymentMethodInfo> PersonPaymentMethodInfo { get; set; }
        public virtual DbSet<PersonTask> PersonTask { get; set; }
        public virtual DbSet<PersonType> PersonType { get; set; }
        public virtual DbSet<PgsdonorContact> PgsdonorContact { get; set; }
        public virtual DbSet<PledgeDetail> PledgeDetail { get; set; }
        public virtual DbSet<Profession> Profession { get; set; }
        public virtual DbSet<Purpose> Purpose { get; set; }
        public virtual DbSet<PurposeAccess> PurposeAccess { get; set; }
        public virtual DbSet<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual DbSet<ReferralReason> ReferralReason { get; set; }
        public virtual DbSet<ReferrerFamily> ReferrerFamily { get; set; }
        public virtual DbSet<ReferrerType> ReferrerType { get; set; }
        public virtual DbSet<Referrers> Referrers { get; set; }
        public virtual DbSet<RegularGift> RegularGift { get; set; }
        public virtual DbSet<ReportLabel> ReportLabel { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenuPrivilege> RoleMenuPrivilege { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<SmartFilter> SmartFilter { get; set; }
        public virtual DbSet<StandardComments> StandardComments { get; set; }
        public virtual DbSet<StandingGift> StandingGift { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<StockHistory> StockHistory { get; set; }
        public virtual DbSet<StripePayment> StripePayment { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskSchedule> TaskSchedule { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TaxYear> TaxYear { get; set; }
        public virtual DbSet<TransactionFee> TransactionFee { get; set; }
        public virtual DbSet<TrueLayerAnnualIncraeseDetails> TrueLayerAnnualIncraeseDetails { get; set; }
        public virtual DbSet<TrueLayerStandingOrder> TrueLayerStandingOrder { get; set; }
        public virtual DbSet<TwoFactorEmailLogin> TwoFactorEmailLogin { get; set; }
        public virtual DbSet<TypeMaster> TypeMaster { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDataAccessibility> UserDataAccessibility { get; set; }
        public virtual DbSet<UserPreference> UserPreference { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<VolunteerAvailability> VolunteerAvailability { get; set; }
        public virtual DbSet<VolunteerSkill> VolunteerSkill { get; set; }
        public virtual DbSet<VolunteerUnavailability> VolunteerUnavailability { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<WebsiteButton> WebsiteButton { get; set; }
        public virtual DbSet<Xmltbl> Xmltbl { get; set; }

        // Unable to generate entity type for table 'dbo.Error_Log'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.MMODonation'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.donation_back'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PaypalLog'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.CharityMap'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.CharityConnectedCharity_backup'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.CommentsBackup'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.menu_internal'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.2.12;Database=foodbankdb;user Id=SAURABHSHUKHLA\\SQLEXPRESS2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(e => e.ActivityId);

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityDetail).IsRequired();

                entity.Property(e => e.ActivityType).HasMaxLength(200);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.County).HasMaxLength(50);

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.HouseName).HasMaxLength(255);

                entity.Property(e => e.HouseNumber).HasMaxLength(255);

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.IsMmo).HasColumnName("IsMMO");

                entity.Property(e => e.Mccpkaddress)
                    .HasColumnName("MCCPKADDRESS")
                    .HasMaxLength(250);

                entity.Property(e => e.MmoaddressType).HasColumnName("MMOAddressType");

                entity.Property(e => e.Mmodescripton)
                    .HasColumnName("MMODescripton")
                    .HasMaxLength(255);

                entity.Property(e => e.MmoisExDirectory).HasColumnName("MMOIsExDirectory");

                entity.Property(e => e.MmoisMailing).HasColumnName("MMOIsMailing");

                entity.Property(e => e.OtherAddressLine).HasMaxLength(255);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.StreetName).HasMaxLength(255);

                entity.HasOne(d => d.AddressTypeNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressType)
                    .HasConstraintName("FK_Address_AddressType");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Address_Branch");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Address_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Address_Charity");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Address_Country");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK_Address_Household");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Address_Person");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.AddressTypeDescription).HasMaxLength(255);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<AdministrationLetter>(entity =>
            {
                entity.HasKey(e => e.LetterId);

                entity.Property(e => e.LetterId).HasColumnName("LetterID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Greeting).HasMaxLength(20);

                entity.Property(e => e.LetterHeadImage).HasMaxLength(100);

                entity.Property(e => e.Names).HasMaxLength(20);

                entity.Property(e => e.OrgNames).HasMaxLength(10);

                entity.Property(e => e.PostCode).HasMaxLength(20);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AdministrationLetter)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Administr__Branc__4CD7043A");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.AdministrationLetter)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__Administr__Centr__4AEEBBC8");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.AdministrationLetter)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Administr__Chari__4BE2E001");
            });

            modelBuilder.Entity<AdministrativeGroup>(entity =>
            {
                entity.Property(e => e.AdministrativeGroupId).HasColumnName("AdministrativeGroupID");

                entity.Property(e => e.AdministativeGroupDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Agencies>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.BriefSummary).HasMaxLength(1000);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Agencies)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Agencies__Addres__7C51D889");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Agencies)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Agencies__Contac__7B5DB450");

                entity.HasOne(d => d.FoodBank)
                    .WithMany(p => p.Agencies)
                    .HasForeignKey(d => d.FoodBankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Agencies__FoodBa__7A699017");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.AgentNumber)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityOnlinePassword).HasMaxLength(50);

                entity.Property(e => e.CharityOnlineUserId)
                    .HasColumnName("CharityOnlineUserID")
                    .HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Forename).HasMaxLength(50);

                entity.Property(e => e.GatewayId)
                    .HasColumnName("GatewayID")
                    .HasMaxLength(20);

                entity.Property(e => e.GatewayPassword).HasMaxLength(20);

                entity.Property(e => e.Hmrcreference)
                    .HasColumnName("HMRCReference")
                    .HasMaxLength(7);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordSalt).HasMaxLength(100);

                entity.Property(e => e.Position).HasMaxLength(30);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.Signatory).HasMaxLength(100);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.TelephoneExt).HasMaxLength(20);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(10);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agent_CentralOffice");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Agent_Country");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AgentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Agent_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AgentUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Agent_AgentUser");
            });

            modelBuilder.Entity<AgentUser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AgentId })
                    .HasName("PK__AgentUse__1E24F751A2DBC44F");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentUser)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AgentUser__Agent__16C50C4C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AgentUserNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AgentUser__UserI__15D0E813");
            });

            modelBuilder.Entity<Allergies>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<App>(entity =>
            {
                entity.Property(e => e.AppImage).HasMaxLength(150);

                entity.Property(e => e.AppName).HasMaxLength(100);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RedirectUri)
                    .HasColumnName("RedirectURI")
                    .HasMaxLength(150);

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.App)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__App__CentralOffi__1AE0814A");
            });

            modelBuilder.Entity<AppClaimRequest>(entity =>
            {
                entity.HasKey(e => e.AppClaimId)
                    .HasName("PK__AppClaim__050C8D8C36BFCADC");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.ExpectedToClaim)
                    .HasColumnName("expectedToClaim")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastGiftDate).HasColumnType("datetime");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.AppClaimRequest)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppClaimRequest_App");
            });

            modelBuilder.Entity<AppDataAccessibility>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.AppDataAccessibility)
                    .HasForeignKey(d => d.AppId)
                    .HasConstraintName("FK__AppDataAc__AppId__1DBCEDF5");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AppDataAccessibility)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__AppDataAc__Branc__20995AA0");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.AppDataAccessibility)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__AppDataAc__Centr__1EB1122E");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.AppDataAccessibility)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__AppDataAc__Chari__1FA53667");
            });

            modelBuilder.Entity<AppDonationXref>(entity =>
            {
                entity.HasKey(e => e.AppDonationId)
                    .HasName("PK__AppDonat__7CB3F9E91620B10A");

                entity.ToTable("AppDonationXRef");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.AppDonationXref)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppDonationXRef_App");

                entity.HasOne(d => d.Donation)
                    .WithMany(p => p.AppDonationXref)
                    .HasForeignKey(d => d.DonationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppDonationXRef_Donation");
            });

            modelBuilder.Entity<AppErrorLog>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LoginToken).HasMaxLength(250);

                entity.Property(e => e.Message).HasMaxLength(250);

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppLoginToken>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.LoginToken).HasMaxLength(500);

                entity.HasOne(d => d.App)
                    .WithMany(p => p.AppLoginToken)
                    .HasForeignKey(d => d.AppId)
                    .HasConstraintName("FK__AppLoginT__AppId__6BFB752F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppLoginToken)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__AppLoginT__UserI__6CEF9968");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.AttendanceId)
                    .HasColumnName("AttendanceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MeetingId).HasColumnName("MeetingID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_Meeting");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Attendance_Person");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AuditId).HasColumnName("AuditID");

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CorrelationId)
                    .HasColumnName("CorrelationID")
                    .HasMaxLength(50);

                entity.Property(e => e.Cpkfilling)
                    .HasColumnName("CPKFilling")
                    .HasMaxLength(50);

                entity.Property(e => e.EndPointUrl).HasColumnName("EndPointURL");

                entity.Property(e => e.HmrcerrorCode).HasColumnName("HMRCErrorCode");

                entity.Property(e => e.Hmrcreference)
                    .HasColumnName("HMRCReference")
                    .HasMaxLength(10);

                entity.Property(e => e.LostCorrelationId).HasColumnName("LostCorrelationID");

                entity.Property(e => e.PollDate).HasColumnType("date");

                entity.Property(e => e.PollInterval).HasMaxLength(5);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.SubmissionType).HasMaxLength(12);

                entity.Property(e => e.TaxYear).HasMaxLength(10);

                entity.Property(e => e.Xmlreceived).HasColumnName("XMLReceived");

                entity.Property(e => e.Xmlreturned).HasColumnName("XMLReturned");

                entity.Property(e => e.Xmlsent).HasColumnName("XMLSent");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__Audit__CentralOf__7D8F4A2E");

                entity.HasOne(d => d.ClaimNumber)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.ClaimNumberId)
                    .HasConstraintName("FK__Audit__ClaimNumb__13FCE2E3");
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.Property(e => e.AvailabilityId).HasColumnName("AvailabilityID");

                entity.Property(e => e.DateAvailableFrom).HasColumnType("datetime");

                entity.Property(e => e.DateAvalableTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<Available>(entity =>
            {
                entity.Property(e => e.AvailableId).HasColumnName("AvailableID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.AvailabilityNavigation)
                    .WithMany(p => p.Available)
                    .HasForeignKey(d => d.Availability)
                    .HasConstraintName("FK_Available_Availability");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Available)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Available_Person");
            });

            modelBuilder.Entity<BankStatementDonorMapped>(entity =>
            {
                entity.HasIndex(e => new { e.BankStatementTemplateId, e.MappedText, e.MappedDonor })
                    .HasName("uq_mapped_donor")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankStatementTemplateId).HasColumnName("BankStatementTemplateID");

                entity.Property(e => e.MappedText)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.BankStatementTemplate)
                    .WithMany(p => p.BankStatementDonorMapped)
                    .HasForeignKey(d => d.BankStatementTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankState__BankS__599C027B");

                entity.HasOne(d => d.MappedDonorNavigation)
                    .WithMany(p => p.BankStatementDonorMapped)
                    .HasForeignKey(d => d.MappedDonor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankStatementDonorMapped_Person");

                entity.HasOne(d => d.MappedMethodNavigation)
                    .WithMany(p => p.BankStatementDonorMapped)
                    .HasForeignKey(d => d.MappedMethod)
                    .HasConstraintName("FK_BankStatementDonorMapped_Method");

                entity.HasOne(d => d.MappedPurposeNavigation)
                    .WithMany(p => p.BankStatementDonorMapped)
                    .HasForeignKey(d => d.MappedPurpose)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankStatementDonorMapped_Purpose");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BankStatementDonorMapped)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankState__UserI__7366C176");
            });

            modelBuilder.Entity<BankStatementDonorMappedElement>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.DonorMappedId).HasColumnName("DonorMappedID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.HasOne(d => d.DonorMapped)
                    .WithMany(p => p.BankStatementDonorMappedElement)
                    .HasForeignKey(d => d.DonorMappedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankStatementDonorMappedElement_ToBankStatementDonorMappedElement");
            });

            modelBuilder.Entity<BankStatementTemplate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankTemplateMasterId).HasColumnName("BankTemplateMasterID");

                entity.Property(e => e.IsShow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MappedColumn).HasMaxLength(100);

                entity.Property(e => e.TempColumn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.BankTemplateMaster)
                    .WithMany(p => p.BankStatementTemplate)
                    .HasForeignKey(d => d.BankTemplateMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankStatementTemplate_BankTemplateMaster");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BankStatementTemplate)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankState__UserI__261C5E75");
            });

            modelBuilder.Entity<BankTemplateMaster>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.TemplateName })
                    .HasName("uq_BankTemplateMaster")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CellAddress).HasMaxLength(10);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.TemplateName).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.BankTemplateMaster)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankTempl__Centr__509CA826");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BankTemplateMaster)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBankT__UserI__28E3BC87");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BatchReference).HasMaxLength(100);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.Gasdseligible).HasColumnName("GASDSEligible");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Batch_Branch");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_Batch_Envelope");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK_Batch_Method");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Batch_Person");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK_Batch_Purpose");
            });

            modelBuilder.Entity<BraintreeTransaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchDescription).HasMaxLength(255);

                entity.Property(e => e.BranchReference).HasMaxLength(50);

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CommunityAddress).HasMaxLength(255);

                entity.Property(e => e.CommunityName).HasMaxLength(255);

                entity.Property(e => e.CommunityPostcode).HasMaxLength(20);

                entity.Property(e => e.ContactEmail).HasMaxLength(50);

                entity.Property(e => e.Cpkclient)
                    .HasColumnName("CPKClient")
                    .HasMaxLength(50);

                entity.Property(e => e.FlatRate).HasColumnType("money");

                entity.Property(e => e.Gasdstype).HasColumnName("GASDSType");

                entity.Property(e => e.IsMgo)
                    .IsRequired()
                    .HasColumnName("IsMGO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMmo).HasColumnName("IsMMO");

                entity.Property(e => e.IsMmosystemCreated).HasColumnName("IsMMOSystemCreated");

                entity.Property(e => e.MailChimpToken).HasMaxLength(100);

                entity.Property(e => e.ManagerName).HasMaxLength(255);

                entity.Property(e => e.Mccpkorganization)
                    .HasColumnName("MCCPKORGANIZATION")
                    .HasMaxLength(250);

                entity.Property(e => e.PacReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParishFundName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PercentageFee).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PercentageGreterThanThreshold).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PercentageLessThanThreshold).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.SignatoryAddress).HasMaxLength(500);

                entity.Property(e => e.SignatoryEmail).HasMaxLength(50);

                entity.Property(e => e.SignatoryPhone).HasMaxLength(20);

                entity.Property(e => e.SignatoryPostcode).HasMaxLength(20);

                entity.Property(e => e.Threshold).HasColumnType("money");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Branch)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Charity");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Branch)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Branch_CreatedBy");
            });

            modelBuilder.Entity<BranchApp>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.BranchApp)
                    .HasForeignKey(d => d.AppId)
                    .HasConstraintName("FK__BranchApp__AppId__2746582F");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchApp)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__BranchApp__Branc__283A7C68");
            });

            modelBuilder.Entity<BranchContactPerson>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.HouseName).HasMaxLength(100);

                entity.Property(e => e.HouseNumber).HasMaxLength(100);

                entity.Property(e => e.LandLineNo).HasMaxLength(50);

                entity.Property(e => e.MobileNo).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Postcode).HasMaxLength(20);

                entity.Property(e => e.StreetName).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(10);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchContactPerson)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BranchCon__Branc__5A1103C7");
            });

            modelBuilder.Entity<BranchToken>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("PK_BranchID");

                entity.Property(e => e.BranchId)
                    .HasColumnName("BranchID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Olgatoken)
                    .IsRequired()
                    .HasColumnName("OLGAToken")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithOne(p => p.BranchToken)
                    .HasForeignKey<BranchToken>(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchToken_Branch");
            });

            modelBuilder.Entity<BtdonationsVerifiedEmail>(entity =>
            {
                entity.ToTable("BTDonationsVerifiedEmail");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmailDate).HasColumnType("datetime");

                entity.Property(e => e.EmailLinkId).HasColumnName("EmailLinkID");

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CentralOffice>(entity =>
            {
                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.DateJoined).HasColumnType("date");

                entity.Property(e => e.IsMgo)
                    .IsRequired()
                    .HasColumnName("IsMGO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMmo).HasColumnName("IsMMO");

                entity.Property(e => e.IsMmosystemCreated).HasColumnName("IsMMOSystemCreated");

                entity.Property(e => e.LicenceEndDate).HasColumnType("datetime");

                entity.Property(e => e.LicenceValidYears).HasDefaultValueSql("((1))");

                entity.Property(e => e.MmolicenceEndDate)
                    .HasColumnName("MMOLicenceEndDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.MsadminConsent).HasColumnName("MSAdminConsent");

                entity.Property(e => e.OrganisationName).HasMaxLength(255);

                entity.Property(e => e.SerialNumber).HasMaxLength(8);

                entity.Property(e => e.StandingOrderId).HasColumnName("StandingOrderID");

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CentralOffice)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CentralOffice_CreatedBy");
            });

            modelBuilder.Entity<CentralOfficeApp>(entity =>
            {
                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.CentralOfficeApp)
                    .HasForeignKey(d => d.AppId)
                    .HasConstraintName("FK__CentralOf__AppId__2C0B0D4C");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.CentralOfficeApp)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__CentralOf__Centr__2B16E913");
            });

            modelBuilder.Entity<Charity>(entity =>
            {
                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AllowNoOfBtemailDonation).HasColumnName("AllowNoOfBTEmailDonation");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.AuxiliaryReference).HasMaxLength(10);

                entity.Property(e => e.BrainTreeDonateRedirectUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BraintreeAccountMerchantId).HasMaxLength(100);

                entity.Property(e => e.BraintreeMerchantId).HasMaxLength(100);

                entity.Property(e => e.BraintreePrivateKey).HasMaxLength(100);

                entity.Property(e => e.BraintreePublicKey).HasMaxLength(100);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityName).HasMaxLength(100);

                entity.Property(e => e.CharityReference).HasMaxLength(8);

                entity.Property(e => e.CharityToken).HasMaxLength(100);

                entity.Property(e => e.Cpkclient)
                    .HasColumnName("CPKClient")
                    .HasMaxLength(50);

                entity.Property(e => e.DefaultPurposeId).HasColumnName("DefaultPurposeID");

                entity.Property(e => e.DonateRegularlyReturnUrl)
                    .HasColumnName("DonateRegularlyReturnURL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EnableBtemailVerification).HasColumnName("EnableBTEmailVerification");

                entity.Property(e => e.EnableNoOfBtemailDonation).HasColumnName("EnableNoOfBTEmailDonation");

                entity.Property(e => e.FailedBtdonationLimit).HasColumnName("FailedBTDonationLimit");

                entity.Property(e => e.FeesType).HasDefaultValueSql("((1))");

                entity.Property(e => e.FinancialYearEnd).HasColumnType("date");

                entity.Property(e => e.FlatRate).HasColumnType("money");

                entity.Property(e => e.Gasdstype)
                    .HasColumnName("GASDSType")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GoCardLessAccessKey).HasMaxLength(100);

                entity.Property(e => e.GoCardLessAppId)
                    .HasColumnName("GoCardLessAppID")
                    .HasMaxLength(100);

                entity.Property(e => e.GoCardLessAppSecret).HasMaxLength(100);

                entity.Property(e => e.GoCardLessMerchantId)
                    .HasColumnName("GoCardLessMerchantID")
                    .HasMaxLength(50);

                entity.Property(e => e.GoCardlessOrganisationId)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleReCaptchaKey).HasMaxLength(100);

                entity.Property(e => e.GoogleReCaptchaSecret).HasMaxLength(100);

                entity.Property(e => e.Hmrcreference)
                    .HasColumnName("HMRCReference")
                    .HasMaxLength(10);

                entity.Property(e => e.IncludeTransactionFeeForPac).HasColumnName("IncludeTransactionFeeForPAC");

                entity.Property(e => e.IncludeTransactionFeeTl).HasColumnName("IncludeTransactionFeeTL");

                entity.Property(e => e.IsCoi).HasColumnName("isCOI");

                entity.Property(e => e.IsMgo)
                    .IsRequired()
                    .HasColumnName("IsMGO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMmo).HasColumnName("IsMMO");

                entity.Property(e => e.IsMmosystemCreated).HasColumnName("IsMMOSystemCreated");

                entity.Property(e => e.IsTrueLayerStobeta).HasColumnName("IsTrueLayerSTOBeta");

                entity.Property(e => e.JustGivingAppId).HasMaxLength(100);

                entity.Property(e => e.JustGivingCharityId).HasMaxLength(500);

                entity.Property(e => e.MailChimpToken).HasMaxLength(250);

                entity.Property(e => e.Mccpkorganization)
                    .HasColumnName("MCCPKORGANIZATION")
                    .HasMaxLength(250);

                entity.Property(e => e.MsadminConsent).HasColumnName("MSAdminConsent");

                entity.Property(e => e.Nominee).HasMaxLength(10);

                entity.Property(e => e.OnlinePassword).HasMaxLength(100);

                entity.Property(e => e.OnlinePasswordSalt).HasMaxLength(50);

                entity.Property(e => e.OnlineUserId)
                    .HasColumnName("OnlineUserID")
                    .HasMaxLength(50);

                entity.Property(e => e.PaccharityId)
                    .HasColumnName("PACCharityID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayAcharityAccountMerchantId)
                    .HasColumnName("PayACharityAccountMerchantId")
                    .HasMaxLength(100);

                entity.Property(e => e.PayAcharityMerchantId)
                    .HasColumnName("PayACharityMerchantId")
                    .HasMaxLength(100);

                entity.Property(e => e.PayAcharityPrivateKey)
                    .HasColumnName("PayACharityPrivateKey")
                    .HasMaxLength(100);

                entity.Property(e => e.PayAcharityPublicKey)
                    .HasColumnName("PayACharityPublicKey")
                    .HasMaxLength(100);

                entity.Property(e => e.PaypalClientId).HasMaxLength(500);

                entity.Property(e => e.PaypalClientSecret).HasMaxLength(500);

                entity.Property(e => e.PercentageFee).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PercentageGreterThanThreshold).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PercentageLessThanThreshold).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Position).HasMaxLength(30);

                entity.Property(e => e.Prefix).HasMaxLength(10);

                entity.Property(e => e.ReferenceType).HasDefaultValueSql("((1))");

                entity.Property(e => e.RegulatedBy).HasMaxLength(100);

                entity.Property(e => e.RegulatorNumber).HasMaxLength(20);

                entity.Property(e => e.ScreenImage).IsUnicode(false);

                entity.Property(e => e.SignatoryAddress).HasMaxLength(500);

                entity.Property(e => e.SignatoryEmail).HasMaxLength(50);

                entity.Property(e => e.SignatoryForename).HasMaxLength(40);

                entity.Property(e => e.SignatoryPhone).HasMaxLength(20);

                entity.Property(e => e.SignatoryPostcode).HasMaxLength(20);

                entity.Property(e => e.SignatorySurname).HasMaxLength(50);

                entity.Property(e => e.SignatoryTitle).HasMaxLength(4);

                entity.Property(e => e.StripeApiKey).HasMaxLength(50);

                entity.Property(e => e.StripePublishableKey).HasMaxLength(50);

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.Property(e => e.Threshold).HasColumnType("money");

                entity.Property(e => e.TpdonationAmountLimit)
                    .HasColumnName("TPDonationAmountLimit")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TrueLayerAccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrueLayerAccountNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrueLayerAnnualIncrease).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TrueLayerReferencePrefix)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrueLayerSortCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Charity)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Charity_Agent");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Charity)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Charity_CentralOffice");

                entity.HasOne(d => d.ConnectedCharityNavigation)
                    .WithMany(p => p.Charity)
                    .HasForeignKey(d => d.ConnectedCharity)
                    .HasConstraintName("FK_Charity_ConnectedCharity");

                entity.HasOne(d => d.DefaultPurpose)
                    .WithMany(p => p.Charity)
                    .HasForeignKey(d => d.DefaultPurposeId)
                    .HasConstraintName("FK_Charity_Purpose");
            });

            modelBuilder.Entity<CharityApp>(entity =>
            {
                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.CharityApp)
                    .HasForeignKey(d => d.AppId)
                    .HasConstraintName("FK__CharityAp__AppId__2375C74B");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.CharityApp)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__CharityAp__Chari__2469EB84");
            });

            modelBuilder.Entity<CharityConnectedCharity>(entity =>
            {
                entity.HasKey(e => new { e.CharityId, e.ConnectedCharityId });

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ConnectedCharityId).HasColumnName("ConnectedCharityID");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.CharityConnectedCharity)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__CharityCo__Charity");

                entity.HasOne(d => d.ConnectedCharity)
                    .WithMany(p => p.CharityConnectedCharity)
                    .HasForeignKey(d => d.ConnectedCharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharityConnectedCharity_ConnectedCharity");
            });

            modelBuilder.Entity<CharityContactPerson>(entity =>
            {
                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.HouseName).HasMaxLength(100);

                entity.Property(e => e.HouseNumber).HasMaxLength(100);

                entity.Property(e => e.LandLineNo).HasMaxLength(50);

                entity.Property(e => e.MobileNo).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Postcode).HasMaxLength(20);

                entity.Property(e => e.StreetName).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(10);

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.CharityContactPerson)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Charity_CharityContactPerson_CharityID");
            });

            modelBuilder.Entity<CharityGoCardLessPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId);

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AnnualIncrease).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlanLink)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.CharityGoCardLessPlan)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharityGoCardLessPlan_CharityID");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.CharityGoCardLessPlan)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK__CharityGo__Metho__4A24B4C1");
            });

            modelBuilder.Entity<CheckType>(entity =>
            {
                entity.Property(e => e.CheckTypeId).HasColumnName("CheckTypeID");

                entity.Property(e => e.CheckAuthority).HasMaxLength(255);

                entity.Property(e => e.CheckTypeDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<Checked>(entity =>
            {
                entity.Property(e => e.CheckedId).HasColumnName("CheckedID");

                entity.Property(e => e.DateChecked).HasColumnType("date");

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.CheckTypeNavigation)
                    .WithMany(p => p.Checked)
                    .HasForeignKey(d => d.CheckType)
                    .HasConstraintName("FK_Checked_CheckType");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => new { e.CityName, e.CentralOfficeId, e.CountyId })
                    .HasName("UC_City_CentralOffice")
                    .IsUnique();

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CountyId).HasColumnName("CountyID");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_City_CentralOffice");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_Counties");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.AdjustmentId)
                    .HasColumnName("AdjustmentID")
                    .HasMaxLength(8);

                entity.Property(e => e.AdjustmentSent).HasMaxLength(50);

                entity.Property(e => e.AdjustmentValue).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ClaimAmount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.ClaimEnd).HasColumnType("datetime");

                entity.Property(e => e.ClaimStart).HasColumnType("datetime");

                entity.Property(e => e.CommBuildBranchId).HasColumnName("CommBuildBranchID");

                entity.Property(e => e.Comment).HasMaxLength(30);

                entity.Property(e => e.DateProcessed).HasColumnType("datetime");

                entity.Property(e => e.FeeAmount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Gasdsclaim).HasColumnName("GASDSCLaim");

                entity.Property(e => e.GasdsclaimTaxYear).HasColumnName("GASDSClaimTaxYear");

                entity.Property(e => e.NetValue).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.OverpaymentAmount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.RefundClaimId)
                    .HasColumnName("RefundClaimID")
                    .HasMaxLength(10);

                entity.Property(e => e.TaxRate).HasColumnType("decimal(2, 0)");

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.Claim)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claim__AuditId__34D3C6C9");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Claim)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK_Claim_Charity");

                entity.HasOne(d => d.CommBuildBranch)
                    .WithMany(p => p.Claim)
                    .HasForeignKey(d => d.CommBuildBranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Claim__CommBuild__17A35695");
            });

            modelBuilder.Entity<ClaimLog>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CharityName).HasMaxLength(100);

                entity.Property(e => e.ClaimNo).HasMaxLength(20);

                entity.Property(e => e.CommonReference).HasMaxLength(50);

                entity.Property(e => e.CorrelationId).HasMaxLength(50);

                entity.Property(e => e.ErrorMessage).HasMaxLength(500);

                entity.Property(e => e.ErrorSource).HasMaxLength(50);

                entity.Property(e => e.IsHmrcsubmit).HasColumnName("IsHMRCSubmit");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SentXml).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<ClaimNumberGeneration>(entity =>
            {
                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimNo).HasMaxLength(20);
            });

            modelBuilder.Entity<CommType>(entity =>
            {
                entity.Property(e => e.CommTypeId).HasColumnName("CommTypeID");

                entity.Property(e => e.CommTypeDescription).HasMaxLength(50);
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.Property(e => e.CommunicationId).HasColumnName("CommunicationID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CommReference).HasMaxLength(255);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Private).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Communication_Communication");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Communication_Charity");

                entity.HasOne(d => d.CommunicationTypeNavigation)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.CommunicationType)
                    .HasConstraintName("FK_Communication_CommType");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Communication_Person");
            });

            modelBuilder.Entity<ConnectedCharity>(entity =>
            {
                entity.HasIndex(e => new { e.Description, e.CentralOfficeId })
                    .HasName("UC_Description_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.ConnectedCharityId).HasColumnName("ConnectedCharityID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Hmrcreference)
                    .HasColumnName("HMRCReference")
                    .HasMaxLength(7);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ConnectedCharity)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Connected__Branc__0CFBAAF0");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.ConnectedCharity)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_ConnectedCharity_CentralOffice");

                entity.HasOne(d => d.CharityNavigation)
                    .WithMany(p => p.ConnectedCharity1)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Connected__Chari__0C0786B7");
            });

            modelBuilder.Entity<ContactHistory>(entity =>
            {
                entity.Property(e => e.ContactHistoryId).HasColumnName("ContactHistoryID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.ContactDate).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(200);

                entity.Property(e => e.InboundOutbound).HasMaxLength(1);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.ContactTypeNavigation)
                    .WithMany(p => p.ContactHistory)
                    .HasForeignKey(d => d.ContactType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactHistory_ContactType");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ContactHistory)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_ContactHistory_Person");
            });

            modelBuilder.Entity<ContactLessDonorToken>(entity =>
            {
                entity.HasIndex(e => e.Token)
                    .HasName("uq_donor_token")
                    .IsUnique();

                entity.HasIndex(e => new { e.Token, e.BranchId })
                    .HasName("uq_ContactLessDonorToken")
                    .IsUnique();

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DonorReceiptEmail).HasMaxLength(100);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ContactLessDonorToken)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ContactLe__Branc__0ED9D1C1");

                entity.HasOne(d => d.Donor)
                    .WithMany(p => p.ContactLessDonorToken)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ContactLe__Donor__265C5597");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContactLessDonorToken)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ContactLe__UserI__34FF7AA9");
            });

            modelBuilder.Entity<ContactLessSchedule>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecentDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TerminalId).HasColumnName("TerminalID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ContactLessSchedule)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ContactLe__Branc__4456CCD2");

                entity.HasOne(d => d.Terminal)
                    .WithMany(p => p.ContactLessSchedule)
                    .HasForeignKey(d => d.TerminalId)
                    .HasConstraintName("FK__ContactLe__Termi__2DB35E9C_Cascade");
            });

            modelBuilder.Entity<ContactLessScheduleDate>(entity =>
            {
                entity.HasKey(e => new { e.ScheduleId, e.ScheduleDate });

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.ScheduleDate).HasColumnType("datetime");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ContactLessScheduleDate)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FK__ContactLe__Sched__39251148_Cascade");
            });

            modelBuilder.Entity<ContactLessTerminal>(entity =>
            {
                entity.HasIndex(e => e.TerminalId)
                    .HasName("UQ_Terminal")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastImportDate).HasColumnType("datetime");

                entity.Property(e => e.MerchantId).HasColumnName("MerchantID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StartedImportDate).HasColumnType("datetime");

                entity.Property(e => e.TerminalId)
                    .HasColumnName("TerminalID")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ContactLessTerminal)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__ContactLe__Branc__32381C97");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.ContactLessTerminal)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ContactLe__Chari__332C40D0");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.HasIndex(e => new { e.ContactTypeDescription, e.CentralOfficeId })
                    .HasName("UC_ContactTypeDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.ContactTypeId).HasColumnName("ContactTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ContactTypeDescription).HasMaxLength(30);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ContactType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__ContactTy__Branc__092B1A0C");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.ContactType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_ContactType_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.ContactType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__ContactTy__Chari__0836F5D3");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => new { e.CountryName, e.CentralOfficeId })
                    .HasName("uc_Country")
                    .IsUnique();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Abbreviation).HasMaxLength(20);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Country__BranchI__0EE3F362");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Country_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Country__Charity__0DEFCF29");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.HasIndex(e => new { e.CountyName, e.CentralOfficeId })
                    .HasName("uc_County_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.CountyId).HasColumnName("CountyID");

                entity.Property(e => e.Abbreviation).HasMaxLength(20);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.County)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_County_CentralOffice");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.County)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Counties_Countries");
            });

            modelBuilder.Entity<DataDevelopmentProcessFeeDetails>(entity =>
            {
                entity.HasKey(e => e.DataDevelopmentProcessFeeSummaryId)
                    .HasName("PK__DataDeve__2E14E0D8EE12789D");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MonthDate).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.DataDevelopmentProcessFeeDetails)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__DataDevel__Centr__6C8FA6B5");
            });

            modelBuilder.Entity<DataImportErrorLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.TableName).HasMaxLength(50);
            });

            modelBuilder.Entity<DataImportLog>(entity =>
            {
                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.DataImportStatusId).HasColumnName("DataImportStatusID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ImportedFile).HasMaxLength(100);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.DataImportLog)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_DataImportLog_CentralOffice");
            });

            modelBuilder.Entity<DataImportStatus>(entity =>
            {
                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ImportedFile).HasMaxLength(100);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DataNotImport>(entity =>
            {
                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.Property(e => e.TableName).HasMaxLength(100);

                entity.Property(e => e.TableRowPk)
                    .HasColumnName("TableRowPK")
                    .HasMaxLength(100);

                entity.HasOne(d => d.DataImportLog)
                    .WithMany(p => p.DataNotImport)
                    .HasForeignKey(d => d.DataImportLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DataNotIm__DataI__782B7693");
            });

            modelBuilder.Entity<Dcusers>(entity =>
            {
                entity.ToTable("DCUsers");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Declaration>(entity =>
            {
                entity.Property(e => e.DeclarationId).HasColumnName("DeclarationID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.DateDeclarationSigned).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationValidFrom).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationValidTo).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Declaration)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Declaration_Person");
            });

            modelBuilder.Entity<DeclarationHistory>(entity =>
            {
                entity.HasKey(e => e.DeclarationId);

                entity.Property(e => e.DeclarationId).HasColumnName("DeclarationID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.DateDeclarationSigned).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationValidFrom).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationValidTo).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DeclarationHistory)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeclarationHistory_Person");
            });

            modelBuilder.Entity<DeclarationHistoryVerify>(entity =>
            {
                entity.HasKey(e => e.DeclarationId);

                entity.Property(e => e.DeclarationId).HasColumnName("DeclarationID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.DateDeclarationCurrentEnd).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationSigned).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationValidFrom).HasColumnType("datetime");

                entity.Property(e => e.DateDeclarationValidTo).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DeclarationHistoryVerify)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeclarationHistoryVerify_Person");
            });

            modelBuilder.Entity<DeleteTablesData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BatchReference).HasMaxLength(50);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.ContactLessTerminalId)
                    .HasColumnName("ContactLessTerminalID")
                    .HasMaxLength(100);

                entity.Property(e => e.CpkGift).HasColumnName("cpkGift");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.DonationTypeId).HasColumnName("DonationTypeID");

                entity.Property(e => e.DonorId).HasColumnName("DonorID");

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ExternalDonId)
                    .HasColumnName("ExternalDonID")
                    .HasMaxLength(50);

                entity.Property(e => e.Gasdseligible).HasColumnName("GASDSEligible");

                entity.Property(e => e.IsTransferedToFc)
                    .HasColumnName("IsTransferedToFC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTransferedToFcweb).HasColumnName("IsTransferedToFCWeb");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RegularGiftId).HasColumnName("RegularGiftID");

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donation_Branch");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Donation_Claim");

                entity.HasOne(d => d.DonationType)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.DonationTypeId)
                    .HasConstraintName("FK_Donation_DonationType");

                entity.HasOne(d => d.Donor)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Donation_Person");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_Donation_Envelope");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Donation_Event");

                entity.HasOne(d => d.FrequencyNavigation)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.Frequency)
                    .HasConstraintName("FK_Donation_Frequency");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK_Donation_Method");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK_Donation_Purpose");
            });

            modelBuilder.Entity<DonationGoToLive>(entity =>
            {
                entity.HasKey(e => e.DonationId);

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BatchReference).HasMaxLength(50);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.DonationTypeId).HasColumnName("DonationTypeID");

                entity.Property(e => e.DonorId).HasColumnName("DonorID");

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.Gasdseligible).HasColumnName("GASDSEligible");

                entity.Property(e => e.IsTransferedToFc).HasColumnName("IsTransferedToFC");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RegularGiftId).HasColumnName("RegularGiftID");
            });

            modelBuilder.Entity<DonationType>(entity =>
            {
                entity.Property(e => e.DonationTypeId).HasColumnName("DonationTypeID");

                entity.Property(e => e.DonationTypeDescription)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DonorFoodbank>(entity =>
            {
                entity.HasOne(d => d.Donor)
                    .WithMany(p => p.DonorFoodbank)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonorFood__Donor__62920686");

                entity.HasOne(d => d.FoodBank)
                    .WithMany(p => p.DonorFoodbank)
                    .HasForeignKey(d => d.FoodBankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonorFood__FoodB__63862ABF");
            });

            modelBuilder.Entity<DonorToken>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK_OlgaDonorToken");

                entity.Property(e => e.TokenId).HasColumnName("TokenID");

                entity.Property(e => e.Token).HasMaxLength(200);

                entity.Property(e => e.TokenDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Envelope>(entity =>
            {
                entity.HasIndex(e => new { e.EnvelopeNumber, e.PersonId, e.Comment })
                    .HasName("UC_EnvelopeNumber_PersonID")
                    .IsUnique();

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.EnvelopeNumber).HasMaxLength(10);

                entity.Property(e => e.OldEnvelopeNumber).HasMaxLength(8);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Envelope)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Envelope__Branch__10CC3BD4");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Envelope)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Envelope_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Envelope)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Envelope__Charit__0FD8179B");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Envelope)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Envelope_Person");
            });

            modelBuilder.Entity<EnvelopeSetting>(entity =>
            {
                entity.Property(e => e.EnvelopeSettingId).HasColumnName("EnvelopeSettingID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Names).HasMaxLength(20);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.EnvelopeSetting)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__EnvelopeS__Branc__20E37363");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.EnvelopeSetting)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__EnvelopeS__Centr__1EFB2AF1");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.EnvelopeSetting)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__EnvelopeS__Chari__1FEF4F2A");
            });

            modelBuilder.Entity<Ethnicity>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventDescription).HasMaxLength(255);

                entity.Property(e => e.EventLocation).HasMaxLength(255);

                entity.Property(e => e.EventName).HasMaxLength(50);

                entity.Property(e => e.EventReference).HasMaxLength(8);

                entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Event_Charity");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("FK_Event_EventType");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

                entity.Property(e => e.EventTypeDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<ExceptionLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Controller).HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Method).HasMaxLength(100);

                entity.Property(e => e.OrganisationId).HasColumnName("OrganisationID");

                entity.Property(e => e.PageName).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ExceptionLog)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Exception__Branc__2157A958");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.ExceptionLog)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Exception__Chari__224BCD91");

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.ExceptionLog)
                    .HasForeignKey(d => d.OrganisationId)
                    .HasConstraintName("FK__Exception__Organ__233FF1CA");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExceptionLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Exception__UserI__24341603");
            });

            modelBuilder.Entity<FailedBtdonationEmailDetail>(entity =>
            {
                entity.ToTable("FailedBTDonationEmailDetail");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FiledDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.FailedBtdonationEmailDetail)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__FailedBTD__Branc__7B9CE01B");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.FailedBtdonationEmailDetail)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__FailedBTD__Chari__7C910454");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Contactno)
                    .HasColumnName("contactno")
                    .HasMaxLength(500);

                entity.Property(e => e.DeliveryNote).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FamilyToken)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gdprpreferences)
                    .HasColumnName("GDPRPreferences")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParcelDeliverDate).HasColumnType("datetime");

                entity.Property(e => e.PostponeDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAdults)
                    .HasColumnName("total_adults")
                    .HasMaxLength(500);

                entity.Property(e => e.TotalChild)
                    .HasColumnName("total_child")
                    .HasMaxLength(500);

                entity.Property(e => e.TotalFamily)
                    .HasColumnName("total_family")
                    .HasMaxLength(500);

                entity.Property(e => e.VoucherNumber).HasMaxLength(20);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Family)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Family__BranchId__2D2A1A0E");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Family)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__Family__CentralO__2A4DAD63");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Family)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Family__CharityI__2C35F5D5");
            });

            modelBuilder.Entity<FamilyAddress>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.FamilyAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyAdd__Addre__77C22D96");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyAddress)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyAdd__Famil__76CE095D");
            });

            modelBuilder.Entity<FamilyAgency>(entity =>
            {
                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.FamilyAgency)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyAge__Agenc__02C9CBEE");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyAgency)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyAge__Famil__01D5A7B5");
            });

            modelBuilder.Entity<FamilyMember>(entity =>
            {
                entity.Property(e => e.ContactNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.FamilyId).HasColumnName("Family_Id");

                entity.Property(e => e.ForeName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Enthcicity)
                    .WithMany(p => p.FamilyMember)
                    .HasForeignKey(d => d.EnthcicityId)
                    .HasConstraintName("FK__FamilyMem__Enthc__22AC8B9B");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyMember)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyMem__Famil__21B86762");
            });

            modelBuilder.Entity<FamilyMemberAllergy>(entity =>
            {
                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.FamilyMemberAllergy)
                    .HasForeignKey(d => d.AllergyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyMem__Aller__286564F1");

                entity.HasOne(d => d.FamilyMember)
                    .WithMany(p => p.FamilyMemberAllergy)
                    .HasForeignKey(d => d.FamilyMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyMem__Famil__277140B8");
            });

            modelBuilder.Entity<FamilyNotes>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK__FamilyNo__EACE357F909BF3D5");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

                entity.Property(e => e.NoteDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FamilyNotes)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyNot__Creat__7B28AA26");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyNotes)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyNot__Famil__7A3485ED");
            });

            modelBuilder.Entity<FamilyParcelFoodItem>(entity =>
            {
                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FamilyParcelFoodItem)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyPar__FoodI__0976C97D");

                entity.HasOne(d => d.Parcel)
                    .WithMany(p => p.FamilyParcelFoodItem)
                    .HasForeignKey(d => d.ParcelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FamilyPar__Parce__0882A544");
            });

            modelBuilder.Entity<Fbaddress>(entity =>
            {
                entity.ToTable("FBAddress");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CountryName).HasMaxLength(200);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.HouseName).HasMaxLength(200);

                entity.Property(e => e.HouseNumber).HasMaxLength(200);

                entity.Property(e => e.OtherAddressLine).HasMaxLength(200);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Fbaddress)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__FBAddress__Count__2F126280");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Fbaddress)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__FBAddress__UserI__0022696D");
            });

            modelBuilder.Entity<Fbcontact>(entity =>
            {
                entity.ToTable("FBContact");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ForeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(30);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrganisationName).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Surname).HasMaxLength(200);
            });

            modelBuilder.Entity<Fblocation>(entity =>
            {
                entity.ToTable("FBLocation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SocialMedia).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Fblocation)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__FBLocatio__Addre__72FD7879");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Fblocation)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK__FBLocatio__Conta__73F19CB2");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Fblocation)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FBLocatio__Famil__70210BCE");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.DateCompletd).HasColumnType("date");

                entity.Property(e => e.FoodbankId).HasColumnName("FoodbankID");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__Family__4F7F3212");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.FoodbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__Foodba__51677A84");

                entity.HasOne(d => d.Parcel)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.ParcelId)
                    .HasConstraintName("FK__Feedback__Parcel__572053DA");
            });

            modelBuilder.Entity<FeedbackFormDetails>(entity =>
            {
                entity.Property(e => e.Answer).HasMaxLength(500);

                entity.Property(e => e.FeedbackMasterFieldOptionId).HasColumnName("FeedbackMasterFieldOptionID");

                entity.Property(e => e.FeedbackMasterId).HasColumnName("FeedbackMasterID");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackFormDetails)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackF__Feedb__5443E72F");

                entity.HasOne(d => d.FeedbackMasterFieldOption)
                    .WithMany(p => p.FeedbackFormDetails)
                    .HasForeignKey(d => d.FeedbackMasterFieldOptionId)
                    .HasConstraintName("FK__FeedbackF__Feedb__562C2FA1");

                entity.HasOne(d => d.FeedbackMaster)
                    .WithMany(p => p.FeedbackFormDetails)
                    .HasForeignKey(d => d.FeedbackMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackF__Feedb__55380B68");
            });

            modelBuilder.Entity<FeedbackMaster>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("PK__Feedback__C8B6FF27DAAA99DC");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(40);

                entity.Property(e => e.FieldDefaultValue).HasMaxLength(500);

                entity.Property(e => e.FieldDescription).HasMaxLength(500);

                entity.Property(e => e.FoodbankId).HasColumnName("FoodbankID");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.FeedbackMaster)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__FeedbackM__Foodb__49C658BC");
            });

            modelBuilder.Entity<FeedbackMasterFieldOption>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PK__Feedback__92C7A1DF6A08F83A");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.OptionValue).HasMaxLength(500);

                entity.Property(e => e.UserDefinedFieldId).HasColumnName("UserDefinedFieldID");

                entity.HasOne(d => d.UserDefinedField)
                    .WithMany(p => p.FeedbackMasterFieldOption)
                    .HasForeignKey(d => d.UserDefinedFieldId)
                    .HasConstraintName("FK__FeedbackM__UserD__389BCCBA");
            });

            modelBuilder.Entity<FilterCriteria>(entity =>
            {
                entity.Property(e => e.FilterCriteriaId).HasColumnName("FilterCriteriaID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParentCriteriaId).HasColumnName("ParentCriteriaID");

                entity.Property(e => e.SmartFilterId).HasColumnName("SmartFilterID");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value1)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Value2).HasMaxLength(200);

                entity.Property(e => e.Value3).HasMaxLength(200);

                entity.Property(e => e.Value4).HasMaxLength(200);

                entity.HasOne(d => d.ParentCriteria)
                    .WithMany(p => p.InverseParentCriteria)
                    .HasForeignKey(d => d.ParentCriteriaId)
                    .HasConstraintName("FK_FilterCriteria_FilterCriteria");

                entity.HasOne(d => d.SmartFilter)
                    .WithMany(p => p.FilterCriteria)
                    .HasForeignKey(d => d.SmartFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilterCriteria_SmartFilter");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.Barcode).HasMaxLength(145);

                entity.Property(e => e.CategoryApiId).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(145);

                entity.Property(e => e.ProductIdApi)
                    .HasMaxLength(540)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CauseofDonation).HasMaxLength(500);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.QuantityUnit).HasMaxLength(20);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Donor)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.Donorid)
                    .HasConstraintName("FK__FoodItem__Donori__50A86075");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__FoodItem__Foodba__27072C64");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.Foodid)
                    .HasConstraintName("FK__FoodItem__Foodid__4FB43C3C");

                entity.HasOne(d => d.Grantor)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.GrantorId)
                    .HasConstraintName("FK__FoodItem__Granto__251EE3F2");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("FK__FoodItem__StockI__2613082B");
            });

            modelBuilder.Entity<FoodItemAllergy>(entity =>
            {
                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.FoodItemAllergy)
                    .HasForeignKey(d => d.AllergyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodItemA__Aller__2EA84E2C");

                entity.HasOne(d => d.FoodItem)
                    .WithMany(p => p.FoodItemAllergy)
                    .HasForeignKey(d => d.FoodItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodItemA__FoodI__2F9C7265");
            });

            modelBuilder.Entity<Foodbank>(entity =>
            {
                entity.Property(e => e.FeedbackResponse)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FeedbackReturnUrl)
                    .HasColumnName("FeedbackReturnURl")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.SelfReferralToken)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Foodbank)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Foodbank__Addres__48D23483");

                entity.HasOne(d => d.InsertByNavigation)
                    .WithMany(p => p.FoodbankInsertByNavigation)
                    .HasForeignKey(d => d.InsertBy)
                    .HasConstraintName("FK__Foodbank__Insert__5EC175A2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FoodbankUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Foodbank__UserId__5FB599DB");
            });

            modelBuilder.Entity<FoodbankFamily>(entity =>
            {
                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FoodbankFamily)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodbankF__Famil__5AF0E4BE");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.FoodbankFamily)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__FoodbankF__Foodb__5BE508F7");
            });

            modelBuilder.Entity<FoodbankMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.MenuIcon).HasMaxLength(50);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuUrl).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");
            });

            modelBuilder.Entity<FoodbankRecipe>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Ingredients).HasMaxLength(500);

                entity.Property(e => e.RecipeNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RecipeTitle).HasMaxLength(500);

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.FoodbankRecipe)
                    .HasForeignKey(d => d.FoodbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodbankRecipe_Foodbank");
            });

            modelBuilder.Entity<FoodbankRecipeFoodItem>(entity =>
            {
                entity.Property(e => e.FoodbankRecipeId).HasColumnName("FoodbankRecipeID");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodbankRecipeFoodItem)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__FoodbankR__FoodI__34612782");

                entity.HasOne(d => d.FoodbankRecipe)
                    .WithMany(p => p.FoodbankRecipeFoodItem)
                    .HasForeignKey(d => d.FoodbankRecipeId)
                    .HasConstraintName("FK__FoodbankR__Foodb__336D0349");
            });

            modelBuilder.Entity<FoodbankRoleMenuPrivilege>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId })
                    .HasName("PK__Foodbank__71317EB3B2D8833B");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.FoodbankRoleMenuPrivilege)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodbankR__MenuI__205A2ED5");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.FoodbankRoleMenuPrivilege)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodbankR__RoleI__214E530E");
            });

            modelBuilder.Entity<FoodbankSetting>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.DashboardImage).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.LogoImage).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(500);

                entity.Property(e => e.TailoredNotes).HasMaxLength(500);

                entity.HasOne(d => d.FoodBank)
                    .WithMany(p => p.FoodbankSetting)
                    .HasForeignKey(d => d.FoodBankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodbankSetting_Foodbank");
            });

            modelBuilder.Entity<FoodbankUserDataAccessibility>(entity =>
            {
                entity.HasKey(e => e.UserAccessId)
                    .HasName("PK__Foodbank__BA8431D197576751");

                entity.Property(e => e.UserAccessId).HasColumnName("UserAccessID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FoodbankUserDataAccessibility)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodbankU__UserI__242ABFB9");
            });

            modelBuilder.Entity<ForgotPassword>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ForgotPassword)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForgotPas__UserI__53041385");
            });

            modelBuilder.Entity<Frequency>(entity =>
            {
                entity.Property(e => e.FrequencyId).HasColumnName("FrequencyID");

                entity.Property(e => e.FrequencyDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<Gasds>(entity =>
            {
                entity.ToTable("GASDS");

                entity.Property(e => e.Gasdsid).HasColumnName("GASDSID");

                entity.Property(e => e.Allocation).HasColumnType("decimal(6, 0)");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Gasds)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GASDS_Branch");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Gasds)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GASDS_Charity");
            });

            modelBuilder.Entity<GoCardLessPayment>(entity =>
            {
                entity.Property(e => e.GoCardLessPaymentId).HasColumnName("GoCardLessPaymentID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasMaxLength(50);

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Donation)
                    .WithMany(p => p.GoCardLessPayment)
                    .HasForeignKey(d => d.DonationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoCardLessPayment_Donation");
            });

            modelBuilder.Entity<GoCardlessNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PK_GoCardless_Notification");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EventId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mandate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationDate).HasColumnType("datetime");

                entity.Property(e => e.Origin)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResourceTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.GoCardlessNotification)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK_Charity_CharityId_GoCardlessNotification_CharityId");
            });

            modelBuilder.Entity<GoCardlessSubscription>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.AnnualIncrease).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Currency).HasMaxLength(20);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GocardlessCustomerId).HasMaxLength(100);

                entity.Property(e => e.Mandate).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.SubscriptionId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SubscriptionStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.GoCardlessSubscription)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoCardles__Chari__477D5240");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.GoCardlessSubscription)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK__GoCardles__Metho__4B18D8FA");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.GoCardlessSubscription)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoCardles__Perso__48717679");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.GoCardlessSubscription)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__GoCardles__PlanI__49659AB2");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.GoCardlessSubscription)
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK__GoCardles__Purpo__4A59BEEB");
            });

            modelBuilder.Entity<Granter>(entity =>
            {
                entity.Property(e => e.GranterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GranterToken)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Granter)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Granter__Contact__64AF5922");
            });

            modelBuilder.Entity<GranterReceipt>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedInMfo).HasColumnName("CreatedInMFO");

                entity.Property(e => e.ReceiptImage).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Granter)
                    .WithMany(p => p.GranterReceipt)
                    .HasForeignKey(d => d.GranterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GranterRe__Grant__678BC5CD");
            });

            modelBuilder.Entity<Grantor>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.ForeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GrantorToken).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SurName).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Grantor)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grantor__Address__6E03B932");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Grantor)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grantor__Contact__693F0415");

                entity.HasOne(d => d.FoodBank)
                    .WithMany(p => p.Grantor)
                    .HasForeignKey(d => d.FoodBankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grantor__FoodBan__684ADFDC");
            });

            modelBuilder.Entity<GrantorReceipt>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedInMfo).HasColumnName("CreatedInMFO");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptImage).HasMaxLength(100);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.TransectionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Grantor)
                    .WithMany(p => p.GrantorReceipt)
                    .HasForeignKey(d => d.GrantorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GrantorRe__Grant__6C1B70C0");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.DateEnded).HasColumnType("date");

                entity.Property(e => e.DateStarted).HasColumnType("date");

                entity.Property(e => e.GroupDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<GroupGroupType>(entity =>
            {
                entity.Property(e => e.GroupGroupTypeId)
                    .HasColumnName("GroupGroupTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupTypeId).HasColumnName("GroupTypeID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupGroupType)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupGroupType_Group");

                entity.HasOne(d => d.GroupType)
                    .WithMany(p => p.GroupGroupType)
                    .HasForeignKey(d => d.GroupTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupGroupType_GroupType");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.Property(e => e.GroupMemberId)
                    .HasColumnName("GroupMemberID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateJoined).HasColumnType("datetime");

                entity.Property(e => e.DateLeft).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMemberGroup)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupMember_Group");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.GroupMemberPerson)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMember_Person");
            });

            modelBuilder.Entity<GroupType>(entity =>
            {
                entity.Property(e => e.GroupTypeId).HasColumnName("GroupTypeID");

                entity.Property(e => e.GroupTypeDescription).HasMaxLength(50);
            });

            modelBuilder.Entity<Household>(entity =>
            {
                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.Envelope).HasMaxLength(255);

                entity.Property(e => e.FamilyAdded).HasColumnType("datetime");

                entity.Property(e => e.Formal).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.HouseholdDescription).HasMaxLength(255);

                entity.Property(e => e.InActive).HasColumnType("datetime");

                entity.Property(e => e.InFormal).HasMaxLength(255);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Mccpkhousehold)
                    .HasColumnName("MCCPKHOUSEHOLD")
                    .HasMaxLength(250);

                entity.Property(e => e.MembershipId)
                    .HasColumnName("MembershipID")
                    .HasMaxLength(50);

                entity.Property(e => e.Photo).HasMaxLength(255);
            });

            modelBuilder.Entity<ImportLog>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__ImportLo__C8EE20435A8B9507");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ImportedFile).HasMaxLength(100);

                entity.Property(e => e.Service).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ImportLogDetail>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__ImportLo__5E5499A84C1296B7");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ImportLogDetail)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__ImportLog__Statu__4925A390");
            });

            modelBuilder.Entity<InitialContact>(entity =>
            {
                entity.Property(e => e.InitialContactId).HasColumnName("InitialContactID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.InitialContactDescription).HasMaxLength(255);

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.InitialContact)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK_InitialContact_Charity");
            });

            modelBuilder.Entity<JustGivingPaymentRef>(entity =>
            {
                entity.HasKey(e => e.JustGivingPayRefId);

                entity.Property(e => e.LastImportDate).HasColumnType("datetime");

                entity.Property(e => e.PayRefId).HasMaxLength(500);

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.JustGivingPaymentRef)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_JustGivingPaymentRef_Charity");
            });

            modelBuilder.Entity<KycXcharity>(entity =>
            {
                entity.HasKey(e => e.KycId)
                    .HasName("PK__KycXChar__58CB43CEA0657329");

                entity.ToTable("KycXCharity");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.KycXcharity)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KycXCharity_Charity");
            });

            modelBuilder.Entity<Letter>(entity =>
            {
                entity.Property(e => e.LetterId).HasColumnName("LetterID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Greeting).HasMaxLength(20);

                entity.Property(e => e.LetterHeadImage).HasMaxLength(100);

                entity.Property(e => e.Names).HasMaxLength(20);

                entity.Property(e => e.OrgNames).HasMaxLength(10);

                entity.Property(e => e.PostCode).HasMaxLength(20);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Letter)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Letter__BranchID__12B48446");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Letter)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Letter_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Letter)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Letter__CharityI__11C0600D");
            });

            modelBuilder.Entity<Licence>(entity =>
            {
                entity.Property(e => e.LicenceId).HasColumnName("LicenceID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LicenceNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Licence)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Licence_CentralOffice");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CurrentPage).HasMaxLength(255);

                entity.Property(e => e.ErrorDescription).HasMaxLength(500);

                entity.Property(e => e.ErrorNumber).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogEntry).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Log_User");
            });

            modelBuilder.Entity<Mcusers>(entity =>
            {
                entity.ToTable("MCUsers");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.MeetingId).HasColumnName("MeetingID");

                entity.Property(e => e.FrequencyId).HasColumnName("FrequencyID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.MeetingDate).HasColumnType("date");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.HasOne(d => d.Frequency)
                    .WithMany(p => p.Meeting)
                    .HasForeignKey(d => d.FrequencyId)
                    .HasConstraintName("FK_Meeting_Frequency");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Meeting)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Meeting_Group");
            });

            modelBuilder.Entity<MeetingSchedule>(entity =>
            {
                entity.Property(e => e.MeetingScheduleId).HasColumnName("MeetingScheduleID");

                entity.Property(e => e.MeetingId).HasColumnName("MeetingID");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.MeetingSchedule)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeetingSchedule_Meeting");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.MeetingSchedule)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeetingSchedule_Schedule");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.MenuIcon).HasMaxLength(50);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuUrl).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");

                entity.HasOne(d => d.ParentMenu)
                    .WithMany(p => p.InverseParentMenu)
                    .HasForeignKey(d => d.ParentMenuId)
                    .HasConstraintName("FK_Menu_ParentMenu");
            });

            modelBuilder.Entity<Method>(entity =>
            {
                entity.HasIndex(e => new { e.MethodDescription, e.CentralOfficeId })
                    .HasName("uc_Method_CentralOffic")
                    .IsUnique();

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.MethodDescription).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Method)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Method__BranchID__055A8928");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Method)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Method_Centraloffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Method)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__Method__CharityI__046664EF");
            });

            modelBuilder.Entity<MethodAccess>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MethodAccess)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MethodAcc__Branc__752F0E57");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.MethodAccess)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MethodAcc__Metho__76233290");
            });

            modelBuilder.Entity<MmoactivityLog>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__MMOActiv__45F4A7F1277F2199");

                entity.ToTable("MMOActivityLog");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityDetail).IsRequired();

                entity.Property(e => e.ActivityType).HasMaxLength(200);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MmoattendanceCode>(entity =>
            {
                entity.HasKey(e => e.AttendanceCodeId)
                    .HasName("PK__MMOAtten__C93ABFBA8441F4CA");

                entity.ToTable("MMOAttendanceCode");

                entity.HasIndex(e => new { e.AttendanceCodeDescription, e.CentralOfficeId })
                    .HasName("UC_AttendanceCodeDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.AttendanceCodeId).HasColumnName("AttendanceCodeID");

                entity.Property(e => e.AttendanceCodeDescription).HasMaxLength(255);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpkreason)
                    .HasColumnName("MCCPKREASON")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmoattendanceCode)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOAttend__Branc__359F647E");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmoattendanceCode)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOAttend__Centr__33B71C0C");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmoattendanceCode)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOAttend__Chari__34AB4045");
            });

            modelBuilder.Entity<MmobraintreeTransaction>(entity =>
            {
                entity.ToTable("MMOBraintreeTransaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MmobraintreeUserDefinedField>(entity =>
            {
                entity.ToTable("MMOBraintreeUserDefinedField");

                entity.Property(e => e.MmobraintreeUserDefinedFieldId).HasColumnName("MMOBraintreeUserDefinedFieldId");

                entity.Property(e => e.BraintreeCustomerId)
                    .IsRequired()
                    .HasColumnName("BraintreeCustomerID")
                    .HasMaxLength(100);

                entity.Property(e => e.FieldValue).HasMaxLength(100);

                entity.HasOne(d => d.Button)
                    .WithMany(p => p.MmobraintreeUserDefinedField)
                    .HasForeignKey(d => d.ButtonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOBraint__Butto__77031387");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmobraintreeUserDefinedField)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOBraint__Chari__77F737C0");

                entity.HasOne(d => d.EnrolementForm)
                    .WithMany(p => p.MmobraintreeUserDefinedField)
                    .HasForeignKey(d => d.EnrolementFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOBraint__Enrol__78EB5BF9");
            });

            modelBuilder.Entity<MmobranchToken>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("PK__MMOBranc__A1682FA5325F964A");

                entity.ToTable("MMOBranchToken");

                entity.Property(e => e.BranchId)
                    .HasColumnName("BranchID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Mmotoken)
                    .IsRequired()
                    .HasColumnName("MMOToken")
                    .HasMaxLength(60);

                entity.HasOne(d => d.Branch)
                    .WithOne(p => p.MmobranchToken)
                    .HasForeignKey<MmobranchToken>(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MMOBranchToken_Branch");
            });

            modelBuilder.Entity<Mmocertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateId)
                    .HasName("PK__MMOCerti__BBF8A7E147913E39");

                entity.ToTable("MMOCertificate");

                entity.HasIndex(e => new { e.CertificateName, e.CentralOfficeId, e.CharityId, e.BranchId })
                    .HasName("UC_CertificateName_CentralOfficeID_CharityID_BranchID")
                    .IsUnique();

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CertificateName).HasMaxLength(255);

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpksecuritycheckcode)
                    .HasColumnName("MCCPKSECURITYCHECKCODE")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Mmocertificate)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOCertif__Branc__469F07F7");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Mmocertificate)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOCertif__Centr__44B6BF85");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Mmocertificate)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOCertif__Chari__45AAE3BE");
            });

            modelBuilder.Entity<MmocertificateIssuer>(entity =>
            {
                entity.HasKey(e => e.CertificateIssuerId)
                    .HasName("PK__MMOCerti__582EDFB3B0BB8D69");

                entity.ToTable("MMOCertificateIssuer");

                entity.HasIndex(e => new { e.IssuerCompanyName, e.CentralOfficeId })
                    .HasName("UC_IssuerCompanyName_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.CertificateIssuerId).HasColumnName("CertificateIssuerID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.County).HasMaxLength(50);

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.HouseName).HasMaxLength(255);

                entity.Property(e => e.HouseNumber).HasMaxLength(255);

                entity.Property(e => e.IssuerCompanyAddress).HasMaxLength(500);

                entity.Property(e => e.IssuerCompanyName).HasMaxLength(255);

                entity.Property(e => e.Mccpksecuritycheckcode)
                    .HasColumnName("MCCPKSECURITYCHECKCODE")
                    .HasMaxLength(250);

                entity.Property(e => e.OtherAddressLine).HasMaxLength(255);

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.StreetName).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmocertificateIssuer)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOCertif__Branc__4C57E14D");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmocertificateIssuer)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOCertif__Centr__4A6F98DB");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmocertificateIssuer)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOCertif__Chari__4B63BD14");
            });

            modelBuilder.Entity<Mmocontact>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__MMOConta__5C6625BB588426D0");

                entity.ToTable("MMOContact");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.ContactTypeId).HasColumnName("ContactTypeID");

                entity.Property(e => e.DefaultContactTypeId).HasColumnName("DefaultContactTypeID");

                entity.Property(e => e.Detail).HasMaxLength(255);

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.Mccpkphone)
                    .HasColumnName("MCCPKPHONE")
                    .HasMaxLength(250);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Mmocontact)
                    .HasForeignKey(d => d.ContactTypeId)
                    .HasConstraintName("FK__MMOContac__Conta__0EC4C328");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.Mmocontact)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK__MMOContac__House__0FB8E761");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Mmocontact)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOContac__Perso__10AD0B9A");
            });

            modelBuilder.Entity<MmocontactType>(entity =>
            {
                entity.HasKey(e => e.ContactTypeId)
                    .HasName("PK__MMOConta__17E1EE72D336029D");

                entity.ToTable("MMOContactType");

                entity.HasIndex(e => new { e.ContactTypeDescription, e.CentralOfficeId })
                    .HasName("UC_MMO_ContactTypeDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.ContactTypeId).HasColumnName("ContactTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ContactTypeDescription).HasMaxLength(255);

                entity.Property(e => e.Mccpkphonetype)
                    .HasColumnName("MCCPKPHONETYPE")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmocontactType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOContac__Branc__6ABC6CDC");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmocontactType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOContac__Centr__68D4246A");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmocontactType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOContac__Chari__69C848A3");
            });

            modelBuilder.Entity<Mmocorrespondence>(entity =>
            {
                entity.HasKey(e => e.CorrespondenceId)
                    .HasName("PK__MMOCorre__709EC6225E4F9322");

                entity.ToTable("MMOCorrespondence");

                entity.Property(e => e.CorrespondenceId).HasColumnName("CorrespondenceID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.Mmocorrespondence)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK__MMOCorres__House__527ACEF7");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Mmocorrespondence)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOCorres__Perso__536EF330");
            });

            modelBuilder.Entity<MmodataImportErrorLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("MMODataImportErrorLog");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.TableName).HasMaxLength(50);
            });

            modelBuilder.Entity<MmodataImportLog>(entity =>
            {
                entity.ToTable("MMODataImportLog");

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.DataImportStatusId).HasColumnName("DataImportStatusID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ImportedFile).HasMaxLength(100);

                entity.Property(e => e.IsOverwriteMgo).HasColumnName("IsOverwriteMGO");

                entity.Property(e => e.IsRemoveDataFromMgo).HasColumnName("IsRemoveDataFromMGO");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmodataImportLog)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_MMODataImportLog_CentralOffice");
            });

            modelBuilder.Entity<MmodataImportStatus>(entity =>
            {
                entity.ToTable("MMODataImportStatus");

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ImportedFile).HasMaxLength(100);

                entity.Property(e => e.IsOverwriteMgo).HasColumnName("IsOverwriteMGO");

                entity.Property(e => e.IsRemoveDataFromMgo).HasColumnName("IsRemoveDataFromMGO");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MmodataNotImport>(entity =>
            {
                entity.ToTable("MMODataNotImport");

                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.Property(e => e.TableName).HasMaxLength(100);

                entity.Property(e => e.TableRowPk)
                    .HasColumnName("TableRowPK")
                    .HasMaxLength(100);

                entity.HasOne(d => d.DataImportLog)
                    .WithMany(p => p.MmodataNotImport)
                    .HasForeignKey(d => d.DataImportLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMODataNo__DataI__20A44798");
            });

            modelBuilder.Entity<Mmogroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK__MMOGroup__149AF30A471CDED6");

                entity.ToTable("MMOGroup");

                entity.HasIndex(e => new { e.Name, e.BranchId })
                    .HasName("UC_Name_BranchID")
                    .IsUnique();

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Fee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.HouseHoldId).HasColumnName("HouseHoldID");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.Mccpkgroup)
                    .HasColumnName("MCCPKGROUP")
                    .HasMaxLength(250);

                entity.Property(e => e.MsgroupId)
                    .HasColumnName("MSGroupId")
                    .HasMaxLength(255);

                entity.Property(e => e.MsteamId)
                    .HasColumnName("MSTeamId")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Mmogroup)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOGroup__Branch__02E8FC28");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Mmogroup)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOGroup__Centra__0100B3B6");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Mmogroup)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOGroup__Charit__01F4D7EF");

                entity.HasOne(d => d.HouseHold)
                    .WithMany(p => p.Mmogroup)
                    .HasForeignKey(d => d.HouseHoldId)
                    .HasConstraintName("FK__MMOGroup__HouseH__000C8F7D");
            });

            modelBuilder.Entity<MmogroupEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__MMOGroup__7944C8708723FF73");

                entity.ToTable("MMOGroupEvent");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comments).HasMaxLength(255);

                entity.Property(e => e.EndTime).HasMaxLength(10);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Mccpkgroupschedule)
                    .HasColumnName("MCCPKGROUPSCHEDULE")
                    .HasMaxLength(250);

                entity.Property(e => e.MseventId).HasColumnName("MSEventId");

                entity.Property(e => e.RescheduledDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasMaxLength(10);

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MmogroupEvent)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOGroupE__Group__1042F746");
            });

            modelBuilder.Entity<MmogroupEventAttendance>(entity =>
            {
                entity.HasKey(e => e.GroupEventAttendanceId)
                    .HasName("PK__MMOGroup__4EE8A58C1D8A107E");

                entity.ToTable("MMOGroupEventAttendance");

                entity.Property(e => e.GroupEventAttendanceId).HasColumnName("GroupEventAttendanceID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comments).HasMaxLength(255);

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.MmogroupEventAttendance)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOGroupE__Event__1413882A");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MmogroupEventAttendance)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOGroupE__Group__1507AC63");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.MmogroupEventAttendance)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK__MMOGroupE__House__15FBD09C");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmogroupEventAttendance)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOGroupE__Perso__16EFF4D5");

                entity.HasOne(d => d.ReasonNavigation)
                    .WithMany(p => p.MmogroupEventAttendance)
                    .HasForeignKey(d => d.Reason)
                    .HasConstraintName("FK__MMOGroupE__Reaso__46C9F080");
            });

            modelBuilder.Entity<MmogroupLink>(entity =>
            {
                entity.HasKey(e => e.GroupLinkId)
                    .HasName("PK__MMOGroup__2134E9786DA0E5CE");

                entity.ToTable("MMOGroupLink");

                entity.HasIndex(e => new { e.GroupId, e.GroupTypeId })
                    .HasName("UC_GroupID_GroupTypeID")
                    .IsUnique();

                entity.Property(e => e.GroupLinkId).HasColumnName("GroupLinkID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupTypeId).HasColumnName("GroupTypeID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MmogroupLink)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__MMOGroupL__Group__06B98D0C");

                entity.HasOne(d => d.GroupType)
                    .WithMany(p => p.MmogroupLink)
                    .HasForeignKey(d => d.GroupTypeId)
                    .HasConstraintName("FK__MMOGroupL__Group__07ADB145");
            });

            modelBuilder.Entity<MmogroupMembers>(entity =>
            {
                entity.HasKey(e => e.GroupMemberId)
                    .HasName("PK__MMOGroup__344812B245E8EDF7");

                entity.ToTable("MMOGroupMembers");

                entity.Property(e => e.GroupMemberId).HasColumnName("GroupMemberID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comments).HasMaxLength(255);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.RecentDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MmogroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOGroupM__Group__19CC6180");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.MmogroupMembers)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK__MMOGroupM__House__1AC085B9");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmogroupMembers)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOGroupM__Perso__1BB4A9F2");
            });

            modelBuilder.Entity<MmogroupMembersFee>(entity =>
            {
                entity.HasKey(e => e.GroupMemberFeeId)
                    .HasName("PK__MMOGroup__B09CE574CC54530A");

                entity.ToTable("MMOGroupMembersFee");

                entity.Property(e => e.GroupMemberFeeId).HasColumnName("GroupMemberFeeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.GroupMemberId).HasColumnName("GroupMemberID");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.HasOne(d => d.GroupMember)
                    .WithMany(p => p.MmogroupMembersFee)
                    .HasForeignKey(d => d.GroupMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOGroupM__Group__2261A781");
            });

            modelBuilder.Entity<MmogroupType>(entity =>
            {
                entity.HasKey(e => e.GroupTypeId)
                    .HasName("PK__MMOGroup__12195A4DBCC935A7");

                entity.ToTable("MMOGroupType");

                entity.HasIndex(e => new { e.GroupTypeDescription, e.CentralOfficeId })
                    .HasName("UC_GroupTypeDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.GroupTypeId).HasColumnName("GroupTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.GroupTypeDescription).HasMaxLength(255);

                entity.Property(e => e.Mccpkgrouptype)
                    .HasColumnName("MCCPKGROUPTYPE")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmogroupType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOGroupT__Branc__658DA36B");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmogroupType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOGroupT__Centr__63A55AF9");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmogroupType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOGroupT__Chari__64997F32");
            });

            modelBuilder.Entity<MmoimportLog>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__MMOImpor__C8EE2043F54FBED9");

                entity.ToTable("MMOImportLog");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ImportedFile).HasMaxLength(100);

                entity.Property(e => e.IsOverwriteMgo).HasColumnName("IsOverwriteMGO");

                entity.Property(e => e.IsRemoveDataFromMgo).HasColumnName("IsRemoveDataFromMGO");

                entity.Property(e => e.Service).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<MmoimportLogDetail>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__MMOImpor__5E5499A84974BFA9");

                entity.ToTable("MMOImportLogDetail");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.MmoimportLogDetail)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__MMOImport__Statu__153294EC");
            });

            modelBuilder.Entity<Mmolicence>(entity =>
            {
                entity.HasKey(e => e.LicenceId);

                entity.ToTable("MMOLicence");

                entity.Property(e => e.LicenceId).HasColumnName("LicenceID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LicenceNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Mmolicence)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MMOLicence_CentralOffice");
            });

            modelBuilder.Entity<MmomaritalStatus>(entity =>
            {
                entity.HasKey(e => e.MaritalStatusId)
                    .HasName("PK__MMOMarit__C8B1BA52BB52516E");

                entity.ToTable("MMOMaritalStatus");

                entity.HasIndex(e => new { e.MaritalStatusDescription, e.CentralOfficeId })
                    .HasName("UC_MaritalStatusDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.MaritalStatusDescription).HasMaxLength(255);

                entity.Property(e => e.Mccpkmaritalcode)
                    .HasColumnName("MCCPKMARITALCODE")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmomaritalStatus)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOMarita__Branc__4C82C9D6");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmomaritalStatus)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOMarita__Centr__4A9A8164");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmomaritalStatus)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOMarita__Chari__4B8EA59D");
            });

            modelBuilder.Entity<MmomasterTask>(entity =>
            {
                entity.HasKey(e => e.MasterTaskId)
                    .HasName("PK__MMOMaste__91C54A8E05D2F874");

                entity.ToTable("MMOMasterTask");

                entity.HasIndex(e => new { e.Name, e.BranchId })
                    .HasName("UC_TaskName_BranchID")
                    .IsUnique();

                entity.Property(e => e.MasterTaskId).HasColumnName("MasterTaskID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpktask)
                    .HasColumnName("MCCPKTASK")
                    .HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmomasterTask)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOMaster__Branc__7559F6E0");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmomasterTask)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOMaster__Centr__7371AE6E");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmomasterTask)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOMaster__Chari__7465D2A7");
            });

            modelBuilder.Entity<MmomemberCertificate>(entity =>
            {
                entity.HasKey(e => e.MemberCertificateId)
                    .HasName("PK__MMOMembe__EC6DA5EF98E6C660");

                entity.ToTable("MMOMemberCertificate");

                entity.Property(e => e.MemberCertificateId).HasColumnName("MemberCertificateID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.Property(e => e.CertificateIssuerId).HasColumnName("CertificateIssuerID");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.MmomemberCertificate)
                    .HasForeignKey(d => d.CertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Certi__55773733");

                entity.HasOne(d => d.CertificateIssuer)
                    .WithMany(p => p.MmomemberCertificate)
                    .HasForeignKey(d => d.CertificateIssuerId)
                    .HasConstraintName("FK__MMOMember__Certi__575F7FA5");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmomemberCertificate)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Perso__566B5B6C");
            });

            modelBuilder.Entity<MmomemberRule>(entity =>
            {
                entity.HasKey(e => e.MemberRuleId)
                    .HasName("PK__MMOMembe__591C8FB0BF48C4C6");

                entity.ToTable("MMOMemberRule");

                entity.HasIndex(e => new { e.MasterTaskId, e.PersonId, e.PreferenceType, e.OppositePersonId })
                    .HasName("UC_MasterTaskID_PersonID_PreferenceType_OppositePersonID")
                    .IsUnique();

                entity.Property(e => e.MemberRuleId).HasColumnName("MemberRuleID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.MasterTaskId).HasColumnName("MasterTaskID");

                entity.Property(e => e.OppositePersonId).HasColumnName("OppositePersonID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.UnAvailableDate).HasColumnType("datetime");

                entity.Property(e => e.UnAvailableTime).HasMaxLength(10);

                entity.HasOne(d => d.MasterTask)
                    .WithMany(p => p.MmomemberRule)
                    .HasForeignKey(d => d.MasterTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Maste__2AC1E358");

                entity.HasOne(d => d.OppositePerson)
                    .WithMany(p => p.MmomemberRuleOppositePerson)
                    .HasForeignKey(d => d.OppositePersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Oppos__2CAA2BCA");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmomemberRulePerson)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Perso__2BB60791");
            });

            modelBuilder.Entity<MmomemberSkill>(entity =>
            {
                entity.HasKey(e => e.MemberSkillId)
                    .HasName("PK__MMOMembe__244C956EA94B06A7");

                entity.ToTable("MMOMemberSkill");

                entity.HasIndex(e => new { e.SkillId, e.PersonId })
                    .HasName("UC_SkillID_PersonID")
                    .IsUnique();

                entity.Property(e => e.MemberSkillId).HasColumnName("MemberSkillID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmomemberSkill)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Perso__511C966A");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.MmomemberSkill)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Skill__50287231");
            });

            modelBuilder.Entity<Mmomembership>(entity =>
            {
                entity.HasKey(e => e.MembershipMemberId)
                    .HasName("PK__MMOMembe__01DA9F4E908BF12A");

                entity.ToTable("MMOMembership");

                entity.Property(e => e.MembershipMemberId).HasColumnName("MembershipMemberID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Fee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.MembershipCodeId).HasColumnName("MembershipCodeID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.RecentDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.Mmomembership)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK__MMOMember__House__30CFC269");

                entity.HasOne(d => d.MembershipCode)
                    .WithMany(p => p.Mmomembership)
                    .HasForeignKey(d => d.MembershipCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Membe__2FDB9E30");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Mmomembership)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOMember__Perso__31C3E6A2");
            });

            modelBuilder.Entity<MmomembershipCode>(entity =>
            {
                entity.HasKey(e => e.MembershipCodeId)
                    .HasName("PK__MMOMembe__0EE4F46A4CDB6DAE");

                entity.ToTable("MMOMembershipCode");

                entity.HasIndex(e => new { e.ShortCode, e.BranchId })
                    .HasName("UC_MMO_ShortCode_BranchID")
                    .IsUnique();

                entity.Property(e => e.MembershipCodeId).HasColumnName("MembershipCodeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Fee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Mccpkmembercode)
                    .HasColumnName("MCCPKMEMBERCODE")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ShortCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmomembershipCode)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOMember__Branc__70754632");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmomembershipCode)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOMember__Centr__6E8CFDC0");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmomembershipCode)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOMember__Chari__6F8121F9");
            });

            modelBuilder.Entity<MmomembershipEnrolmentForm>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK__MMOMembe__FB05B7BDD2AA2DDC");

                entity.ToTable("MMOMembershipEnrolmentForm");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.MembershipCodeId).HasColumnName("MembershipCodeID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmomembershipEnrolmentForm)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOMember__Branc__16B0C90A");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmomembershipEnrolmentForm)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOMember__Centr__14C88098");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmomembershipEnrolmentForm)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOMember__Chari__15BCA4D1");

                entity.HasOne(d => d.MembershipCode)
                    .WithMany(p => p.MmomembershipEnrolmentForm)
                    .HasForeignKey(d => d.MembershipCodeId)
                    .HasConstraintName("FK__MMOMember__Membe__17A4ED43");
            });

            modelBuilder.Entity<MmomembershipEnrolmentFormUserDefinedFields>(entity =>
            {
                entity.HasKey(e => e.FormUserDefinedFieldId)
                    .HasName("PK__MMOMembe__26C60780AA835EC2");

                entity.ToTable("MMOMembershipEnrolmentFormUserDefinedFields");

                entity.Property(e => e.FormUserDefinedFieldId).HasColumnName("FormUserDefinedFieldID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.MmomembershipEnrolmentFormUserDefinedFields)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Field__1B757E27");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.MmomembershipEnrolmentFormUserDefinedFields)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__FormI__1A8159EE");
            });

            modelBuilder.Entity<MmomembershipMembersFee>(entity =>
            {
                entity.HasKey(e => e.MembershipMemberFeeId)
                    .HasName("PK__MMOMembe__8B0A8B191DCDE449");

                entity.ToTable("MMOMembershipMembersFee");

                entity.Property(e => e.MembershipMemberFeeId).HasColumnName("MembershipMemberFeeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.MembershipMemberId).HasColumnName("MembershipMemberID");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasMaxLength(100);

                entity.Property(e => e.ProcessingFee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.SubcriptionId).HasMaxLength(100);

                entity.Property(e => e.TransectionId).HasMaxLength(50);

                entity.HasOne(d => d.MembershipMember)
                    .WithMany(p => p.MmomembershipMembersFee)
                    .HasForeignKey(d => d.MembershipMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOMember__Membe__34A0534D");
            });

            modelBuilder.Entity<MmomembershipType>(entity =>
            {
                entity.HasKey(e => e.MembershipTypeId)
                    .HasName("PK__MMOMembe__F35A3E59B04A9114");

                entity.ToTable("MMOMembershipType");

                entity.HasIndex(e => new { e.MembershipTypeDescription, e.CentralOfficeId })
                    .HasName("UC_MembershipTypeDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.MembershipTypeId).HasColumnName("MembershipTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpkgrouptype)
                    .HasColumnName("MCCPKGROUPTYPE")
                    .HasMaxLength(250);

                entity.Property(e => e.MembershipTypeDescription).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmomembershipType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOMember__Branc__631B4B14");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmomembershipType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOMember__Centr__613302A2");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmomembershipType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOMember__Chari__622726DB");
            });

            modelBuilder.Entity<Mmomenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("MMOMenu");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.MenuIcon).HasMaxLength(50);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuUrl).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");

                entity.HasOne(d => d.ParentMenu)
                    .WithMany(p => p.InverseParentMenu)
                    .HasForeignKey(d => d.ParentMenuId)
                    .HasConstraintName("FK_MMOMenu_ParentMenu");
            });

            modelBuilder.Entity<Mmomodules>(entity =>
            {
                entity.HasKey(e => e.ModulesId)
                    .HasName("PK__MMOModul__E6C746AD6B9F2E73");

                entity.ToTable("MMOModules");

                entity.Property(e => e.ModulesId).HasColumnName("ModulesID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Mmomodules)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOModule__Centr__6350553E");
            });

            modelBuilder.Entity<Mmonotes>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK__MMONotes__EACE357F4160675C");

                entity.ToTable("MMONotes");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.NoteDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Mmonotes)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMONotes__Create__782C41EA");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Mmonotes)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMONotes__Person__7643F978");
            });

            modelBuilder.Entity<MmopersonAdditonalDetails>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__MMOPerso__AA2FFB85DB87C502");

                entity.ToTable("MMOPersonAdditonalDetails");

                entity.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.DeceasedDate).HasColumnType("datetime");

                entity.Property(e => e.Envelope).HasMaxLength(255);

                entity.Property(e => e.Formal).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.InActiveDate).HasColumnType("datetime");

                entity.Property(e => e.InFormal).HasMaxLength(255);

                entity.Property(e => e.IsMgo).HasColumnName("IsMGO");

                entity.Property(e => e.IsMmo).HasColumnName("IsMMO");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Mccpkpeople)
                    .HasColumnName("MCCPKPEOPLE")
                    .HasMaxLength(250);

                entity.Property(e => e.MccpkphoneprimaryEmail)
                    .HasColumnName("MCCPKPHONEPrimaryEmail")
                    .HasMaxLength(250);

                entity.Property(e => e.MccpkphoneprimaryPhoneNumber)
                    .HasColumnName("MCCPKPHONEPrimaryPhoneNumber")
                    .HasMaxLength(250);

                entity.Property(e => e.MemberShipId)
                    .HasColumnName("MemberShipID")
                    .HasMaxLength(255);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MsmemberObjectId)
                    .HasColumnName("MSMemberObjectId")
                    .HasMaxLength(255);

                entity.Property(e => e.MsuserPrincipalName)
                    .HasColumnName("MSUserPrincipalName")
                    .HasMaxLength(255);

                entity.Property(e => e.SchoolWorkPlace).HasMaxLength(255);

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.Property(e => e.VisitorComments).HasMaxLength(255);

                entity.HasOne(d => d.MaritalStatusNavigation)
                    .WithMany(p => p.MmopersonAdditonalDetails)
                    .HasForeignKey(d => d.MaritalStatus)
                    .HasConstraintName("FK__MMOPerson__Marit__55180FD7");

                entity.HasOne(d => d.MemberShipTypeNavigation)
                    .WithMany(p => p.MmopersonAdditonalDetails)
                    .HasForeignKey(d => d.MemberShipType)
                    .HasConstraintName("FK__MMOPerson__Membe__234BAA19");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.MmopersonAdditonalDetails)
                    .HasForeignKey<MmopersonAdditonalDetails>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOPerson__Perso__13A973D6");

                entity.HasOne(d => d.PrimaryEmailNavigation)
                    .WithMany(p => p.MmopersonAdditonalDetailsPrimaryEmailNavigation)
                    .HasForeignKey(d => d.PrimaryEmail)
                    .HasConstraintName("FK__MMOPerson__Prima__1685E081");

                entity.HasOne(d => d.PrimaryPhoneNumberNavigation)
                    .WithMany(p => p.MmopersonAdditonalDetailsPrimaryPhoneNumberNavigation)
                    .HasForeignKey(d => d.PrimaryPhoneNumber)
                    .HasConstraintName("FK__MMOPerson__Prima__177A04BA");
            });

            modelBuilder.Entity<MmorelationshipMember>(entity =>
            {
                entity.HasKey(e => e.RelationshipMemberId)
                    .HasName("PK__MMORelat__D33E5926E1762346");

                entity.ToTable("MMORelationshipMember");

                entity.HasIndex(e => new { e.RelationshipTypeId, e.OwnerId, e.RelatedId })
                    .HasName("UC_RelationshipTypeID_OwnerID_RelatedID")
                    .IsUnique();

                entity.Property(e => e.RelationshipMemberId).HasColumnName("RelationshipMemberID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.RelatedId).HasColumnName("RelatedID");

                entity.Property(e => e.RelationshipTypeId).HasColumnName("RelationshipTypeID");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.MmorelationshipMemberOwner)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMORelati__Owner__1B2A9A0D");

                entity.HasOne(d => d.Related)
                    .WithMany(p => p.MmorelationshipMemberRelated)
                    .HasForeignKey(d => d.RelatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMORelati__Relat__1C1EBE46");

                entity.HasOne(d => d.RelationshipType)
                    .WithMany(p => p.MmorelationshipMember)
                    .HasForeignKey(d => d.RelationshipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMORelati__Relat__1A3675D4");
            });

            modelBuilder.Entity<MmorelationshipType>(entity =>
            {
                entity.HasKey(e => e.RelationshipTypeId)
                    .HasName("PK__MMORelat__20FE5F618B9C054E");

                entity.ToTable("MMORelationshipType");

                entity.Property(e => e.RelationshipTypeId).HasColumnName("RelationshipTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpkrelcode)
                    .HasColumnName("MCCPKRELCODE")
                    .HasMaxLength(250);

                entity.Property(e => e.OwnerDescription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RelatedDescription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmorelationshipType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMORelati__Branc__169AEF1A");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmorelationshipType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMORelati__Centr__14B2A6A8");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmorelationshipType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMORelati__Chari__15A6CAE1");

                entity.HasOne(d => d.ParentRelationshipType)
                    .WithMany(p => p.InverseParentRelationshipType)
                    .HasForeignKey(d => d.ParentRelationshipTypeId)
                    .HasConstraintName("FK_MMORelationshipType_ParentMenu");
            });

            modelBuilder.Entity<MmoreportLabel>(entity =>
            {
                entity.ToTable("MMOReportLabel");

                entity.HasIndex(e => new { e.Description, e.CentralOfficeId })
                    .HasName("UQ_MMO_ReportLabel_Description_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BottomMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HorizontalPitch).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LabelHeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LabelWidth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LeftMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RightMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TopMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VerticalPitch).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmoreportLabel)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOReport__Branc__6A083FC5");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmoreportLabel)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOReport__Centr__681FF753");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmoreportLabel)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOReport__Chari__69141B8C");
            });

            modelBuilder.Entity<MmoroleMenuPrivilege>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId })
                    .HasName("pk_MMORoleMenuPrivilege");

                entity.ToTable("MMORoleMenuPrivilege");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MmoroleMenuPrivilege)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MMORoleMenuPrivilege_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MmoroleMenuPrivilege)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MMORoleMenuPrivilege_Role");
            });

            modelBuilder.Entity<Mmoskill>(entity =>
            {
                entity.HasKey(e => e.SkillId)
                    .HasName("PK__MMOSkill__DFA091E756E0CA90");

                entity.ToTable("MMOSkill");

                entity.HasIndex(e => new { e.SkillName, e.BranchId })
                    .HasName("UC_SkillName_BranchID")
                    .IsUnique();

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpkgroup)
                    .HasColumnName("MCCPKGROUP")
                    .HasMaxLength(250);

                entity.Property(e => e.SkillGroupId).HasColumnName("SkillGroupID");

                entity.Property(e => e.SkillName).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Mmoskill)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOSkill__Branch__40E62EA1");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Mmoskill)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOSkill__Centra__3EFDE62F");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Mmoskill)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOSkill__Charit__3FF20A68");

                entity.HasOne(d => d.SkillGroup)
                    .WithMany(p => p.Mmoskill)
                    .HasForeignKey(d => d.SkillGroupId)
                    .HasConstraintName("FK__MMOSkill__SkillG__3E09C1F6");
            });

            modelBuilder.Entity<MmoskillGroup>(entity =>
            {
                entity.HasKey(e => e.SkillGroupId)
                    .HasName("PK__MMOSkill__72E756734AD3F6E5");

                entity.ToTable("MMOSkillGroup");

                entity.HasIndex(e => new { e.GroupName, e.CentralOfficeId })
                    .HasName("UC_GroupName_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.SkillGroupId).HasColumnName("SkillGroupID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.GroupName).HasMaxLength(255);

                entity.Property(e => e.Mccpkgrouptype)
                    .HasColumnName("MCCPKGROUPTYPE")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmoskillGroup)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOSkillG__Branc__3A393112");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmoskillGroup)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOSkillG__Centr__3850E8A0");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmoskillGroup)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOSkillG__Chari__39450CD9");
            });

            modelBuilder.Entity<Mmospresult>(entity =>
            {
                entity.ToTable("MMOSPResult");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<MmotaskShift>(entity =>
            {
                entity.HasKey(e => e.TaskShiftId)
                    .HasName("PK__MMOTaskS__9F764DBD6EC6B42B");

                entity.ToTable("MMOTaskShift");

                entity.Property(e => e.TaskShiftId).HasColumnName("TaskShiftID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.MasterTaskId).HasColumnName("MasterTaskID");

                entity.Property(e => e.MstaskEventId).HasColumnName("MSTaskEventId");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.ShiftDate).HasColumnType("datetime");

                entity.Property(e => e.ShiftTimeFrom).HasMaxLength(10);

                entity.Property(e => e.ShiftTimeTo).HasMaxLength(10);

                entity.HasOne(d => d.IsCompleteNavigation)
                    .WithMany(p => p.MmotaskShift)
                    .HasForeignKey(d => d.IsComplete)
                    .HasConstraintName("FK__MMOTaskSh__IsCom__45D5CC47");

                entity.HasOne(d => d.MasterTask)
                    .WithMany(p => p.MmotaskShift)
                    .HasForeignKey(d => d.MasterTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOTaskSh__Maste__4634F3A3");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmotaskShift)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOTaskSh__Perso__472917DC");
            });

            modelBuilder.Entity<MmotaskSkill>(entity =>
            {
                entity.HasKey(e => e.TaskSkillId)
                    .HasName("PK__MMOTaskS__DEC5E77A5BD17C94");

                entity.ToTable("MMOTaskSkill");

                entity.HasIndex(e => new { e.MasterTaskId, e.SkillId })
                    .HasName("UC_MasterTaskID_SkillID")
                    .IsUnique();

                entity.Property(e => e.TaskSkillId).HasColumnName("TaskSkillID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.MasterTaskId).HasColumnName("MasterTaskID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.MasterTask)
                    .WithMany(p => p.MmotaskSkill)
                    .HasForeignKey(d => d.MasterTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOTaskSk__Maste__049C3A70");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.MmotaskSkill)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOTaskSk__Skill__05905EA9");
            });

            modelBuilder.Entity<MmotaskStatus>(entity =>
            {
                entity.HasKey(e => e.TaskStatusId)
                    .HasName("PK__MMOTaskS__C023DD0CC45923BE");

                entity.ToTable("MMOTaskStatus");

                entity.HasIndex(e => new { e.TaskStatusDescription, e.CentralOfficeId })
                    .HasName("UC_TaskStatusDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.TaskStatusId).HasColumnName("TaskStatusID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.TaskStatusDescription).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmotaskStatus)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOTaskSt__Branc__435886F8");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmotaskStatus)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOTaskSt__Centr__41703E86");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmotaskStatus)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOTaskSt__Chari__426462BF");
            });

            modelBuilder.Entity<MmotaskWillingMember>(entity =>
            {
                entity.HasKey(e => e.TaskWillingId)
                    .HasName("PK__MMOTaskW__290AD416E3B43CC2");

                entity.ToTable("MMOTaskWillingMember");

                entity.HasIndex(e => new { e.MasterTaskId, e.PersonId })
                    .HasName("UC_MasterTaskID_PersonID")
                    .IsUnique();

                entity.Property(e => e.TaskWillingId).HasColumnName("TaskWillingID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.AvailableFrom).HasColumnType("datetime");

                entity.Property(e => e.AvailableTo).HasColumnType("datetime");

                entity.Property(e => e.Comments).HasMaxLength(255);

                entity.Property(e => e.MasterTaskId).HasColumnName("MasterTaskID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.MasterTask)
                    .WithMany(p => p.MmotaskWillingMember)
                    .HasForeignKey(d => d.MasterTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOTaskWi__Maste__2138791E");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmotaskWillingMember)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOTaskWi__Perso__222C9D57");
            });

            modelBuilder.Entity<Mmounavailability>(entity =>
            {
                entity.HasKey(e => e.UnavailabilityId)
                    .HasName("PK__MMOUnava__5E886028442BD5EF");

                entity.ToTable("MMOUnavailability");

                entity.Property(e => e.UnavailabilityId).HasColumnName("UnavailabilityID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.EndTime).HasMaxLength(10);

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Pattern).HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasMaxLength(10);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Mmounavailability)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOUnavai__Perso__5888AE08");
            });

            modelBuilder.Entity<MmouserDataAccessibility>(entity =>
            {
                entity.HasKey(e => e.UserAccessId)
                    .HasName("PK__MMOUserD__BA8431D1130479B3");

                entity.ToTable("MMOUserDataAccessibility");

                entity.Property(e => e.UserAccessId).HasColumnName("UserAccessID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MmouserDataAccessibility)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOUserDa__UserI__69FD52CD");
            });

            modelBuilder.Entity<MmouserDefined>(entity =>
            {
                entity.HasKey(e => e.UserDefinedId)
                    .HasName("PK__MMOUserD__35EB0EF521C72DF9");

                entity.ToTable("MMOUserDefined");

                entity.Property(e => e.UserDefinedId).HasColumnName("UserDefinedID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FieldValue).HasMaxLength(255);

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.MmouserDefined)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOUserDe__Field__4CC1F5A1");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.MmouserDefined)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK__MMOUserDe__House__4DB619DA");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmouserDefined)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOUserDe__Perso__4EAA3E13");
            });

            modelBuilder.Entity<MmouserDefinedField>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("PK__MMOUserD__C8B6FF2707F9D9C6");

                entity.ToTable("MMOUserDefinedField");

                entity.HasIndex(e => new { e.FieldDescription, e.FieldType, e.FieldDefaultValue, e.CentralOfficeId })
                    .HasName("UC_MMO_FieldDescription_FieldType_FieldDefaultValue_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.FieldDefaultValue).HasMaxLength(255);

                entity.Property(e => e.FieldDescription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Mccpkufielddef)
                    .HasColumnName("MCCPKUFIELDDEF")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmouserDefinedField)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOUserDe__Branc__4244672E");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmouserDefinedField)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOUserDe__Centr__405C1EBC");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmouserDefinedField)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOUserDe__Chari__415042F5");
            });

            modelBuilder.Entity<MmouserDefinedFieldOption>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PK__MMOUserD__92C7A1DF2505ADEA");

                entity.ToTable("MMOUserDefinedFieldOption");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.OptionValue)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserDefinedFieldId).HasColumnName("UserDefinedFieldID");

                entity.HasOne(d => d.UserDefinedField)
                    .WithMany(p => p.MmouserDefinedFieldOption)
                    .HasForeignKey(d => d.UserDefinedFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOUserDe__UserD__153CB68D");
            });

            modelBuilder.Entity<Mmovisit>(entity =>
            {
                entity.HasKey(e => e.VisitId)
                    .HasName("PK__MMOVisit__4D3AA1BEDFF16A8F");

                entity.ToTable("MMOVisit");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.Property(e => e.ActualDate).HasColumnType("datetime");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.FinishTime).HasMaxLength(10);

                entity.Property(e => e.Mccpkvisits)
                    .HasColumnName("MCCPKVISITS")
                    .HasMaxLength(250);

                entity.Property(e => e.MseventId).HasColumnName("MSEventId");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.StartTime).HasMaxLength(10);

                entity.Property(e => e.TargetDate).HasColumnType("datetime");

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.Property(e => e.VisitTypeId).HasColumnName("VisitTypeID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Mmovisit)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__MMOVisit__Addres__39CF1CBE");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Mmovisit)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__MMOVisit__Person__38DAF885");

                entity.HasOne(d => d.VisitType)
                    .WithMany(p => p.Mmovisit)
                    .HasForeignKey(d => d.VisitTypeId)
                    .HasConstraintName("FK__MMOVisit__VisitT__37E6D44C");
            });

            modelBuilder.Entity<MmovisitLink>(entity =>
            {
                entity.HasKey(e => e.VisitLinkId)
                    .HasName("PK__MMOVisit__889FA2399014B1E8");

                entity.ToTable("MMOVisitLink");

                entity.Property(e => e.VisitLinkId).HasColumnName("VisitLinkID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MmovisitLink)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOVisitL__Perso__3D9FADA2");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.MmovisitLink)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOVisitL__Visit__3CAB8969");
            });

            modelBuilder.Entity<MmovisitType>(entity =>
            {
                entity.HasKey(e => e.VisitTypeId)
                    .HasName("PK__MMOVisit__9BF3CC722B57A5B0");

                entity.ToTable("MMOVisitType");

                entity.HasIndex(e => new { e.VisitTypeDescription, e.CentralOfficeId })
                    .HasName("UC_VisitTypeDescription_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.VisitTypeId).HasColumnName("VisitTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Mccpkgrouptype)
                    .HasColumnName("MCCPKGROUPTYPE")
                    .HasMaxLength(250);

                entity.Property(e => e.VisitTypeDescription).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmovisitType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOVisitT__Branc__1F1B2682");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.MmovisitType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK__MMOVisitT__Centr__1D32DE10");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmovisitType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__MMOVisitT__Chari__1E270249");
            });

            modelBuilder.Entity<MmowebsiteButton>(entity =>
            {
                entity.HasKey(e => e.ButtonId)
                    .HasName("PK__MMOWebsi__EF873A23487DCAE5");

                entity.ToTable("MMOWebsiteButton");

                entity.Property(e => e.ButtonId)
                    .HasColumnName("ButtonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Qrcode).HasColumnName("QRCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MmowebsiteButton)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__MMOWebsit__Branc__50DD6A9F");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.MmowebsiteButton)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOWebsit__Chari__51D18ED8");

                entity.HasOne(d => d.EnrolmentForm)
                    .WithMany(p => p.MmowebsiteButton)
                    .HasForeignKey(d => d.EnrolmentFormId)
                    .HasConstraintName("FK__MMOWebsit__Enrol__54ADFB83");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MmowebsiteButton)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MMOWebsit__UserI__53B9D74A");
            });

            modelBuilder.Entity<MyGivingTaskUserSeen>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.MyGivingTaskId })
                    .HasName("UQ__MyGiving__79FFA854CD4A0FF5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MyGivingTaskId).HasColumnName("MyGivingTaskID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.MyGivingTask)
                    .WithMany(p => p.MyGivingTaskUserSeen)
                    .HasForeignKey(d => d.MyGivingTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MyGivingT__MyGiv__523AD7D5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MyGivingTaskUserSeen)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MyGivingT__UserI__5146B39C");
            });

            modelBuilder.Entity<MyGivingTasks>(entity =>
            {
                entity.HasIndex(e => e.TaskNo)
                    .HasName("uq_task")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TaskName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<OrganisationPreference>(entity =>
            {
                entity.HasIndex(e => e.OrganisationId)
                    .HasName("uq_organisation")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<OrganizationLicenseHistory>(entity =>
            {
                entity.HasKey(e => e.UpdateLicenseHistoryId);

                entity.Property(e => e.UpdateLicenseHistoryId).HasColumnName("UpdateLicenseHistoryID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.OrganizationLicenseHistory)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_OrganizationLicenseHistory_CentralOffice");
            });

            modelBuilder.Entity<OrganizationMmolicenseHistory>(entity =>
            {
                entity.HasKey(e => e.UpdateLicenseHistoryId);

                entity.ToTable("OrganizationMMOLicenseHistory");

                entity.Property(e => e.UpdateLicenseHistoryId).HasColumnName("UpdateLicenseHistoryID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.OrganizationMmolicenseHistory)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_OrganizationMMOLicenseHistory_CentralOffice");
            });

            modelBuilder.Entity<PacerrorLog>(entity =>
            {
                entity.ToTable("PACErrorLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paragraph>(entity =>
            {
                entity.Property(e => e.ParagraphId).HasColumnName("ParagraphID");

                entity.Property(e => e.LetterId).HasColumnName("LetterID");

                entity.HasOne(d => d.Letter)
                    .WithMany(p => p.Paragraph)
                    .HasForeignKey(d => d.LetterId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_paragraph_letter_cas_id");
            });

            modelBuilder.Entity<ParcelFoodItem>(entity =>
            {
                entity.HasOne(d => d.Food)
                    .WithMany(p => p.ParcelFoodItem)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ParcelFoo__FoodI__6CDA8ACF");

                entity.HasOne(d => d.ParcelType)
                    .WithMany(p => p.ParcelFoodItem)
                    .HasForeignKey(d => d.ParcelTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ParcelFoo__Parce__6BE66696");
            });

            modelBuilder.Entity<ParcelType>(entity =>
            {
                entity.Property(e => e.Adddate).HasColumnType("datetime");

                entity.Property(e => e.FoodbankId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.ParcelType)
                    .HasForeignKey(d => d.FoodbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ParcelTyp__Foodb__65396907");
            });

            modelBuilder.Entity<Parcels>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.PackOnDate).HasColumnType("datetime");

                entity.Property(e => e.PackedDate).HasColumnType("datetime");

                entity.Property(e => e.ParcelQrcode).HasColumnName("ParcelQRCode");

                entity.Property(e => e.ParcelToken).HasMaxLength(50);

                entity.HasOne(d => d.Deliverer)
                    .WithMany(p => p.ParcelsDeliverer)
                    .HasForeignKey(d => d.DelivererId)
                    .HasConstraintName("FK_Parcels_Parcels_delivererid");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.FamilyId)
                    .HasConstraintName("FK__Parcels__FamilyI__58147813");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__Parcels__Foodban__6FEC01A4");

                entity.HasOne(d => d.Granter)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.GranterId)
                    .HasConstraintName("FK__Parcels__Granter__13F457F0");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Parcels__Locatio__78B651CF");

                entity.HasOne(d => d.Packer)
                    .WithMany(p => p.ParcelsPacker)
                    .HasForeignKey(d => d.PackerId)
                    .HasConstraintName("FK_Parcels_volunteer_packeted");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__Parcels__RecipeI__04B21460");

                entity.HasOne(d => d.StandardParcelType)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.StandardParcelTypeId)
                    .HasConstraintName("FK__Parcels__Standar__1023C70C");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Parcels__Voucher__14E87C29");
            });

            modelBuilder.Entity<PayAcharityTempData>(entity =>
            {
                entity.HasKey(e => e.PactempId)
                    .HasName("PK__PayAChar__2B11105208D0097A");

                entity.ToTable("PayACharityTempData");

                entity.Property(e => e.PactempId).HasColumnName("PACTempID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.HouseName).HasMaxLength(255);

                entity.Property(e => e.HouseNumber).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.OtherAddressLine).HasMaxLength(255);

                entity.Property(e => e.PacReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayCharityId)
                    .HasColumnName("PayCharityID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.ReturnUrlForLink).HasMaxLength(100);

                entity.Property(e => e.SecretKey).HasMaxLength(100);

                entity.Property(e => e.StreetName).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TransactionReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserHash).HasMaxLength(100);
            });

            modelBuilder.Entity<PaymentImport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateString).HasMaxLength(100);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.PaymentGateway).HasMaxLength(100);

                entity.Property(e => e.PaymentId)
                    .HasColumnName("PaymentID")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Donation)
                    .WithMany(p => p.PaymentImport)
                    .HasForeignKey(d => d.DonationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoCardLessPaymentImport_Donation");
            });

            modelBuilder.Entity<PaypalTransaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PendingPayment>(entity =>
            {
                entity.Property(e => e.PendingPaymentId).HasColumnName("PendingPaymentID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.DonationDate).HasColumnType("date");

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.Gasdseligible).HasColumnName("GASDSEligible");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RegularGiftId).HasColumnName("RegularGiftID");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.PendingPayment)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_PendingPayment_Envelope");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.PendingPayment)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PendingPayment_Method");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.PendingPayment)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PendingPayment_Purpose");

                entity.HasOne(d => d.RegularGift)
                    .WithMany(p => p.PendingPayment)
                    .HasForeignKey(d => d.RegularGiftId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PendingPayment_RegularGift");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => new { e.BranchId, e.Reference })
                    .HasName("uc_branch_reference")
                    .IsUnique();

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.AdministrativeGroupId).HasColumnName("AdministrativeGroupID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BraintreeCustomerId)
                    .HasColumnName("BraintreeCustomerID")
                    .HasMaxLength(50);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.DateAdded)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DefaultClaimTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DefaultEnvelopeId).HasColumnName("DefaultEnvelopeID");

                entity.Property(e => e.DefaultMethodId).HasColumnName("DefaultMethodID");

                entity.Property(e => e.DefaultPurposeId)
                    .HasColumnName("DefaultPurposeID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DonorReceiptEmail).HasMaxLength(100);

                entity.Property(e => e.DonorReference).HasMaxLength(18);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(50);

                entity.Property(e => e.Forenames).HasMaxLength(100);

                entity.Property(e => e.Hmrcaddress)
                    .HasColumnName("HMRCAddress")
                    .HasMaxLength(500);

                entity.Property(e => e.HmrcaddressOverride).HasColumnName("HMRCAddressOverride");

                entity.Property(e => e.HomePhone).HasMaxLength(50);

                entity.Property(e => e.HouseholdId).HasColumnName("HouseholdID");

                entity.Property(e => e.Initials).HasMaxLength(15);

                entity.Property(e => e.LinkedCode).HasMaxLength(100);

                entity.Property(e => e.MobilePhone).HasMaxLength(50);

                entity.Property(e => e.OfficePhone).HasMaxLength(50);

                entity.Property(e => e.OfficePhoneExt).HasMaxLength(50);

                entity.Property(e => e.PayReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");

                entity.Property(e => e.PreferredGreeting).HasMaxLength(50);

                entity.Property(e => e.Reference).HasMaxLength(27);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Suffix).HasMaxLength(15);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.HasOne(d => d.AdministrativeGroup)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.AdministrativeGroupId)
                    .HasConstraintName("FK_Person_AdministrativeGroup");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Person_Branch");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Person_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK_Person_Charity");

                entity.HasOne(d => d.Household)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.HouseholdId)
                    .HasConstraintName("FK_Person_Household");

                entity.HasOne(d => d.InitialContactSourceNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.InitialContactSource)
                    .HasConstraintName("FK_Person_InitialContact");

                entity.HasOne(d => d.PersonType)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.PersonTypeId)
                    .HasConstraintName("FK_Person_PersonType");
            });

            modelBuilder.Entity<PersonCheck>(entity =>
            {
                entity.Property(e => e.PersonCheckId)
                    .HasColumnName("PersonCheckID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CheckedId).HasColumnName("CheckedID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Checked)
                    .WithMany(p => p.PersonCheck)
                    .HasForeignKey(d => d.CheckedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonCheck_Checked");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonCheck)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonCheck_Person");
            });

            modelBuilder.Entity<PersonEvent>(entity =>
            {
                entity.Property(e => e.PersonEventId).HasColumnName("PersonEventID");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PersonSponsored).HasMaxLength(8);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.PersonEvent)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_PersonEvent_Event");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonEvent)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonEvent_Person");
            });

            modelBuilder.Entity<PersonPaymentMethodInfo>(entity =>
            {
                entity.Property(e => e.PersonPaymentMethodInfoId).HasColumnName("PersonPaymentMethodInfoID");

                entity.Property(e => e.AccountBalance).HasColumnType("money");

                entity.Property(e => e.CharityApiKey).HasMaxLength(50);

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.RegisteredOn).HasColumnType("datetime");

                entity.Property(e => e.RegistrationId)
                    .HasColumnName("RegistrationID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonPaymentMethodInfo)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonPaymentMethodInfo_Person");
            });

            modelBuilder.Entity<PersonTask>(entity =>
            {
                entity.Property(e => e.PersonTaskId)
                    .HasColumnName("PersonTaskID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonTask)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonTask_Person");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.PersonTask)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonTask_Task");
            });

            modelBuilder.Entity<PersonType>(entity =>
            {
                entity.HasIndex(e => new { e.PersonTypeDescription, e.CentralOfficeId })
                    .HasName("uc_PersonType_CentralOfficeID")
                    .IsUnique();

                entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.PersonTypeDescription).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.PersonType)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__PersonTyp__Branc__0742D19A");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.PersonType)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_PersonType_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.PersonType)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__PersonTyp__Chari__064EAD61");
            });

            modelBuilder.Entity<PgsdonorContact>(entity =>
            {
                entity.ToTable("PGSDonorContact");

                entity.HasIndex(e => e.ContactId)
                    .HasName("UQ__PGSDonor__5C6625BA2F476F5A")
                    .IsUnique();

                entity.HasIndex(e => e.DonorId)
                    .HasName("UQ__PGSDonor__052E3F998D746418")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.DonorId).HasColumnName("DonorID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.PgsdonorContact)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PGSDonorC__Branc__222BD200");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.PgsdonorContact)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PGSDonorC__Centr__2043898E");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.PgsdonorContact)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PGSDonorC__Chari__2137ADC7");

                entity.HasOne(d => d.Donor)
                    .WithOne(p => p.PgsdonorContact)
                    .HasForeignKey<PgsdonorContact>(d => d.DonorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PGSDonorC__Donor__1F4F6555");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.PgsdonorContact)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PGSDonorC__Purpo__231FF639");
            });

            modelBuilder.Entity<PledgeDetail>(entity =>
            {
                entity.HasKey(e => e.PledgeId);

                entity.Property(e => e.PledgeId).HasColumnName("PledgeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PledgeAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PledgeDetail)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PledgeDetail_Person");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.PledgeDetail)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PledgeDetail_Purpose");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.Property(e => e.ProfessionId).HasColumnName("Profession_ID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Profession)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__Professio__Foodb__6EC2D341");
            });

            modelBuilder.Entity<Purpose>(entity =>
            {
                entity.HasIndex(e => new { e.CentralOfficeId, e.PurposeTitle, e.CharityId, e.BranchId })
                    .HasName("UC_PurposeTitle_CentralOfficeID_CharityID_BranchID")
                    .IsUnique();

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.PurposeDescription).HasMaxLength(255);

                entity.Property(e => e.PurposeTitle).HasMaxLength(10);

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.Purpose)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_Purpose_CentralOffice");
            });

            modelBuilder.Entity<PurposeAccess>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.PurposeAccess)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurposeAc__Branc__6483A542");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.PurposeAccess)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurposeAc__Purpo__638F8109");
            });

            modelBuilder.Entity<QuickDonorGift>(entity =>
            {
                entity.HasKey(e => e.QuickAddId)
                    .HasName("PK_QuickDonorGift_1");

                entity.Property(e => e.QuickAddId).HasColumnName("QuickAddID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BatchReference).HasMaxLength(50);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Forenames)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.HouseName).HasMaxLength(100);

                entity.Property(e => e.HouseNumber).HasMaxLength(50);

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PasswordAnswer).HasMaxLength(300);

                entity.Property(e => e.PasswordQuestion).HasMaxLength(300);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");

                entity.Property(e => e.Postcode).HasMaxLength(8);

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(17);

                entity.Property(e => e.StreetName).HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(4);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QuickDonorGift_Branch");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuickDonorGift_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QuickDonorGift_Charity");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK_QuickDonorGift_Method");

                entity.HasOne(d => d.PersonType)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.PersonTypeId)
                    .HasConstraintName("FK_QuickDonorGift_PersonType");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuickDonorGift_Purpose");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuickDonorGift)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuickDonorGift_User");
            });

            modelBuilder.Entity<ReferralReason>(entity =>
            {
                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ReferrerFamily>(entity =>
            {
                entity.Property(e => e.ReferralDate).HasColumnType("datetime");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.ReferrerFamily)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReferrerF__Famil__051C28B4");

                entity.HasOne(d => d.Reason)
                    .WithMany(p => p.ReferrerFamily)
                    .HasForeignKey(d => d.ReasonId)
                    .HasConstraintName("FK__ReferrerF__Reaso__0BC92643");

                entity.HasOne(d => d.Referrer)
                    .WithMany(p => p.ReferrerFamily)
                    .HasForeignKey(d => d.ReferrerId)
                    .HasConstraintName("FK__ReferrerF__Refer__06104CED");
            });

            modelBuilder.Entity<ReferrerType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Referrers>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostponeDate).HasColumnType("datetime");

                entity.Property(e => e.ReffToken)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ServiceDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Referrers)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Referrers__Addre__00577397");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Referrers)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK__Referrers__Conta__014B97D0");

                entity.HasOne(d => d.DefaultParcelTypeNavigation)
                    .WithMany(p => p.Referrers)
                    .HasForeignKey(d => d.DefaultParcelType)
                    .HasConstraintName("FK__Referrers__Defau__023FBC09");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Referrers)
                    .HasForeignKey(d => d.FoodbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrers__Foodb__01168DA6");

                entity.HasOne(d => d.RefType)
                    .WithMany(p => p.Referrers)
                    .HasForeignKey(d => d.RefTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrers__RefTy__0EA592EE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Referrers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrers__UserI__136A480B");
            });

            modelBuilder.Entity<RegularGift>(entity =>
            {
                entity.Property(e => e.RegularGiftId).HasColumnName("RegularGiftID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.DateFinish).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.Gasdseligible).HasColumnName("GASDSEligible");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.NextDate).HasColumnType("date");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RecentDate).HasColumnType("date");

                entity.Property(e => e.RegularGiftReference).HasMaxLength(8);

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.Type).HasMaxLength(12);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_RegularGift_Branch");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_RegularGift_Envelope");

                entity.HasOne(d => d.GoCardlessSubscription)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.GoCardlessSubscriptionId)
                    .HasConstraintName("FK__RegularGi__GoCar__57B3BA09");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegularGift_Method");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RegularGift_Person");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK_RegularGift_Purpose");

                entity.HasOne(d => d.TrueLayerStandingOrder)
                    .WithMany(p => p.RegularGift)
                    .HasForeignKey(d => d.TrueLayerStandingOrderId)
                    .HasConstraintName("FK__RegularGi__TrueL__67CAF198");
            });

            modelBuilder.Entity<ReportLabel>(entity =>
            {
                entity.HasIndex(e => new { e.Description, e.CentralOfficeId })
                    .HasName("UQ__ReportLa__FD2B73CCD9A8C5DB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BottomMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HorizontalPitch).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LabelHeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LabelWidth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LeftMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RightMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TopMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VerticalPitch).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ReportLabel)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_ReportLabel_Branch");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.ReportLabel)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_ReportLabel_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.ReportLabel)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK_ReportLabel_Charity");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ParentRoleId).HasColumnName("ParentRoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__Role__FoodbankId__28EF74D6");
            });

            modelBuilder.Entity<RoleMenuPrivilege>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId })
                    .HasName("pk_RoleMenuPrivilege");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RoleMenuPrivilege)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleMenuPrivilege_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMenuPrivilege)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleMenuPrivilege_Role");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.ScheduleDate).HasColumnType("date");
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.Property(e => e.SkillName).HasMaxLength(50);
            });

            modelBuilder.Entity<SmartFilter>(entity =>
            {
                entity.Property(e => e.SmartFilterId).HasColumnName("SmartFilterID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FilterName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.SmartFilter)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_SmartFilter_CentralOffice");
            });

            modelBuilder.Entity<StandardComments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CommentDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.StandardComments)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__StandardC__Branc__0B13627E");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.StandardComments)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_StandardComments_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.StandardComments)
                    .HasForeignKey(d => d.CharityId)
                    .HasConstraintName("FK__StandardC__Chari__0A1F3E45");
            });

            modelBuilder.Entity<StandingGift>(entity =>
            {
                entity.Property(e => e.StandingGiftId).HasColumnName("StandingGiftID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BatchReference).HasMaxLength(50);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.EnvelopeId).HasColumnName("EnvelopeID");

                entity.Property(e => e.Gasdseligible).HasColumnName("GASDSEligible");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RegularGiftId).HasColumnName("RegularGiftID");

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.StandingGift)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandingGift_Branch");

                entity.HasOne(d => d.Envelope)
                    .WithMany(p => p.StandingGift)
                    .HasForeignKey(d => d.EnvelopeId)
                    .HasConstraintName("FK_StandingGift_Envelope");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.StandingGift)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK_StandingGift_Method");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.StandingGift)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandingGift_Purpose");

                entity.HasOne(d => d.RegularGift)
                    .WithMany(p => p.StandingGift)
                    .HasForeignKey(d => d.RegularGiftId)
                    .HasConstraintName("FK_StandingGift_RegularGift");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusDescription).HasMaxLength(50);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.AboutServicerOffered).HasMaxLength(500);

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.GrantorId).HasColumnName("GrantorID");

                entity.Property(e => e.PricePerItem).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Stock)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stock__FoodId__73BC9288");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Stock)
                    .HasForeignKey(d => d.FoodbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stock__FoodbankI__72C86E4F");

                entity.HasOne(d => d.Grantor)
                    .WithMany(p => p.Stock)
                    .HasForeignKey(d => d.GrantorId)
                    .HasConstraintName("FK__Stock__GrantorID__74B0B6C1");
            });

            modelBuilder.Entity<StockHistory>(entity =>
            {
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Parcel)
                    .WithMany(p => p.StockHistory)
                    .HasForeignKey(d => d.ParcelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockHist__Parce__0E3B7E9A");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockHistory)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockHist__Stock__0F2FA2D3");
            });

            modelBuilder.Entity<StripePayment>(entity =>
            {
                entity.Property(e => e.StripePaymentId).HasColumnName("StripePaymentID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CardId)
                    .HasColumnName("CardID")
                    .HasMaxLength(50);

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Donation)
                    .WithMany(p => p.StripePayment)
                    .HasForeignKey(d => d.DonationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StripePayment_Donation");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.FrequencyId).HasColumnName("FrequencyID");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.TaskDescription).HasMaxLength(255);

                entity.HasOne(d => d.Frequency)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.FrequencyId)
                    .HasConstraintName("FK_Task_Frequency");

                entity.HasOne(d => d.TaskTypeNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.TaskType)
                    .HasConstraintName("FK_Task_TaskType");
            });

            modelBuilder.Entity<TaskSchedule>(entity =>
            {
                entity.Property(e => e.TaskScheduleId).HasColumnName("TaskScheduleID");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TaskSchedule)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskSchedule_Schedule");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskSchedule)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskSchedule_Task");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.Property(e => e.TaskTypeId).HasColumnName("TaskTypeID");

                entity.Property(e => e.TaskTypeDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<TaxYear>(entity =>
            {
                entity.Property(e => e.TaxYearId).HasColumnName("TaxYearID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.Gasdslimit)
                    .HasColumnName("GASDSLimit")
                    .HasColumnType("decimal(6, 0)");

                entity.Property(e => e.GasdsmaxGiftAmount)
                    .HasColumnName("GASDSMaxGiftAmount")
                    .HasColumnType("decimal(6, 0)");

                entity.Property(e => e.TaxRate).HasColumnType("decimal(2, 0)");

                entity.Property(e => e.TaxYearEnd).HasColumnType("date");

                entity.Property(e => e.TaxYearStart).HasColumnType("date");

                entity.HasOne(d => d.CentralOffice)
                    .WithMany(p => p.TaxYear)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_TaxYear_CentralOffice");
            });

            modelBuilder.Entity<TransactionFee>(entity =>
            {
                entity.Property(e => e.TransactionFeeId).HasColumnName("TransactionFeeID");

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DataDevFixedAmount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.DataDevPercentage).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.DataDevelopmentFee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PenceFee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.PercentFee).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TransactionFeeForTp).HasColumnName("TransactionFeeForTP");
            });

            modelBuilder.Entity<TrueLayerAnnualIncraeseDetails>(entity =>
            {
                entity.HasKey(e => e.TrueLyerAnnualIncraeseDetailsId)
                    .HasName("PK_TrueLyer_AnnualIncraese_Details");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AnnualIncrease).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Currency).HasMaxLength(20);

                entity.Property(e => e.EmailDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RegularGiftId).HasColumnName("RegularGiftID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<TrueLayerStandingOrder>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.AnnualIncrease).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.Currency).HasMaxLength(20);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Simpid)
                    .HasColumnName("SIMPId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.TransactionFeeAmount).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.TrueLayerStandingOrder)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrueLayer__Chari__64EE84ED");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.TrueLayerStandingOrder)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("FK__TrueLayer__Metho__66D6CD5F");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TrueLayerStandingOrder)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrueLayer__Perso__63FA60B4");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.TrueLayerStandingOrder)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__TrueLayer__PlanI__63063C7B");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.TrueLayerStandingOrder)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrueLayer__Purpo__65E2A926");
            });

            modelBuilder.Entity<TwoFactorEmailLogin>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Guid).IsRequired();

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasColumnName("SessionID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TwoFactorEmailLogin)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TwoFactor__UserI__1F1104E1");
            });

            modelBuilder.Entity<TypeMaster>(entity =>
            {
                entity.HasKey(e => e.EnumId);

                entity.Property(e => e.EnumId).HasColumnName("EnumID");

                entity.Property(e => e.EnumGroup).HasMaxLength(50);

                entity.Property(e => e.EnumRefColumn).HasMaxLength(50);

                entity.Property(e => e.EnumRefTable).HasMaxLength(50);

                entity.Property(e => e.EnumText).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.AlternateMobile).HasMaxLength(20);

                entity.Property(e => e.AuditIp)
                    .HasColumnName("AuditIP")
                    .HasMaxLength(20);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.Cofeguid)
                    .HasColumnName("COFEguid")
                    .HasMaxLength(32);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(50);

                entity.Property(e => e.IsMgo)
                    .IsRequired()
                    .HasColumnName("IsMGO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMmo).HasColumnName("IsMMO");

                entity.Property(e => e.IsMmosystemCreated).HasColumnName("IsMMOSystemCreated");

                entity.Property(e => e.IsTfa).HasColumnName("isTFA");

                entity.Property(e => e.Landline).HasMaxLength(20);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LastPasswordChange).HasColumnType("datetime");

                entity.Property(e => e.LoginAttemptDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MsuserObjectId)
                    .HasColumnName("MSUserObjectId")
                    .HasMaxLength(255);

                entity.Property(e => e.MsuserPrincipalName)
                    .HasColumnName("MSUserPrincipalName")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordAnswer)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PasswordQuestion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Postcode).HasMaxLength(8);

                entity.Property(e => e.PrimaryMobile).HasMaxLength(20);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TenantId).HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.BranchNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_Branch");

                entity.HasOne(d => d.CentralOfficeNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CentralOfficeId)
                    .HasConstraintName("FK_User_CentralOffice");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_Charity");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.UserNavigation)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__User__FoodbankId__7E3A20FB");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_Person");
            });

            modelBuilder.Entity<UserDataAccessibility>(entity =>
            {
                entity.HasKey(e => e.UserAccessId)
                    .HasName("PK__UserData__BA8431D19DD7CF0E");

                entity.Property(e => e.UserAccessId).HasColumnName("UserAccessID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CentralOfficeId).HasColumnName("CentralOfficeID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDataAccessibility)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDataA__UserI__1A6AB4A7");
            });

            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AutosaveDonor)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultCityId).HasColumnName("DefaultCityID");

                entity.Property(e => e.DisplayGiftReminder).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.IsSslenccyption).HasColumnName("IsSSLEnccyption");

                entity.Property(e => e.IspaccountName)
                    .HasColumnName("ISPAccountName")
                    .HasMaxLength(100);

                entity.Property(e => e.IspaccountPassword)
                    .HasColumnName("ISPAccountPassword")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ShowInactiveRecords).HasDefaultValueSql("((0))");

                entity.Property(e => e.Smtpport).HasColumnName("SMTPPort");

                entity.Property(e => e.Smtpserver)
                    .HasColumnName("SMTPServer")
                    .HasMaxLength(100);

                entity.Property(e => e.TextAccountName).HasMaxLength(100);

                entity.Property(e => e.TextPassword).HasMaxLength(50);

                entity.HasOne(d => d.DefaultCity)
                    .WithMany(p => p.UserPreference)
                    .HasForeignKey(d => d.DefaultCityId)
                    .HasConstraintName("FK_UserPreference_City");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserPreference)
                    .HasForeignKey<UserPreference>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPreference_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserRole)
                    .HasForeignKey<UserRole>(d => d.UserId)
                    .HasConstraintName("FK_UserRoles_User");
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.DocPath).HasMaxLength(500);

                entity.Property(e => e.Howelsecanyouhelp).HasMaxLength(1000);

                entity.Property(e => e.IsDbscheck).HasColumnName("IsDBScheck");

                entity.Property(e => e.IsDbscheckDate)
                    .HasColumnName("IsDBScheckDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Packingordelivery).HasColumnName("packingordelivery");

                entity.Property(e => e.ProfilePic).HasMaxLength(200);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Volunteer__Conta__5849823D");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.FoodbankId)
                    .HasConstraintName("FK__Volunteer__Foodb__647A4EF8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Volunteer__UserI__145E6C44");
            });

            modelBuilder.Entity<VolunteerAvailability>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.FormDate).HasColumnType("datetime");

                entity.Property(e => e.Frequency).HasColumnName("frequency");

                entity.Property(e => e.Pattern).HasMaxLength(50);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerAvailability)
                    .HasForeignKey(d => d.VolunteerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Volunteer__Volun__127623D2");
            });

            modelBuilder.Entity<VolunteerSkill>(entity =>
            {
                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.VolunteerSkill)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK__Volunteer__Skill__5FEAA405");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerSkill)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("FK__Volunteer__Volun__31B9C501");
            });

            modelBuilder.Entity<VolunteerUnavailability>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.FormDate).HasColumnType("datetime");

                entity.Property(e => e.Frequency).HasColumnName("frequency");

                entity.Property(e => e.Pattern).HasMaxLength(50);

                entity.Property(e => e.Singledate)
                    .HasColumnName("singledate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerUnavailability)
                    .HasForeignKey(d => d.VolunteerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Volunteer__Volun__5B25EEE8");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.FoodbankId).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModfiedDate).HasColumnType("datetime");

                entity.Property(e => e.RedeemedDate).HasColumnType("datetime");

                entity.Property(e => e.VoucherToken)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Voucher__FamilyI__540EDD05");

                entity.HasOne(d => d.Foodbank)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.FoodbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Voucher__Foodban__6721B179");

                entity.HasOne(d => d.ParcelType)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.ParcelTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Voucher__ParcelT__55F72577");

                entity.HasOne(d => d.Referrer)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.ReferrerId)
                    .HasConstraintName("FK__Voucher__Referre__531AB8CC");
            });

            modelBuilder.Entity<WebsiteButton>(entity =>
            {
                entity.HasKey(e => e.ButtonId)
                    .HasName("PK__WebsiteB__EF873A230BA54369");

                entity.Property(e => e.ButtonId)
                    .HasColumnName("ButtonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CharityId).HasColumnName("CharityID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.Qrcode).HasColumnName("QRCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.WebsiteButton)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__WebsiteBu__Branc__6B3B8FC9");

                entity.HasOne(d => d.Charity)
                    .WithMany(p => p.WebsiteButton)
                    .HasForeignKey(d => d.CharityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WebsiteBu__Chari__0CDBAF5F");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.WebsiteButton)
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK__WebsiteBu__Purpo__0DCFD398");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WebsiteButton)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WebsiteBu__UserI__0EC3F7D1");
            });

            modelBuilder.Entity<Xmltbl>(entity =>
            {
                entity.ToTable("xmltbl");

                entity.Property(e => e.DonDate).HasColumnType("datetime");

                entity.Property(e => e.Fore).HasMaxLength(200);

                entity.Property(e => e.House).HasMaxLength(1000);

                entity.Property(e => e.Postcode).HasMaxLength(500);

                entity.Property(e => e.Sur).HasMaxLength(200);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ttl).HasMaxLength(50);
            });
        }
    }
}
