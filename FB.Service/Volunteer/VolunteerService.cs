using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IRepository<Volunteer> repoVolunteer;
        private readonly IRepository<Parcels> repoParcel;
        private readonly IRepository<VolunteerUnavailability> repoVolunteerUnavailability;
        private readonly IRepository<VolunteerAvailability> repoVolunteerAvailability;
        private readonly IRepository<VolunteerSkill> repoVolunteerSkill;
        private readonly IRepository<Skills> repoSkills;

        public VolunteerService(IRepository<Volunteer> _repoVolunteer, IRepository<Parcels> _repoParcel, IRepository<VolunteerUnavailability> _repoVolunteerUnavailability
            , IRepository<VolunteerAvailability> _repoVolunteerAvailability, IRepository<VolunteerSkill> _repoVolunteerSkill, IRepository<Skills> _repoSkills)
        {
            repoVolunteer = _repoVolunteer;
            repoParcel = _repoParcel;
            repoVolunteerUnavailability = _repoVolunteerUnavailability;
            repoVolunteerAvailability = _repoVolunteerAvailability;
            repoVolunteerSkill = _repoVolunteerSkill;
            repoSkills = _repoSkills;
        }
        /// <summary>
        /// To save the volunteer
        /// </summary>
        /// <param name="country"></param>
        /// <param name="isNew"></param>
        public void Save(Volunteer volunteer, bool isNew = true)
        {
            if (isNew)
            {
                repoVolunteer.Insert(volunteer);
            }
            else
            {
                repoVolunteer.Update(volunteer);
            }
        }

        public KeyValuePair<int, List<VolunteerDeliveryListDto>> GetDeliveryList(DataTableServerSide searchModel, int volunteerid)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels) });
            predicate = predicate.And(x => x.DelivererId == volunteerid);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var deliveryList = repoParcel
                .Query().Include(x=>x.Family)
                .Filter(predicate)
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerDeliveryDtoMapper.DeliveryMap(deliveryList);

            KeyValuePair<int, List<VolunteerDeliveryListDto>> resultResponse = new KeyValuePair<int, List<VolunteerDeliveryListDto>>(totalCount, results);

            return resultResponse;

        }

        public KeyValuePair<int, List<VolunteerPackingListDto>> GetPackingList(DataTableServerSide searchModel, int volunteerid)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels) });
            predicate = predicate.And(x => x.PackerId == volunteerid);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var packingList = repoParcel
                .Query()
                .Filter(predicate)
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerPackingListDtoMapper.PackingMap(packingList);

            KeyValuePair<int, List<VolunteerPackingListDto>> resultResponse = new KeyValuePair<int, List<VolunteerPackingListDto>>(totalCount, results);

            return resultResponse;

        }
        public VolunteerUnavailability GetUnavailability(int id)
        {
            return repoVolunteerUnavailability.Query().Filter(x => x.VolunteerId == id).Get().FirstOrDefault();
        }

        public void Save(VolunteerUnavailability unavailability, bool isNew = true)
        {
            if (isNew)
            {
                repoVolunteerUnavailability.Insert(unavailability);
            }
            else
            {
                repoVolunteerUnavailability.Update(unavailability);
            }
        }

        public void Saveavailability(VolunteerAvailability availability, bool isNew = true)
        {
            if (isNew)
            {
                repoVolunteerAvailability.Insert(availability);
            }
            else
            {
                repoVolunteerAvailability.Update(availability);
            }
        }

        public Volunteer GetVolunteerByUserId(int id)
        {
            return repoVolunteer.Query()
                .Include(x => x.Contact)
                .Include(x => x.User)
                .Include(x => x.VolunteerAvailability)
                .Include(x => x.VolunteerUnavailability)
                .Include(x => x.ParcelsDeliverer)
                .Include(x => x.ParcelsPacker)
                .Filter(x => x.UserId == id).Get().FirstOrDefault();
        }

        public VolunteerAvailability GetAvailability(int id)
        {
            return repoVolunteerAvailability.Query().Filter(x => x.VolunteerId == id).Get().FirstOrDefault();
        }

        public VolunteerAvailability GetAvailabilityById(int AvailiabilityId)
        {
            return repoVolunteerAvailability.Query().Filter(x => x.Id == AvailiabilityId).Get().FirstOrDefault();
        }

        public VolunteerUnavailability GetUnavailabilityById(int UnavailiabilityId)
        {
            return repoVolunteerUnavailability.Query().Filter(x => x.Id == UnavailiabilityId).Get().FirstOrDefault();
        }

        public int GetVolunteerCount()
        {
            return repoVolunteer.Query().Get().Count();
        }


        public int GetVolunteerSkillCount()
        {
            return repoVolunteerSkill.Query().Get().Count();
        }
        public List<Skills> GetSkillByFoodbank(int? FoodbankId)
        {
            return repoSkills.Query().Filter(x => x.FoodBankId == FoodbankId).Get().ToList();
        }
        public List<VolunteerSkill> GetVolunteerSkillByVolunteerId(int? volunteerId)
        {
            return repoVolunteerSkill.Query().Filter(x => x.VolunteerId == volunteerId).Get().ToList();
        }
        public void DeleteVolunteerSkillById(int VolunteerSkillid)
        {
            repoVolunteerSkill.Delete(VolunteerSkillid);
        }
        public void Savevolunteerandunavilvolunteer(VolunteerUnavailability entity, bool isNew = true)
        {

            repoVolunteerUnavailability.Insert(entity);

        }

        public KeyValuePair<int, List<VolunteerDto>> GetVolunteers(DataTableServerSide searchModel)
        {
            var predicate = PredicateBuilder.True<Volunteer>();
            //predicate = CustomPredicate.BuildPredicate<FoodItem>(searchModel, new Type[] { typeof(FoodItem) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoVolunteer
                .Query()
                .Filter(predicate)
                .Include(x => x.Contact)
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerDtoMapper.FBMap(centralOfficeList);

            KeyValuePair<int, List<VolunteerDto>> resultResponse = new KeyValuePair<int, List<VolunteerDto>>(totalCount, results);

            return resultResponse;

        }
        public KeyValuePair<int, List<DonorDonationPaymentDto>> GetFoodDonationPayment(DataTableServerSide searchModel)
        {
            var predicate = PredicateBuilder.True<PaymentImport>();
            predicate = CustomPredicate.BuildPredicate<PaymentImport>(searchModel, new Type[] { typeof(PaymentImport) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);


            var results = DonorDonationDtoMapper.PaymentMap(null);

            KeyValuePair<int, List<DonorDonationPaymentDto>> resultResponse = new KeyValuePair<int, List<DonorDonationPaymentDto>>(0, results);

            return resultResponse;

        }

        public List<Volunteer> GetVolunteerList(int foodbankId)
        {
            return repoVolunteer.Query().Include(x => x.Contact)
                .Include(x => x.User)
                .Include(x => x.VolunteerAvailability).Include(x => x.ParcelsDeliverer).Include(x => x.ParcelsPacker).Include(x => x.VolunteerSkill)
                .Include(x => x.VolunteerUnavailability).Filter(x => x.FoodbankId == foodbankId && x.Active.Value && x.IsDbscheck==true).Get().ToList();
        }

        public Volunteer GetVolunteerById(int Id)
        {
            return repoVolunteer.Query().Include(x => x.Contact)
                .Include(x => x.User)
                .Include(x => x.VolunteerAvailability).Include(x => x.ParcelsDeliverer).Include(x => x.ParcelsPacker).Include(x => x.VolunteerSkill)
                .Include(x => x.VolunteerUnavailability).Filter(x => x.Id == Id).Get().FirstOrDefault();
        }

        public KeyValuePair<int, List<AvailabilityDto>> GetAvailabilityList(DataTableServerSide searchModel, int volunteerId)
        {
            var predicate = PredicateBuilder.True<VolunteerAvailability>();
            predicate = CustomPredicate.BuildPredicate<VolunteerAvailability>(searchModel, new Type[] { typeof(VolunteerAvailability) });
            predicate = predicate.And(x => x.VolunteerId == volunteerId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var availabilityList = repoVolunteerAvailability
                .Query()
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerAvailabilityListDtoMapper.AvaillibilityListgMap(availabilityList);

            KeyValuePair<int, List<AvailabilityDto>> resultResponse = new KeyValuePair<int, List<AvailabilityDto>>(totalCount, results);

            return resultResponse;

        }

        public KeyValuePair<int, List<UnavailabilityDto>> GetUnavailabilityList(DataTableServerSide searchModel, int volunteerId)
        {
            var predicate = PredicateBuilder.True<VolunteerUnavailability>();
            predicate = CustomPredicate.BuildPredicate<VolunteerUnavailability>(searchModel, new Type[] { typeof(VolunteerUnavailability) });
            predicate = predicate.And(x => x.VolunteerId == volunteerId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var availabilityList = repoVolunteerUnavailability
                .Query()
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerUnavailabilityDtoMapper.UnavaillibilityListgMap(availabilityList);

            KeyValuePair<int, List<UnavailabilityDto>> resultResponse = new KeyValuePair<int, List<UnavailabilityDto>>(totalCount, results);

            return resultResponse;

        }

        public void DeleteVolunteerAvailability(int id)
        {
            repoVolunteerAvailability.Delete(id);
        }

        public void DeleteVolunteerUnavailability(int id)
        {
            repoVolunteerUnavailability.Delete(id);
        }
        public KeyValuePair<int, List<VolunteerDto>> GetVolunteersByFoodbank(DataTableServerSide searchModel, int FoodbankID,int CharityId)
        {
            var predicate = PredicateBuilder.True<Volunteer>();
            predicate = CustomPredicate.BuildPredicate<Volunteer>(searchModel, new Type[] { typeof(Volunteer), typeof(Fbcontact) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankID && x.Active == true && x.User.Active == true);
            if (CharityId > 0)
            {
                predicate = predicate.And(x => x.User.CharityId == CharityId); //

            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoVolunteer
                .Query()
                .Include(x => x.Contact).Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Volunteer), typeof(IndividualCouple), typeof(packingordelivery), typeof(Fbcontact) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerDtoMapper.FBMap(centralOfficeList);

            KeyValuePair<int, List<VolunteerDto>> resultResponse = new KeyValuePair<int, List<VolunteerDto>>(totalCount, results);

            return resultResponse;

        }
        public KeyValuePair<int, List<VolunteerPackingAdminListDto>> GetPackingListByFoodbank(DataTableServerSide searchModel, int FoodbankId,int CharityId)
        {
            var predicate = PredicateBuilder.True<Volunteer>();
            predicate = CustomPredicate.BuildPredicate<Volunteer>(searchModel, new Type[] { typeof(Volunteer), typeof(Fbcontact), typeof(Parcels) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankId && x.Active == true && x.User.Active == true && (x.ParcelsPacker.Any(y => y.PackerId != null) || x.ParcelsDeliverer.Any(y => y.DelivererId != null)));
            if (CharityId > 0)
            {
                predicate = predicate.And(x => x.User.CharityId == CharityId); //

            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoVolunteer
                .Query()
                .Include(x => x.Contact).Include(x => x.ParcelsPacker).Include(x => x.ParcelsDeliverer).Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Volunteer), typeof(IndividualCouple), typeof(packingordelivery), typeof(Fbcontact), typeof(Parcels) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VolunteerPackingListDtoMapper.PackingAdminMap(centralOfficeList);

            KeyValuePair<int, List<VolunteerPackingAdminListDto>> resultResponse = new KeyValuePair<int, List<VolunteerPackingAdminListDto>>(totalCount, results);


            return resultResponse;

        }
        public List<Volunteer> GetVolunteersListByDate(string fromdate, string todate)
        {
            var predicate1 = PredicateBuilder.True<VolunteerAvailability>();
            if (fromdate != null && todate == null)
            {
                predicate1 = predicate1.And(m => m.FormDate >= Convert.ToDateTime(fromdate));
            }
            else if (fromdate == null && todate != null)
            {
                predicate1 = predicate1.And(m => m.ToDate <= Convert.ToDateTime(todate));
            }
            else if (fromdate != null && todate != null)
            {
                predicate1 = predicate1.And(m => m.FormDate >= Convert.ToDateTime(fromdate) && m.ToDate <= Convert.ToDateTime(todate));
                //predicate1 = predicate1.And(m => m.FormDate >= fromdate && m.ToDate <= todate);
            }
            var data = repoVolunteerAvailability.Query().Filter(predicate1).Get().Select(x => x.VolunteerId).Distinct().ToList();
            var predicate = PredicateBuilder.True<Volunteer>();
            predicate = predicate.And(p => data.Contains(p.Id));
            return repoVolunteer.Query().Filter(predicate).Include(p => p.Contact).Get().ToList();
        }

        public List<ReportVolunterrsDto> GetVolunteersDetailsForReport(VolunteerReportDto model)
        {
            List<int> VolunteersIds = model.VolunterrsIds.Split(',').Select(int.Parse).ToList();
            List<int> shiftType = model.ShiftTypeIds.Split(',').Select(int.Parse).ToList();
            if (VolunteersIds.Count > 0)
            {
                var predicate = PredicateBuilder.True<Volunteer>();
                predicate = predicate.And(p => VolunteersIds.Contains(p.Id));
                predicate = predicate.And(p => shiftType.Contains(p.Packingordelivery.Value));

                var volunteersDetails = repoVolunteer.
                    Query().
                    Filter(predicate).
                    Include(x => x.VolunteerAvailability).
                    Include(x => x.Contact).
                    Get().
                    ToList();

                List<ReportVolunterrsDto> reportVolunterrs = new List<ReportVolunterrsDto>();
                if (volunteersDetails.Count > 0)
                {
                    foreach (var item in volunteersDetails)
                    {
                        ReportVolunterrsDto report = new ReportVolunterrsDto();
                        List<VolunteerAvailability> avilablity = new List<VolunteerAvailability>();

                        foreach (var data in item.VolunteerAvailability.Where(x=>x.FormDate>=model.DateFrom || x.ToDate<=model.DateTo))
                        {
                            DateSort date = new DateSort();

                            date.FromDate = data.FormDate.ToFormatString();
                            date.ToDate = data.FormDate.ToFormatString();
                            report.AllAvailableDate.Add(date);
                            //List<DateTime> allDates = new List<DateTime>();
                            //for (DateTime date = data.FormDate.Value; date <= data.ToDate.Value; date = date.AddDays(1))
                            //{
                            //    allDates.Add(date);
                            //}
                            //foreach (var date in allDates)
                            //{

                            //    if (model.DateFrom != null && model.DateTo == null)
                            //    {
                            //        if (date >= model.DateFrom)
                            //        {
                            //            report.AvailableDate.Add(date.ToFormatString());
                            //        }
                            //    }
                            //    if (model.DateFrom == null && model.DateTo != null)
                            //    {
                            //        if (date <= model.DateTo)
                            //        {
                            //            report.AvailableDate.Add(date.ToFormatString());
                            //        }
                            //    }
                            //    if (model.DateFrom != null && model.DateTo != null)
                            //    {
                            //        if (date >= model.DateFrom && date <= model.DateTo)
                            //        {
                            //            report.AvailableDate.Add(date.ToFormatString());
                            //        }
                            //    }
                        }
                        //if (model.sortBy == (int)SortBy.Date)
                        //{

                        //    report.AllAvailableDate = report.AllAvailableDate.OrderBy(model.DateFrom.ToFormatString()).ToList();
                        //}
                        report.ShiftType = ((WorkType)item.Packingordelivery).GetDescription();
                        report.VolunteersName = item.Contact.ForeName + item.Contact.Surname;
                        if (model.sortBy == (int)SortBy.Volunteer)
                        {
                            report.IsSortByVolunteer = true;
                        }
                        else
                        {
                            report.IsSortByVolunteer = false;
                        }
                        reportVolunterrs.Add(report);
                    }

                   

                    if (model.sortBy == (int)SortBy.Volunteer)
                    {
                        reportVolunterrs = reportVolunterrs.OrderBy(x => x.VolunteersName).ToList();
                    }

                }
                return reportVolunterrs;
            }


            return new List<ReportVolunterrsDto>();

        }


        public void Dispose()
        {
            if (repoVolunteer != null)
            {
                repoVolunteer.Dispose();
            }
        }
    }
}
