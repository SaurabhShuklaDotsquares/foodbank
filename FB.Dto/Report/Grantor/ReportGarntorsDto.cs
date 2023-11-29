using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ReportGarntorsDto
    {
        public ReportGarntorsDto()
        {
            FoodBought = new List<FoodBought>();
            ParcelsLocation = new List<ParcelsLocation>();
        }

        public decimal? AmountReceived { get; set; }
        public decimal? AmountRemaining { get; set; }
        public string DateReceived { get; set; }
        public string GrantorName { get; set; }
        public List<FoodBought> FoodBought { get; set; }
        public List<ParcelsLocation> ParcelsLocation { get; set; }
    }
    public class FoodBought
    {
        public string FoodItemName { get; set; }
        public string Quantity { get; set; }
        public decimal? Price { get; set; }
        public string DateBought { get; set; }
    }

    public class ParcelsLocation 
    {
        public string Location { get; set; }
        public string ParceType { get; set; }
        public string DatePacked { get; set; }
        public string DateDelivered { get; set; }
    }


}
