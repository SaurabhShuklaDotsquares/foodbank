using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Skills
    {
        public Skills()
        {
            VolunteerSkill = new HashSet<VolunteerSkill>();
        }

        public int Id { get; set; }
        public string SkillName { get; set; }
        public int FoodBankId { get; set; }

        public virtual ICollection<VolunteerSkill> VolunteerSkill { get; set; }
    }
}
