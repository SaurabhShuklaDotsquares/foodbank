using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Dto
{
    public class PersonLiteDto
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Reference { get; set; }
        public string EnvelopeNumber { get; set; }
        public string OrganisationName { get; set; }
        public string CharityName { get; set; }
        public string BranchName { get; set; }
        public bool Active { get; set; }
        public bool IsTagged { get; set; }
        public int CentralOfficeID { get; set; }
        public BranchLiteDto Branch { get; set; }
        public int? BranchID { get; set; }
    }
    public class PersonThinDto
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string Reference { get; set; }
        public string BranchReference { get; set; }
        public int? DefaultPurposeID { get; set; }
        public int? DefaultMethodID { get; set; }
        public bool IsDefaultClaimTax { get; set; }
    }
}
