using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class VolunteerReportDto
    {
        [DisplayName("From")]
        public DateTime? DateFrom { get; set; }
        [DisplayName("To")]
        public DateTime? DateTo { get; set; }
        [DisplayName("Volunteers")]
        public string VolunterrsIds { get; set; }
        [DisplayName("Type of shift")]
        public string ShiftTypeIds { get; set; }
        public int sortBy { get; set; }
    }
}
