using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IAgenciesService : IDisposable
    {
        Agencies GetGrantorById(int id);
        KeyValuePair<int, List<AgenciesDto>> GetGrantorList(DataTableServerSide searchModel, int foodbankId);
        void Delete(int id);
        bool Save(Agencies grantor);
        KeyValuePair<int, List<AgenciesDto>> GetGrantorListByFamilyid(DataTableServerSide searchModel, int familyid);
        List<Agencies> GetAgencyByFoodbankId(int FoodbankId);
        void DeleteFamilyAgency(int id);
        bool SaveFamilyAgency(FamilyAgency familyAgency);
        List<FamilyAgency> GetFamilyAgecnyByFamilyid(int FamilyId,int AgencyId);
    }
}
