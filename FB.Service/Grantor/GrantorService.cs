using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.Service
{
    public class GrantorService : IGrantorService
    {
        private IRepository<Grantor> repoGrantor;
        private IRepository<Parcels> parcels;
        private IRepository<Food> food;

        public GrantorService(IRepository<Grantor> _repoGrantor, IRepository<Parcels> _parcels, IRepository<Food> _food)
        {
            repoGrantor = _repoGrantor;
            parcels = _parcels;
            food = _food;
        }

        public Grantor GetGrantorById(int id)
        {
            return repoGrantor.Query().Filter(x => x.Id == id).Include(x => x.Contact).Include(x => x.Address).Get().FirstOrDefault();
        }

        public Grantor GetGrantorByToken(string token)
        {
            return repoGrantor.Query().Filter(x => x.GrantorToken == token).Include(x => x.Contact).Include(x => x.Address).Get().FirstOrDefault();
        }

        public bool CheckToken(string token)
        {
            var result = repoGrantor.Query().Filter(x => x.GrantorToken == token).Get().FirstOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }

        public KeyValuePair<int, List<GrantorDto>> GetGrantorList(DataTableServerSide searchModel, int foodbankId)
        {
            var predicate = PredicateBuilder.True<Grantor>();
            predicate = CustomPredicate.BuildPredicate<Grantor>(searchModel, new Type[] { typeof(Grantor), typeof(Foodbank), typeof(Fbcontact) });
            predicate = predicate.And(m => m.FoodBankId == foodbankId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var grantorList = repoGrantor
            .Query()
            .Include(x => x.FoodBank)
            .Include(x => x.Contact)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Grantor), typeof(Foodbank), typeof(Fbcontact) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = GrantorDtoMapper.GrantorMapper(grantorList);

            KeyValuePair<int, List<GrantorDto>> resultResponse = new KeyValuePair<int, List<GrantorDto>>(totalCount, results);

            return resultResponse;
        }

        public void Delete(int id)
        {
            repoGrantor.Delete(id);
        }
        public List<Grantor> GetAllGrantor(int foodbankId)
        {
            return repoGrantor.Query().Filter(x=>x.FoodBankId==foodbankId).Include(x => x.Contact).Get().ToList();
        }

        public bool Save(Grantor grantor)
        {
            try
            {
                if (grantor.Id > 0)
                {
                    repoGrantor.Update(grantor);
                }
                else
                {
                    repoGrantor.Insert(grantor);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ReportGarntorsDto> GetGrantorDetails(GrantorReportDto grantorReportDto)
        {
            List<int> grantorIds = grantorReportDto.GrantorId.Split(',').Select(int.Parse).ToList();
            if (grantorIds.Count > 0)
            {
                var predicate = PredicateBuilder.True<Grantor>();
                predicate = predicate.And(p => grantorIds.Contains(p.Id));
                var grantorsDetails = repoGrantor.
                Query().
                Filter(predicate).
                Include(x => x.Stock).
                Get().
                ToList();
                List<ReportGarntorsDto> reportParcelsDtos = new List<ReportGarntorsDto>();
                if (grantorsDetails != null)
                {
                    foreach (var item in grantorsDetails)
                    {
                        ReportGarntorsDto reportGarntors = new ReportGarntorsDto();
                        reportGarntors.AmountReceived = item.TotalAmount ?? 0;
                        reportGarntors.AmountRemaining = item.TotalAmount ?? 0;
                        reportGarntors.GrantorName = item.ForeName ?? "" + item.SurName ?? "";
                        reportGarntors.DateReceived = item.AddedDate != null ? item.AddedDate.ToString("dd-MM-yyyyy") : "";
                        var parcelsDetails = parcels.Query().Filter(x => x.GranterId == item.Id).Include(x => x.StandardParcelType).Include(x => x.Location).Include(x => x.FamilyParcelFoodItem).Get().ToList();

                        foreach (var data in parcelsDetails)
                        {

                            foreach (var fooddetail in data.FamilyParcelFoodItem)
                            {
                                var foodDetails = food.Query().Filter(x => x.Id == fooddetail.FoodId).Get().FirstOrDefault();
                                var stock = item.Stock.Where(x => x.FoodId == fooddetail.FoodId && x.FoodbankId == data.FoodbankId).FirstOrDefault();
                                FoodBought foodBought = new FoodBought();
                                foodBought.FoodItemName = foodDetails.Name != null ? foodDetails.Name : "";
                                foodBought.Quantity = fooddetail.Quantity.ToString() ?? "";
                                foodBought.Price = stock != null ? fooddetail.Quantity * stock.PricePerItem : 00;
                                foodBought.DateBought = "--";
                                reportGarntors.FoodBought.Add(foodBought);
                            }
                            ParcelsLocation parcelsLocation = new ParcelsLocation();
                            parcelsLocation.Location = data.Location != null && data.Location.Name != null ? data.Location.Name : null;
                            parcelsLocation.ParceType = data.StandardParcelType != null && data.StandardParcelType.Name != null ? data.StandardParcelType.Name : null;
                            parcelsLocation.DatePacked = data.PackedDate != null ? data.PackedDate.ToString() : null;
                            parcelsLocation.DateDelivered = data.DeliveredDate != null ? data.DeliveredDate.ToString() : null;
                            reportGarntors.ParcelsLocation.Add(parcelsLocation);
                        }
                        reportParcelsDtos.Add(reportGarntors);
                    }
                    return reportParcelsDtos;
                }
            }
            return new List<ReportGarntorsDto>();
        }

        public void Dispose()
        {
            if (repoGrantor != null)
            {
                repoGrantor.Dispose();
                repoGrantor = null;
            }
        }
    }
}
