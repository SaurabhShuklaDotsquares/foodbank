using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;

namespace FB.Service
{
    public interface IGrantorService : IDisposable
    {
        Grantor GetGrantorById(int id);
        KeyValuePair<int, List<GrantorDto>> GetGrantorList(DataTableServerSide searchModel, int foodbankId);
        void Delete(int id);
        bool Save(Grantor grantor);
        bool CheckToken(string token);
        List<Grantor> GetAllGrantor(int foodbankId);
        List<ReportGarntorsDto> GetGrantorDetails(GrantorReportDto grantorReportDto);
        Grantor GetGrantorByToken(string token);
    }
}
