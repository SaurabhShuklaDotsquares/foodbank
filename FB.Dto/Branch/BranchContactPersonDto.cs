using FB.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Dto
{
    public class BranchContactPersonDto
    {
        [SkipProperty]
        public int Id { get; set; }
        [SkipProperty]
        public int BranchID { get; set; }

        [SkipProperty]
        [ExcelProperty]
        [NotMapped]
        public string Branch { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public bool IsDefault { get; set; }
        public string LandLineNo { get; set; }
        public string MobileNo { get; set; }
        public byte? ContactPreference { get; set; }
        public string Title { get; set; }
    }
}
