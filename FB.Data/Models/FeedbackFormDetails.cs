using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FeedbackFormDetails
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int FeedbackMasterId { get; set; }
        public int? FeedbackMasterFieldOptionId { get; set; }
        public string Answer { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual FeedbackMaster FeedbackMaster { get; set; }
        public virtual FeedbackMasterFieldOption FeedbackMasterFieldOption { get; set; }
    }
}
