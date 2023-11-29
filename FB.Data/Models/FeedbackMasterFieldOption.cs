using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FeedbackMasterFieldOption
    {
        public FeedbackMasterFieldOption()
        {
            FeedbackFormDetails = new HashSet<FeedbackFormDetails>();
        }

        public int OptionId { get; set; }
        public string OptionValue { get; set; }
        public int? UserDefinedFieldId { get; set; }

        public virtual FeedbackMaster UserDefinedField { get; set; }
        public virtual ICollection<FeedbackFormDetails> FeedbackFormDetails { get; set; }
    }
}
