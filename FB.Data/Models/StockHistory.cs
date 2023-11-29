using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class StockHistory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int FoodItemId { get; set; }
        public int ParcelId { get; set; }
        public int StockId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Parcels Parcel { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
