using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ReportVolunterrsDto
    {
        public ReportVolunterrsDto()
        {
            AllAvailableDate = new List<DateSort>();
        }
        public List<DateSort> AllAvailableDate { get; set; }
        public string ShiftType { get; set; }
        public string VolunteersName { get; set; }
        public bool IsSortByVolunteer { get; set; }
    }

    public class DateSort 
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

}
