using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Parcels = new HashSet<Parcels>();
        }

        public int Id { get; set; }
        public int ParcelTypeId { get; set; }
        public string VoucherToken { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModfiedDate { get; set; }
        public DateTime? RedeemedDate { get; set; }
        public int? LocationId { get; set; }
        public int? ReferrerId { get; set; }
        public int FamilyId { get; set; }
        public int FoodbankId { get; set; }
        public byte[] VoucherQrcode { get; set; }

        public virtual Family Family { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual ParcelType ParcelType { get; set; }
        public virtual Referrers Referrer { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
    }
}
