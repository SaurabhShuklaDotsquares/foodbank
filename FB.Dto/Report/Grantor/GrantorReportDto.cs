using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FB.Dto
{
    public class GrantorReportDto
    {
        [Display(Name ="Grantors-")]
        public string GrantorId { get; set; }
    }
}
