using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FB.Service
{
    public class FamilyService : IFamilyService
    {
        private IRepository<Family> repoFamily;
        private IRepository<ReferrerFamily> repoReferrerFamily;
        private IRepository<FamilyMember> repoReFamilyMember;
        private IRepository<FamilyAddress> repoReFamilyAddress;
        private IRepository<Fbaddress> repoFbaddress;
        private IRepository<FamilyMemberAllergy> repoFamilyMemberAllergy;
        private IRepository<FoodbankFamily> repoFoodbankFamily;
        private IRepository<ParcelType> repoParcels;
        private IRepository<FamilyAgency> repoFamilyAgency;
        public FamilyService(IRepository<Family> _repoFamily, IRepository<ReferrerFamily> _repoReferrerFamily, IRepository<FamilyMember> _repoReFamilyMember, IRepository<FamilyAddress> _repoReFamilyAddress,
            IRepository<Fbaddress> _repoFbaddress, IRepository<FamilyMemberAllergy> _repoFamilyMemberAllergy, IRepository<FoodbankFamily> _repoFoodbankFamily
            , IRepository<ParcelType> _repoParcels, IRepository<FamilyAgency> _repoFamilyAgency)
        {

            this.repoFamily = _repoFamily;
            this.repoReferrerFamily = _repoReferrerFamily;
            this.repoReFamilyMember = _repoReFamilyMember;
            this.repoReFamilyAddress = _repoReFamilyAddress;
            this.repoFbaddress = _repoFbaddress;
            this.repoFamilyMemberAllergy = _repoFamilyMemberAllergy;
            this.repoFoodbankFamily = _repoFoodbankFamily;
            repoParcels = _repoParcels;
            repoFamilyAgency = _repoFamilyAgency;
        }

        public Family Save(Family family)
        {
            repoFamily.Insert(family);
            return family;
        }
        public FamilyMember Savesubfamily(FamilyMember family)
        {
            if (family.Id > 0)
            {
                repoReFamilyMember.Update(family);
                return family;
            }
            else
            {
                repoReFamilyMember.Insert(family);
                return family;
            }
        }
        public ReferrerFamily SaveFamilyreferral(ReferrerFamily family)
        {
            repoReferrerFamily.Insert(family);
            return family;
        }
        public void SaveFamilyreferralFoodbank(FoodbankFamily Foodbankfamily)
        {
            repoFoodbankFamily.Insert(Foodbankfamily);

        }
        public FamilyAddress SaveFamilyAddress(FamilyAddress family)
        {
            if (family.Id > 0)
            {
                repoReFamilyAddress.Update(family);
                return family;
            }
            else
            {
                repoReFamilyAddress.Insert(family);
                return family;
            }

        }
        public int CountDailyReferrelLimitByFoodbankId(int foodbankid)
        {
            return repoFoodbankFamily.Query().Include(x => x.Family)
            .Get().Where(x => x.FoodbankId == foodbankid && x.Family.AcceptDate?.Date==System.DateTime.Now.Date && x.Family.ConfirmedById== foodbankid).Count();
        }
        public Fbaddress SaveFbAddress(Fbaddress family)
        {
            if (family.Id > 0)
            {
                repoFbaddress.Update(family);
                return family;
            }
            else
            {
                repoFbaddress.Insert(family);
                return family;
            }

        }

        public List<Family> GetAllFamily(int foodbankId)
        {
            return repoFoodbankFamily.Query().Filter(x => x.FoodbankId == foodbankId && x.Family.Active).Include(x => x.Family.FamilyMember).Get().Select(x => x.Family).ToList();
        }

        public Family GetFamilyDetails(int familyid)
        {
            return repoFamily.Query().Filter(x => x.Id == familyid)
                .Include(x => x.FamilyAddress)
                .Include(x => x.FamilyMember)
                .Include(x => x.ReferrerFamily)
                .Get().FirstOrDefault();
        }
        public Family GetFamilyMoreDetails(int familyid)
        {
            return repoFamily.Query().Filter(x => x.Id == familyid)
                .Include(x => x.FamilyAddress)
                .Include(x => x.FamilyMember)
                .Include(x => x.ReferrerFamily)
                .Include(x => x.CentralOffice)
                .Include(x => x.Charity)
                .Include(x => x.Branch)
                .Get().FirstOrDefault();
        }
        public Fbaddress GetFamilyAddessDetails(int familyaddressid)
        {
            return repoFbaddress.Query().Filter(x => x.Id == familyaddressid)
                .Include(x => x.Country)
                .Get().FirstOrDefault();
        }
        public FamilyMember GetFamilyMember(int familymemberid)
        {
            return repoReFamilyMember.Query().Filter(x => x.Id == familymemberid)
                .Include(x => x.FamilyMemberAllergy)
                .Get().FirstOrDefault();
        }
        public FamilyMemberAllergy GetFamilyMemberAllergy(int familyalleryid)
        {
            return repoFamilyMemberAllergy.Query().Filter(x => x.Id == familyalleryid)
                .Include(x => x.Allergy)
                .Get().FirstOrDefault();
        }
        public void DeleteFamilyMemberAllergy(int familyalleryid)
        {
            repoFamilyMemberAllergy.Delete(familyalleryid);
        }
        public void DeleteFamilyMember(int familymemberid)
        {
            repoReFamilyMember.Delete(familymemberid);
        }
        public List<FamilyMemberAllergy> GetFamilyMemberAllergyDetails(int FamilyMemberId)
        {
            return repoFamilyMemberAllergy.Query().Include(x => x.Allergy)
                .Get().ToList();
        }

        public List<ReferrerFamily> GetReferrerFamily(int referrerId)
        {
            return repoReferrerFamily.Query().Filter(x => x.ReferrerId == referrerId).Include(x => x.Family).Get().ToList();


        }
        public Family UpdateFamily(Family family)
        {
            repoFamily.Update(family);
            return family;
        }
        public KeyValuePair<int, List<MyReferralsDto>> GetMyFamilyByFoodbank(DataTableServerSide searchModel, List<UserDataAccessDto> userDataAccess, int FoodbankId,int roleID, int? organisationId = null, int? charityId = null, int? branchId = null, int? userId = null, bool isUserPrefrence = true)
        {
            var predicate = PredicateBuilder.True<FoodbankFamily>();
            if (searchModel.multisearch != null && searchModel.multisearch.Count > 0)
            {
                List<DataTableMultiSearch> multisearch1 = searchModel.multisearch.Where(e => e.column.ToLower() == "charityid" || e.column.ToLower() == "branchid" || e.column.ToLower() == "groupid").ToList();
                if (multisearch1 != null)
                {
                    searchModel.multisearch.RemoveAll(e => e.column.ToLower() == "charityid" || e.column.ToLower() == "branchid" || e.column.ToLower() == "groupid");
                    predicate = CustomPredicate.BuildPredicate<FoodbankFamily>(searchModel);
                    searchModel.multisearch.AddRange(multisearch1);
                }
                else
                    predicate = CustomPredicate.BuildPredicate<FoodbankFamily>(searchModel);
            }
            else
                predicate = CustomPredicate.BuildPredicate<FoodbankFamily>(searchModel);

            predicate = CustomPredicate.BuildPredicate<FoodbankFamily>(searchModel,new Type[] { typeof(FoodbankFamily), typeof(Family) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankId );
            predicate = predicate.And(x => x.Family.Active == true);
            if (charityId > 0)
            {
                predicate = predicate.And(x => x.Family.CharityId == charityId);
            }
            if (branchId > 0)
            {
                predicate = predicate.And(x => x.Family.BranchId == branchId);
            }
            if (searchModel.multisearch != null && searchModel.multisearch.Count > 0)
            {
                foreach (var item in searchModel.multisearch)
                {
                    if (item.column.ToLower() == "branchid")
                    {
                        int filterBranchId = Convert.ToInt32(item.value);
                        if (filterBranchId > 0)
                        {
                            predicate = predicate.And(p => p.Family.BranchId == filterBranchId);
                        }
                    }
                    else if (item.column.ToLower() == "charityid")
                    {
                        int filterCharityId = Convert.ToInt32(item.value);
                        if (filterCharityId > 0)
                        {
                            predicate = predicate.And(p => p.Family.CharityId == filterCharityId);
                        }
                    }
                    
                }
            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var referrerFamilies = repoFoodbankFamily.Query().Include(x => x.Family).Include(x => x.Family.FamilyAddress)
                .Filter(predicate)
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(FoodbankFamily), typeof(Family)}))
               // .OrderBy(o => o.OrderBy(oo => oo.Family.FamilyName))
                .Get().ToList();
            var results = MyReferralDtoMapper.FamilyListMap(referrerFamilies);
            KeyValuePair<int, List<MyReferralsDto>> resultResponse = new KeyValuePair<int, List<MyReferralsDto>>(0, results);
            return resultResponse;

        }

        public List<ReportReferrerDto> GetFamilyforRefferreport(ReferrerReportDto model)
        {

            List<int> familyIds = model.FamailyIds.Split(',').Select(int.Parse).ToList();
            if (familyIds.Count > 0)
            {
                var predicate = PredicateBuilder.True<Family>();
                predicate = predicate.And(p => familyIds.Contains(p.Id));
                var result = repoFamily
                    .Query()
                    .Filter(predicate)
                    .Include(p => p.FamilyAddress)
                    .Include(p => p.FamilyMember)
                    .Get()
                    .ToList();
                List<ReportReferrerDto> reportData = new List<ReportReferrerDto>();
                foreach (var family in result)
                {
                    ReportReferrerDto reportReferrer = new ReportReferrerDto();
                    reportReferrer.FamilyName = family.FamilyName;
                    reportReferrer.NoofAdults = family.TotalAdults;
                    reportReferrer.NoofChildren = family.TotalChild;
                    reportReferrer.AddedDate = family.AddedDate.ToFormatString();

                    var address = family.FamilyAddress.FirstOrDefault();
                    if (address != null)
                    {
                        var defaultAddress = repoFbaddress.Query().Filter(p => p.Id == address.AddressId).Get().FirstOrDefault();

                        reportReferrer.FullAddressDto.HouseName = defaultAddress.HouseName;
                        reportReferrer.FullAddressDto.HouseNumber = defaultAddress.HouseNumber;
                        reportReferrer.FullAddressDto.StreetName = defaultAddress.Street;
                        reportReferrer.FullAddressDto.OtherAddressLine = defaultAddress.OtherAddressLine;
                        reportReferrer.FullAddressDto.District = defaultAddress.District;
                        reportReferrer.FullAddressDto.City = defaultAddress.City;
                        reportReferrer.FullAddressDto.PostCode = defaultAddress.Postcode;
                        reportReferrer.FullAddressDto.CountryName = defaultAddress.CountryId.HasValue ? defaultAddress.Country?.CountryName.ToTitle() : string.Empty;
                    }

                    if (family.FamilyMember != null)
                    {
                        foreach (var familyMember in family.FamilyMember)
                        {
                            FamailyMemberDetails familyMembers = new FamailyMemberDetails();

                            familyMembers.FamilyMemberName = familyMember.ForeName!=null || familyMember.Surname!=null? familyMember.ForeName + familyMember.Surname:"--";
                            familyMembers.FamilyMemberId = familyMember.Id;
                            familyMembers.Dob = familyMember.Dob!=null? familyMember.Dob.ToFormatString():"--";
                            reportReferrer.FamailyMemberDetails.Add(familyMembers);

                        }

                    }
                    else
                    {
                        reportReferrer.NoMember = "No member Available";
                    }

                    reportData.Add(reportReferrer);
                }
                return reportData;
            }
            return new List<ReportReferrerDto>();
        }

        public List<ReportFamilyDto> GetFamilysDetailsforFamilyReport(FamilyReportDto model)
        {

            List<int> familyIds = model.FamailyIds.Split(',').Select(int.Parse).ToList();
            if (familyIds.Count > 0)
            {
                var predicate = PredicateBuilder.True<Family>();
                predicate = predicate.And(p => familyIds.Contains(p.Id));
                var result = repoFamily
                    .Query()
                    .Filter(predicate)
                    .Include(p => p.FamilyAddress)
                    .Include(p => p.FamilyMember)
                    .Include(p => p.Parcels)
                    .Get()
                    .ToList();
                List<ReportFamilyDto> reportData = new List<ReportFamilyDto>();
                foreach (var family in result)
                {
                    ReportFamilyDto reportReferrer = new ReportFamilyDto();
                    reportReferrer.FamilyName = family.FamilyName;
                    reportReferrer.NoofAdults = family.TotalAdults;
                    reportReferrer.NoofChildren = family.TotalChild;
                    reportReferrer.AddedDate = family.AddedDate.ToFormatString();
                    reportReferrer.IsMemberDetails = model.IncludeFamailyMemberDetails;
                    reportReferrer.IsParcelDetails = model.IncludeParcelDetails;

                    var address = family.FamilyAddress.FirstOrDefault();
                    if (address != null)
                    {
                        var defaultAddress = repoFbaddress.Query().Filter(p => p.Id == address.AddressId).Get().FirstOrDefault();

                        reportReferrer.FullAddressDto.HouseName = defaultAddress.HouseName;
                        reportReferrer.FullAddressDto.HouseNumber = defaultAddress.HouseNumber;
                        reportReferrer.FullAddressDto.StreetName = defaultAddress.Street;
                        reportReferrer.FullAddressDto.OtherAddressLine = defaultAddress.OtherAddressLine;
                        reportReferrer.FullAddressDto.District = defaultAddress.District;
                        reportReferrer.FullAddressDto.City = defaultAddress.City;
                        reportReferrer.FullAddressDto.PostCode = defaultAddress.Postcode;
                        reportReferrer.FullAddressDto.CountryName = defaultAddress.CountryId.HasValue ? defaultAddress.Country?.CountryName.ToTitle() : string.Empty;
                    }

                    if (family.FamilyMember != null)
                    {
                        foreach (var familyMember in family.FamilyMember)
                        {
                            FamailyMemberDetails familyMembers = new FamailyMemberDetails();

                            familyMembers.FamilyMemberName = familyMember.ForeName + familyMember.Surname;
                            familyMembers.FamilyMemberId = familyMember.Id;
                            familyMembers.Dob = familyMember.Dob != null || familyMember.Dob!=DateTime.MinValue? familyMember.Dob.ToFormatString() : "Unknown";
                            reportReferrer.FamailyMemberDetails.Add(familyMembers);

                        }

                    }
                    else
                    {
                        reportReferrer.NoMember = "No member Available";
                    }
                    if (family.Parcels.Count > 0)
                    {
                        foreach (var parcel in family.Parcels)
                        {
                            ParcelsDetails parcelsDetails = new ParcelsDetails();
                            parcelsDetails.Type = parcel.ParcelTypeId == (int)ParcelTypes.Bespoke ? ParcelTypes.Bespoke.GetDescription() : ParcelTypes.Standard.GetDescription(); ;
                            parcelsDetails.DeliveryDate = parcel.DeliveryDate != null ? parcel.DeliveryDate.ToFormatString() : "Unknown";
                            parcelsDetails.DeliveredDate = parcel.DeliveredDate != null ? parcel.DeliveredDate.ToFormatString() : "Unknown";
                            if (parcel.Status != null)
                            {
                                parcelsDetails.Status = parcel.Status == (int)ParcelStatus.Delivered ? ParcelStatus.Delivered.GetDescription() : ParcelStatus.Pending.GetDescription();
                            }
                            else
                            {
                                parcelsDetails.Status = "Unknown";
                            }

                            reportReferrer.ParcelsDetails.Add(parcelsDetails);

                        }

                    }
                    else
                    {
                        reportReferrer.NoMember = "No member Available";
                    }

                    reportData.Add(reportReferrer);
                }
                return reportData;
            }
            return new List<ReportFamilyDto>();
        }

        public List<Family> GetFamilysForFamilyReport(FamilyReportDto model)
        {
            var predicate = PredicateBuilder.True<Family>();

            if (model.DateAdded.HasValue)
            {
                predicate = predicate.And(x => x.AddedDate.Date == model.DateAdded.Value.Date);
            }

            if (model.StatusId != null)
            {
                if (model.StatusId == (int)FamilyReportSatus.Approved)
                {
                    predicate = predicate.And(x => x.Confirmed == true);
                }
                else if (model.StatusId == (int)FamilyReportSatus.Pending)
                {
                    predicate = predicate.And(x => x.Confirmed == null);
                }
                else
                {
                    predicate = predicate.And(x => x.Confirmed != false);
                }
            }


            return repoFamily.Query().Filter(predicate).Get().ToList();
        }

        public KeyValuePair<int, List<FamilyAgency>> GetFeedbackListByFoodbank(DataTableServerSide searchModel, int FoodbankId, int agenciesID)
        {
            var predicate = PredicateBuilder.True<FamilyAgency>();
            predicate = CustomPredicate.BuildPredicate<FamilyAgency>(searchModel, new Type[] { typeof(Feedback), typeof(FamilyAgency), typeof(Agencies), typeof(FeedbackMaster), typeof(FeedbackFormDetails), typeof(ParcelType), typeof(Family), typeof(FamilyAgency) });

            predicate = predicate.And(x => x.Agency.FoodBankId == FoodbankId && x.AgencyId == agenciesID);


            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);


            var repoFeedbacklist = repoFamilyAgency.Query()
                .Include(x => x.Family).Include(x => x.Agency)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.FamilyId))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Feedback), typeof(FamilyAgency), typeof(Agencies), typeof(FeedbackMaster), typeof(FeedbackFormDetails), typeof(ParcelType), typeof(Family), typeof(FamilyAgency) }))
                .GetPage(page, searchModel.length, out totalCount).OrderByDescending(x => x.FamilyId).ToList();

            //var results = FeedbackDtoMapper.MapListFeedbackToFeedbackDto(repoFeedbacklist);
            KeyValuePair<int, List<FamilyAgency>> resultResponse = new KeyValuePair<int, List<FamilyAgency>>(totalCount, repoFeedbacklist);
            return resultResponse;
        }
        public void Dispose()
        {
            if (repoFamily != null)
            {
                repoFamily.Dispose();
                repoFamily = null;
            }
            if (repoReferrerFamily != null)
            {
                repoReferrerFamily.Dispose();
                repoReferrerFamily = null;
            }
            if (repoReFamilyMember != null)
            {
                repoReFamilyMember.Dispose();
                repoReFamilyMember = null;
            }
            if (repoReFamilyAddress != null)
            {
                repoReFamilyAddress.Dispose();
                repoReFamilyAddress = null;
            }
            if (repoFbaddress != null)
            {
                repoFbaddress.Dispose();
                repoFbaddress = null;
            }
            if (repoFamilyMemberAllergy != null)
            {
                repoFamilyMemberAllergy.Dispose();
                repoFamilyMemberAllergy = null;
            }
            if (repoFoodbankFamily != null)
            {
                repoFoodbankFamily.Dispose();
                repoFoodbankFamily = null;
            }
            if (repoFamilyAgency != null)
            {
                repoFamilyAgency.Dispose();
                repoFamilyAgency = null;
            }
        }


    }
}
