using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Household
    {
        public Household()
        {
            Address = new HashSet<Address>();
            Mmocontact = new HashSet<Mmocontact>();
            Mmocorrespondence = new HashSet<Mmocorrespondence>();
            Mmogroup = new HashSet<Mmogroup>();
            MmogroupEventAttendance = new HashSet<MmogroupEventAttendance>();
            MmogroupMembers = new HashSet<MmogroupMembers>();
            Mmomembership = new HashSet<Mmomembership>();
            MmouserDefined = new HashSet<MmouserDefined>();
            Person = new HashSet<Person>();
        }

        public int HouseholdId { get; set; }
        public string HouseholdDescription { get; set; }
        public bool? Active { get; set; }
        public string MembershipId { get; set; }
        public bool NeverEmail { get; set; }
        public DateTime? FamilyAdded { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? InActive { get; set; }
        public int? SortBy { get; set; }
        public string Envelope { get; set; }
        public string Formal { get; set; }
        public string InFormal { get; set; }
        public string FullName { get; set; }
        public bool? IsInFormal { get; set; }
        public string Photo { get; set; }
        public string Mccpkhousehold { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Mmocontact> Mmocontact { get; set; }
        public virtual ICollection<Mmocorrespondence> Mmocorrespondence { get; set; }
        public virtual ICollection<Mmogroup> Mmogroup { get; set; }
        public virtual ICollection<MmogroupEventAttendance> MmogroupEventAttendance { get; set; }
        public virtual ICollection<MmogroupMembers> MmogroupMembers { get; set; }
        public virtual ICollection<Mmomembership> Mmomembership { get; set; }
        public virtual ICollection<MmouserDefined> MmouserDefined { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
