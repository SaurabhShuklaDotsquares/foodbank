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
    public class AgenciesService : IAgenciesService
    {
        private IRepository<Agencies> repoAgencies;
        private IRepository<FamilyAgency> repoFamilyAgency;

        public AgenciesService(IRepository<Agencies> _repoAgencies, IRepository<FamilyAgency> _repoFamilyAgency)
        {
            repoAgencies = _repoAgencies;
            repoFamilyAgency = _repoFamilyAgency;
       }

        public Agencies GetGrantorById(int id)
        {
            return repoAgencies.Query().Filter(x => x.Id == id).Include(x => x.Contact).Include(x => x.Address).Get().FirstOrDefault();
        }

        public KeyValuePair<int, List<AgenciesDto>> GetGrantorList(DataTableServerSide searchModel, int foodbankId)
        {
            var predicate = PredicateBuilder.True<Agencies>();
            predicate = CustomPredicate.BuildPredicate<Agencies>(searchModel, new Type[] { typeof(Agencies), typeof(Fbcontact) });
            predicate = predicate.And(m => m.FoodBankId == foodbankId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var AgencyList = repoAgencies
            .Query()
            .Include(x => x.FoodBank)
            .Include(x => x.Contact)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Agencies), typeof(Fbcontact) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = AgencyDtoMapper.AgencyMapper(AgencyList);

            KeyValuePair<int, List<AgenciesDto>> resultResponse = new KeyValuePair<int, List<AgenciesDto>>(totalCount, results);

            return resultResponse;
        }
        public KeyValuePair<int, List<AgenciesDto>> GetGrantorListByFamilyid(DataTableServerSide searchModel, int familyid)
        {
            var predicate = PredicateBuilder.True<FamilyAgency>();
            predicate = CustomPredicate.BuildPredicate<FamilyAgency>(searchModel, new Type[] { typeof(Agencies), typeof(Fbcontact) });
            predicate = predicate.And(m => m.FamilyId == familyid);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var AgencyList = repoFamilyAgency
            .Query()
            .Include(x => x.Agency).Include(x => x.Agency.Contact)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Agencies), typeof(Fbcontact) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = AgencyDtoMapper.AgencyMapper(AgencyList);

            KeyValuePair<int, List<AgenciesDto>> resultResponse = new KeyValuePair<int, List<AgenciesDto>>(totalCount, results);

            return resultResponse;
        }
        public List<Agencies> GetAgencyByFoodbankId(int FoodbankId)
        {
            return repoAgencies.Query().Filter(x => x.FoodBankId == FoodbankId).Get().ToList();
        }
        public List<FamilyAgency> GetFamilyAgecnyByFamilyid(int FamilyId,int AgencyId)
        {
            return repoFamilyAgency.Query().Filter(x => x.FamilyId == FamilyId && x.AgencyId== AgencyId).Get().ToList();
        }
        public void Delete(int id)
        {
            repoAgencies.Delete(id);
        }
        public void DeleteFamilyAgency(int id)
        {
            repoFamilyAgency.Delete(id);
        }
        public bool SaveFamilyAgency(FamilyAgency familyAgency)
        {
            repoFamilyAgency.Insert(familyAgency);
            return true;
        }
        public bool Save(Agencies grantor)
        {
            try
            {
                if (grantor.Id > 0)
                {
                    repoAgencies.Update(grantor);
                }
                else
                {
                    repoAgencies.Insert(grantor);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (repoAgencies != null)
            {
                repoAgencies.Dispose();
                repoAgencies = null;
            }
        }
    }
}
