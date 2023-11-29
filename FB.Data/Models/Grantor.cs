using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Grantor
    {
        public Grantor()
        {
            FoodItem = new HashSet<FoodItem>();
            GrantorReceipt = new HashSet<GrantorReceipt>();
            Parcels = new HashSet<Parcels>();
            Stock = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string GrantorToken { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int FoodBankId { get; set; }
        public int ContactId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public int AddressId { get; set; }
        public byte[] GrantorQrcode { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual Fbcontact Contact { get; set; }
        public virtual Foodbank FoodBank { get; set; }
        public virtual ICollection<FoodItem> FoodItem { get; set; }
        public virtual ICollection<GrantorReceipt> GrantorReceipt { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
