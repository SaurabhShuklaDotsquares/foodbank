using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Family
    {
        public Family()
        {
            FamilyAddress = new HashSet<FamilyAddress>();
            FamilyAgency = new HashSet<FamilyAgency>();
            FamilyMember = new HashSet<FamilyMember>();
            FamilyNotes = new HashSet<FamilyNotes>();
            Fblocation = new HashSet<Fblocation>();
            Feedback = new HashSet<Feedback>();
            FoodbankFamily = new HashSet<FoodbankFamily>();
            Parcels = new HashSet<Parcels>();
            ReferrerFamily = new HashSet<ReferrerFamily>();
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string FamilyName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FamilyToken { get; set; }
        public int? LocalAuthCodeId { get; set; }
        public string DeliveryNote { get; set; }
        public bool? Confirmed { get; set; }
        public int? ConfirmedById { get; set; }
        public int? SelfReffered { get; set; }
        public string Email { get; set; }
        public string Contactno { get; set; }
        public string TotalFamily { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChild { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime? ParcelDeliverDate { get; set; }
        public string Gdprpreferences { get; set; }
        public bool OtherAgency { get; set; }
        public bool Active { get; set; }
        public DateTime? PostponeDate { get; set; }
        public DateTime? AcceptDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<FamilyAddress> FamilyAddress { get; set; }
        public virtual ICollection<FamilyAgency> FamilyAgency { get; set; }
        public virtual ICollection<FamilyMember> FamilyMember { get; set; }
        public virtual ICollection<FamilyNotes> FamilyNotes { get; set; }
        public virtual ICollection<Fblocation> Fblocation { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<FoodbankFamily> FoodbankFamily { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
        public virtual ICollection<ReferrerFamily> ReferrerFamily { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
