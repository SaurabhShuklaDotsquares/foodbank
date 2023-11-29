using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Agent
    {
        public Agent()
        {
            AgentUser = new HashSet<AgentUser>();
            Charity = new HashSet<Charity>();
        }

        public int AgentId { get; set; }
        public string AgentNumber { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Signatory { get; set; }
        public string Position { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string GatewayId { get; set; }
        public string GatewayPassword { get; set; }
        public string Email { get; set; }
        public int CentralOfficeId { get; set; }
        public bool IsBlockedByAdmin { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public string Hmrcreference { get; set; }
        public int? CountryId { get; set; }
        public string PostCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string TelephoneExt { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string CharityOnlineUserId { get; set; }
        public string CharityOnlinePassword { get; set; }
        public string PasswordSalt { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Country Country { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AgentUser> AgentUser { get; set; }
        public virtual ICollection<Charity> Charity { get; set; }
    }
}
