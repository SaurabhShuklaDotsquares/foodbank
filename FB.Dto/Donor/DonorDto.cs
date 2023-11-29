using System;
using System.ComponentModel;

namespace FB.Dto
{
    public class DonorDto
    {
        public int MenuID { get; set; }

        public int Id { get; set; }
        public int? Foodid { get; set; }
        public int? Donorid { get; set; }
        public int? Quntity { get; set; }
        public int? Locationid { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? Status { get; set; }
        public int? Batchid { get; set; }
        public bool? OwnPurchase { get; set; }
        public string CauseofDonation { get; set; }

    }
}
