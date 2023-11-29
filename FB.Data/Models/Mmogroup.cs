using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmogroup
    {
        public Mmogroup()
        {
            MmogroupEvent = new HashSet<MmogroupEvent>();
            MmogroupEventAttendance = new HashSet<MmogroupEventAttendance>();
            MmogroupLink = new HashSet<MmogroupLink>();
            MmogroupMembers = new HashSet<MmogroupMembers>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ForWhom { get; set; }
        public int? HouseHoldId { get; set; }
        public string Location { get; set; }
        public decimal? Fee { get; set; }
        public int? FeeFrequency { get; set; }
        public bool IsAttendance { get; set; }
        public bool IsFeeCharged { get; set; }
        public bool IsGroupClosed { get; set; }
        public bool IsLeaders { get; set; }
        public bool IsIncludePeopleFilter { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string MsgroupId { get; set; }
        public string MsteamId { get; set; }
        public string TenantId { get; set; }
        public string Mccpkgroup { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Household HouseHold { get; set; }
        public virtual ICollection<MmogroupEvent> MmogroupEvent { get; set; }
        public virtual ICollection<MmogroupEventAttendance> MmogroupEventAttendance { get; set; }
        public virtual ICollection<MmogroupLink> MmogroupLink { get; set; }
        public virtual ICollection<MmogroupMembers> MmogroupMembers { get; set; }
    }
}
