using FB.Data.Models;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FB.Dto;
using FB.Core;

namespace FB.Service
{
    public class CentralOfficeService : ICentralOfficeService
    {
        private IRepository<CentralOffice> repoCentralOffice;
        public CentralOfficeService(IRepository<CentralOffice> _repoCentralOffice)
        {
            this.repoCentralOffice = _repoCentralOffice;
        }

        public CentralOffice GetCentralOffice(int id)
        {
            return repoCentralOffice.Query()
                 .Filter(c => c.CentralOfficeId == id)
                  .Include(x => x.OrganizationLicenseHistory)
                 .Include(x => x.Licence)
                 .Include(x => x.OrganizationMmolicenseHistory)
                 .Include(x => x.Mmomodules)
                 .Include(x => x.Mmolicence)
                 .Get().FirstOrDefault();
        }

        public List<CentralOffice> GetCentralOffices(int CentralOfficeId=0)
        {
            var predicate = PredicateBuilder.True<CentralOffice>();
         
            if (CentralOfficeId > 0)
            {
                predicate = predicate.And(e => e.CentralOfficeId == CentralOfficeId);
            }
            // old condtion to filter data according to MMO/MGO
            predicate = predicate.And(e => e.Active.Value && (e.IsMgo.Value || e.IsMmo));
            return repoCentralOffice.Query().Filter(predicate).OrderBy(o => o.OrderBy(oo => oo.OrganisationName)).Get().ToList();
            //return repoCentralOffice.Query().Filter(c => c.Active.Value && (c.IsMgo.Value || c.IsMmo)).OrderBy(o => o.OrderBy(oo => oo.OrganisationName)).Get().ToList();
        }
        public void Dispose()
        {
            if (repoCentralOffice != null)
            {
                repoCentralOffice.Dispose();
                repoCentralOffice = null;
            }
        }
    }
}
