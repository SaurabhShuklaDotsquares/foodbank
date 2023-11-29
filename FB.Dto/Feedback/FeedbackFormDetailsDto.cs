using FB.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class FeedbackFormDetailsDto
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int FeedbackMasterId { get; set; }
        public int? FeedbackMasterFieldOptionId { get; set; }
        public string Answer { get; set; }

    }
    public class FeedbackFormDetailsFormDto
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int FeedbackMasterId { get; set; }
        public int? FeedbackMasterFieldOptionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string DateCompletd { get; set; }
        public string dynamicString { get; set; }
        public string FamilyName { get; set; }
        public string ParcelTypeName { get; set; }
        public string PackingDate { get; set; }
        public string PackersName { get; set; }

        public string DeliversName { get; set; }
        public string DeliveryDate { get; set; }

    }

}
