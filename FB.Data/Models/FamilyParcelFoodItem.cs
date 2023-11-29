using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FamilyParcelFoodItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ParcelId { get; set; }
        public int FoodId { get; set; }

        public virtual Food Food { get; set; }
        public virtual Parcels Parcel { get; set; }
    }
}
