using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            FeedbackFormDetails = new HashSet<FeedbackFormDetails>();
        }

        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int FoodbankId { get; set; }
        public DateTime? DateCompletd { get; set; }
        public int? ParcelId { get; set; }

        public virtual Family Family { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual Parcels Parcel { get; set; }
        public virtual ICollection<FeedbackFormDetails> FeedbackFormDetails { get; set; }
    }
}
