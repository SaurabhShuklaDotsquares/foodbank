using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class VolunteerSkill
    {
        public int Id { get; set; }
        public int? SkillId { get; set; }
        public int? VolunteerId { get; set; }

        public virtual Skills Skill { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
