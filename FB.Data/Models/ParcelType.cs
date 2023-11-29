using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ParcelType
    {
        public ParcelType()
        {
            ParcelFoodItem = new HashSet<ParcelFoodItem>();
            Parcels = new HashSet<Parcels>();
            Referrers = new HashSet<Referrers>();
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int FoodbankId { get; set; }
        public DateTime? Adddate { get; set; }

        public virtual Foodbank Foodbank { get; set; }
        public virtual ICollection<ParcelFoodItem> ParcelFoodItem { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
        public virtual ICollection<Referrers> Referrers { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
