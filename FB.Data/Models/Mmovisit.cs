using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmovisit
    {
        public Mmovisit()
        {
            MmovisitLink = new HashSet<MmovisitLink>();
        }

        public int VisitId { get; set; }
        public int? VisitTypeId { get; set; }
        public int? PersonId { get; set; }
        public int? AddressId { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public bool? IsCancelled { get; set; }
        public string Comment { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkvisits { get; set; }
        public string MseventId { get; set; }
        public string TenantId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
        public virtual MmovisitType VisitType { get; set; }
        public virtual ICollection<MmovisitLink> MmovisitLink { get; set; }
    }
}
