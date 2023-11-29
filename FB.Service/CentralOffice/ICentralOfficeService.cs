using FB.Data.Models;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface ICentralOfficeService : IDisposable
    {
        CentralOffice GetCentralOffice(int id);
        List<CentralOffice> GetCentralOffices(int CentralOfficeId=0);
    }
}
