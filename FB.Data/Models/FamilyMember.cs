using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FamilyMember
    {
        public FamilyMember()
        {
            FamilyMemberAllergy = new HashSet<FamilyMemberAllergy>();
        }

        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int? EnthcicityId { get; set; }
        public DateTime? Dob { get; set; }
        public bool? IsPrimaryContact { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public string ContactNo { get; set; }
        public bool? IsAdult { get; set; }

        public virtual Ethnicity Enthcicity { get; set; }
        public virtual Family Family { get; set; }
        public virtual ICollection<FamilyMemberAllergy> FamilyMemberAllergy { get; set; }
    }
}
