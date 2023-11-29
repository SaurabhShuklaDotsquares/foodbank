using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Referrers
    {
        public Referrers()
        {
            ReferrerFamily = new HashSet<ReferrerFamily>();
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public int RefTypeId { get; set; }
        public int? ContactId { get; set; }
        public int? AddressId { get; set; }
        public string Name { get; set; }
        public string ReffToken { get; set; }
        public bool IsVoucher { get; set; }
        public int? DefaultParcelType { get; set; }
        public string ServiceDescription { get; set; }
        public int UserId { get; set; }
        public int FoodbankId { get; set; }
        public int IsStatus { get; set; }
        public DateTime? PostponeDate { get; set; }
        public bool Active { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual Fbcontact Contact { get; set; }
        public virtual ParcelType DefaultParcelTypeNavigation { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual ReferrerType RefType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ReferrerFamily> ReferrerFamily { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
