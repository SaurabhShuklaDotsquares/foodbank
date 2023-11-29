using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TrueLayerAnnualIncraeseDetails
    {
        public int TrueLyerAnnualIncraeseDetailsId { get; set; }
        public Guid? LinkId { get; set; }
        public int? PlanId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Frequency { get; set; }
        public byte? IntervalUnit { get; set; }
        public bool IsPlanModified { get; set; }
        public int? PersonId { get; set; }
        public int CharityId { get; set; }
        public int BranchId { get; set; }
        public bool? IsGiftAidDonation { get; set; }
        public int PurposeId { get; set; }
        public int? MethodId { get; set; }
        public decimal? TransactionFeeAmount { get; set; }
        public bool IsAnnualIncrease { get; set; }
        public decimal? AnnualIncrease { get; set; }
        public DateTime? EmailDate { get; set; }
        public int? RegularGiftId { get; set; }
        public bool? IsPaymentCompleted { get; set; }
    }
}
