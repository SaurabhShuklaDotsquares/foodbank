using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.Core;

namespace FB.Dto
{
    public class BranchDto
    {
        public int BranchID { get; set; }

        [DisplayName("Organisation")]
        public int OrganisationID { get; set; }

        [DisplayName("Charity")]
        public int? CharityID { get; set; }

        [DisplayName("Contact Name")]
        public string ManagerName { get; set; }

        [DisplayName("Branch Description")]
        public string BranchDescription { get; set; }

        [DisplayName("Is Community Building?")]
        public bool CommunityBuilding { get; set; }

        [DisplayName("Branch Reference")]
        public string BranchReference { get; set; }

        [DisplayName("Community Name")]
        public string CommunityName { get; set; }

        [DisplayName("Community Address")]
        public string CommunityAddress { get; set; }

        [DisplayName("Community Postcode")]
        public string CommunityPostcode { get; set; }


        [DisplayName("Percentage Fee")]
        public decimal? PercentageFee { get; set; }

        [DisplayName("Flat Rate")]
        public decimal? FlatRate { get; set; }

        [DisplayName("Threshhold")]
        public decimal? Threshold { get; set; }

        [DisplayName("Percentage fee <= threshhold")]
        public decimal? PercentageLessThanThreshold { get; set; }

        [DisplayName("Percentage fee > threshhold")]
        public decimal? PercentageGreterThanThreshold { get; set; }

        [DisplayName("Email")]
        public string SignatoryEmail { get; set; }

        [DisplayName("Phone")]
        public string SignatoryPhone { get; set; }

        [DisplayName("Address")]
        public string SignatoryAddress { get; set; }

        [DisplayName("Postcode")]
        public string SignatoryPostcode { get; set; }

        [DisplayName("Donor Reference Type")]
        public ReferenceType ReferenceType { get; set; }

        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        public int CreatedBy { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int AuditUserId { get; set; }
        public string AuditIP { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }
        public List<BranchContactPersonDto> BranchContactPersons { get; set; }

        [DisplayName("MyGiving.Online")]
        public bool IsMGO { get; set; }
        [DisplayName("MyMembership")]
        public bool IsMMO { get; set; }
    }
}
