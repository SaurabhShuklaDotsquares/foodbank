using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FeedbackMaster
    {
        public FeedbackMaster()
        {
            FeedbackFormDetails = new HashSet<FeedbackFormDetails>();
            FeedbackMasterFieldOption = new HashSet<FeedbackMasterFieldOption>();
        }

        public int FieldId { get; set; }
        public string FieldDescription { get; set; }
        public int? FieldType { get; set; }
        public byte? IsHouseHold { get; set; }
        public string FieldDefaultValue { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool? IsAutoAssignDefaultValue { get; set; }
        public int? FoodbankId { get; set; }

        public virtual Foodbank Foodbank { get; set; }
        public virtual ICollection<FeedbackFormDetails> FeedbackFormDetails { get; set; }
        public virtual ICollection<FeedbackMasterFieldOption> FeedbackMasterFieldOption { get; set; }
    }
}
