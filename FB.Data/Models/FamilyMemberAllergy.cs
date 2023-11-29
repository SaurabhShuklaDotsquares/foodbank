using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FamilyMemberAllergy
    {
        public int Id { get; set; }
        public int FamilyMemberId { get; set; }
        public int AllergyId { get; set; }

        public virtual Allergies Allergy { get; set; }
        public virtual FamilyMember FamilyMember { get; set; }
    }
}
