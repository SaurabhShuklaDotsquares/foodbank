using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Dto
{
    public class BranchLiteDto
    {
        public int BranchID { get; set; }
        public string BranchDescription { get; set; }
        public string CharityName { get; set; }
        public string OrganisationName { get; set; }
        public bool CommunityBuilding { get; set; }
        public bool IsDefault { get; set; }
        public string BranchReference { get; set; }
        public bool IsActive { get; set; }
    }
    public class BranchThinDto
    {
        public int BranchID { get; set; }
        public string BranchReference { get; set; }
    }
}
