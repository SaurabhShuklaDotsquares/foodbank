﻿using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class VolunteerAvailability
    {
        public int Id { get; set; }
        public DateTime? FormDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool AllDay { get; set; }
        public TimeSpan? TimeForm { get; set; }
        public TimeSpan? TimeTo { get; set; }
        public int VolunteerId { get; set; }
        public string Pattern { get; set; }
        public string Comment { get; set; }
        public int? Frequency { get; set; }

        public virtual Volunteer Volunteer { get; set; }
    }
}
